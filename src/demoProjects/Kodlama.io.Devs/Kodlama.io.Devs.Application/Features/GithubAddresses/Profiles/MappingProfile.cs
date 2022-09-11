using AutoMapper;
using Core.Persistence.Paging;
using Kodlama.io.Devs.Application.Features.GithubAddresses.Commands.CreateGithubAddress;
using Kodlama.io.Devs.Application.Features.GithubAddresses.Commands.DeleteGithubAddress;
using Kodlama.io.Devs.Application.Features.GithubAddresses.Commands.UpdateGithubAddress;
using Kodlama.io.Devs.Application.Features.GithubAddresses.Dtos;
using Kodlama.io.Devs.Application.Features.GithubAddresses.Models;
using Kodlama.io.Devs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.GithubAddresses.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GithubAddress, CreatedGithubAddressDto>().ReverseMap();
            CreateMap<GithubAddress, CreateGithubAddressCommand>().ReverseMap();
            CreateMap<GithubAddress, UpdatedGithubAddressDto>().ReverseMap();
            CreateMap<GithubAddress, UpdateGithubAddressCommand>().ReverseMap();
            CreateMap<GithubAddress, DeletedGithubAddressDto>().ReverseMap();
            CreateMap<GithubAddress, DeleteGithubAddressCommand>().ReverseMap();
            CreateMap<GithubAddress, GithubAddressListDto>().ReverseMap();
            CreateMap<IPaginate<GithubAddress>, GithubAddressListModel>().ReverseMap();
            CreateMap<GithubAddress, GithubAddressGetByUserIdDto>().ReverseMap();
        }
    }
}
