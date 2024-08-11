using System;
using AuctionService.Data;
using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Controllers;

[ApiController]
[Route("api/auctions")]
public class AuctionController : ControllerBase
{
    private readonly AuctionDbContext _context;
    private readonly IMapper _mapper;

    public AuctionController(AuctionDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<AuctionDtos>>> GetAllAuction()
    {
        var auctions = await _context.Auctions
                        .Include(x => x.Item)
                        .OrderByDescending(x => x.Item.Make)
                        .ToListAsync();

        return _mapper.Map<List<AuctionDtos>>(auctions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<List<AuctionDtos>>> GetAuctionById(Guid id)
    {
        var auctions = await _context.Auctions
        .Include(x => x.Item)
        .FirstOrDefaultAsync(x => x.Id == id);

        if (auctions == null) return NotFound();

        return _mapper.Map<ActionResult>(auctions);
    }

    [HttpPost]
    public async Task<ActionResult<AuctionDtos>> CreateAuction(CreateAuctionDto auctionDto)
    {
        var auction = _mapper.Map<Auction>(auctionDto);

        // TODO: add current User as Seller
        auction.Seller = "Test";

        _context.Auctions.Add(auction);

        var result = await _context.SaveChangesAsync() > 0;

        if (!result) return BadRequest("Could not save changes to the Database");

        return CreatedAtAction(nameof(GetAuctionById),
         new {auction.Id}, _mapper.Map<AuctionDtos>(auction));

    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAuction(Guid id, UpdateAuctionDto updateAuctionDto)
    {
       var auction = await _context.Auctions.Include(x => x.Item)
       .FirstOrDefaultAsync(x => x.Id == id);

        if(auction == null) return NotFound();

       // TODO: Check Seller = username

        auction.Item.Make = updateAuctionDto.Make ?? auction.Item.Make;
        auction.Item.Model = updateAuctionDto.Model ?? auction.Item.Model;
        auction.Item.Year = updateAuctionDto.Year ?? auction.Item.Year;
        auction.Item.Color = updateAuctionDto.Color ?? auction.Item.Color;
        auction.Item.Mileage = updateAuctionDto.Mileage ?? auction.Item.Mileage;

        var result = await _context.SaveChangesAsync() > 0;

        if (result) return Ok();

        return BadRequest("Problem Saving Changes");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAuction(Guid id)
    {
        var auction = await _context.Auctions.FindAsync(id);

        if(auction == null) return NotFound();

        // TODO: check seller == username

        _context.Remove(auction);

        var result = await _context.SaveChangesAsync() > 0;
        if (!result) return BadRequest("Could not update DB");

        return Ok();
    }
}
