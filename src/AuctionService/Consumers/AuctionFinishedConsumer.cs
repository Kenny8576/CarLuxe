using System;
using AuctionService.Data;
using AuctionService.Entities;
using Contracts;
using MassTransit;

namespace AuctionService.Consumers;

public class AuctionFinishedConsumer : IConsumer<AuctionFinished>
{
    private readonly AuctionDbContext _dbcontext;

    public AuctionFinishedConsumer(AuctionDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }
    public async Task Consume(ConsumeContext<AuctionFinished> context)
    {
         Console.WriteLine("--> Consuming Finished Auction");

         var auction = await _dbcontext.Auctions.FindAsync(context.Message.AuctionId);

         if(context.Message.ItemSold)
         {
            auction.Winner = context.Message.Winner;
            auction.SoldAmount = context.Message.Amount;
         }

         auction.Status = auction.SoldAmount > auction.ReservePrice ? Status.Finished : Status.ReserveNotMet;

         await _dbcontext.SaveChangesAsync();
    }
}
