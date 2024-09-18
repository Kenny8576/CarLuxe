using System;
using AutoMapper;
using BiddingService.DTOs;
using BiddingService.Model;
using BiddingService.Services;
using Contracts;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;

namespace BiddingService.Controllers;
[ApiController]
[Route("api/[controller]")]
public class BidController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly GrpcAuctionClient _grpcClient;

    public BidController(IMapper mapper, IPublishEndpoint publishEndpoint, GrpcAuctionClient grpcClient)
    {
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
        _grpcClient = grpcClient;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<BidDto>> PlacedBid(string auctionId, int Amount )
    {
        var auction = await DB.Find<Auction>().OneAsync(auctionId);
        
        if( auction == null  )
        {
            // TODO: check aution service if that has auction
            auction = _grpcClient.GetAuction(auctionId);

            if ( auction == null ) return BadRequest("Cannot accept bids on this auction at this time");

            return NotFound();
        }

        if(auction.Seller == User.Identity.Name )
        {
            return BadRequest("You cannot bid on your own auction");
        }

        var bid = new Bid
        {
            Amount = Amount,
            Bidder = User.Identity.Name,
            AuctionId = auctionId
        };

        if(auction.AuctionEnd < DateTime.UtcNow){
            bid.BidStatus = BidStatus.Finished;
        } else
        {   
        var highBid = await DB.Find<Bid>()
                     .Match(a =>a.AuctionId == auctionId )
                     .Sort(b => b.Descending(x => x.Amount))
                     .ExecuteFirstAsync();

        if(highBid != null && Amount > highBid.Amount || highBid == null)
        {
            bid.BidStatus = Amount > highBid.Amount 
                            ? BidStatus.Accepted
                            : BidStatus.AcceptedBelowReserve;
        }

        if(highBid != null && bid.Amount <= highBid.Amount)
        {
            bid.BidStatus = BidStatus.TooLow;
        }
        }

        await DB.SaveAsync(bid);
        await _publishEndpoint.Publish(_mapper.Map<BidPlaced>(bid));

        return Ok(_mapper.Map<BidDto>(bid));
    }

    [HttpGet("{auctionId}")]
    public async Task<ActionResult<List<BidDto>>> GetBidForAuction(string auctionId)
    {
        var bids = await DB.Find<Bid>().Match(a => a.AuctionId == auctionId)
                           .Sort(b => b.Descending(x => x.BidTime))
                           .ExecuteAsync();
        
        return bids.Select(_mapper.Map<BidDto>).ToList();
    }
}
