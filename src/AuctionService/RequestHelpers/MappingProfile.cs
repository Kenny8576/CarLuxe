using System;
using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;

namespace AuctionService.RequestHelpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Auction, AuctionDtos>().IncludeMembers(x => x.Item);
        CreateMap<Item, AuctionDtos>();
        CreateMap<CreateAuctionDto, Auction>().ForMember(x => x.Item, o => o.MapFrom(s => s));
        CreateMap<CreateAuctionDto, Item>();
    }
}
