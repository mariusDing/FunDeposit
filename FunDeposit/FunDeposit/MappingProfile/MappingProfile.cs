using AutoMapper;
using FunDeposit.Models;
using FunDeposit.ViewModels;
using System.Collections.Generic;

namespace FunDeposit.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DepositModel, DepositVM>();
        }
    }
}
