using AutoMapper;
using HelloWorld.Models;
using HelloWorld.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.ObjectModel;

namespace HelloWorld.App_Start
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<ServiceCategoryVM, ServiceCategory>().ReverseMap();
            Mapper.CreateMap<ServiceProviderVM, ServiceProvider>().ReverseMap();
            Mapper.CreateMap<ServiceVM, Service>().ReverseMap();
            Mapper.CreateMap<OperationVM, Operation>().ReverseMap();
            Mapper.CreateMap<ServiceVersionVM, ServiceVersion>().ReverseMap();
            Mapper.CreateMap<ServiceLogVM, ServiceLog>().ReverseMap();
            Mapper.CreateMap<EnterpriseApplication, EnterpriseApplicationVM>().
                ForMember(dest => dest.UsingServices, op =>
                        op.ResolveUsing<CustomConvert>()
                            .FromMember(x => x.Workflows));
            Mapper.CreateMap<EnterpriseApplicationVM, EnterpriseApplication>();
        }
    }

    public class CustomConvert : ValueResolver<ICollection<Workflow>, Collection<string>>
    {
        protected override Collection<string> ResolveCore(ICollection<Workflow> source)
        {
            Collection<string> result = new Collection<string>();
            foreach(var wf in source)
            {
                result.Add(String.Format("{0} {1}", wf.ServiceVersion.Service.ServiceName, wf.ServiceVersion.Version));
            }

            return result;
        }
    }
}