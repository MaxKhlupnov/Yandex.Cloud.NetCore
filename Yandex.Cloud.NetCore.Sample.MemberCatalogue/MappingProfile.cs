using System;
using System.Collections.Generic;
using Yandex.Cloud.NetCore.Sample.MemberCatalogue.Models;
using Yandex.Cloud.NetCore.Sample.Common.Models;
using AutoMapper;

namespace Yandex.Cloud.NetCore.Sample.MemberCatalogue
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<RegistrationModel, Member>();
        }
    }
}
