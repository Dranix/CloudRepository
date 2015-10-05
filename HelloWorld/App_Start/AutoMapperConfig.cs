using AutoMapper;
using HelloWorld.Models;
using HelloWorld.Models.ViewModels;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Drawing;

namespace HelloWorld.App_Start
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<ServiceCategoryVM, ServiceCategory>().ReverseMap();

            Mapper.CreateMap<ServiceProviderVM, ServiceProvider>().ReverseMap();

            Mapper.CreateMap<Service, ServiceVM>()
                .ForMember(dest => dest.ServiceLogo, op => op.ResolveUsing<ToServiceLogo>().FromMember(x => x.ServiceLogo)).ReverseMap();

            Mapper.CreateMap<OperationVM, Operation>().ReverseMap();

            Mapper.CreateMap<ServiceVersion, ServiceVersionVM>()
                .ForMember(dest => dest.Version, op => op.MapFrom(x => x.Service.ServiceName + " " + x.Version)).ReverseMap();

            Mapper.CreateMap<ServiceLogVM, ServiceLog>().ReverseMap();

            Mapper.CreateMap<EnterpriseApplication, EnterpriseApplicationVM>()
                .ForMember(dest => dest.UsingServices, op => op.ResolveUsing<ToUsingServices>().FromMember(x => x.Workflows))
                .ForMember(dest => dest.ApplicationLogo, op => op.ResolveUsing<ToServiceLogo>().FromMember(x => x.ApplicationLogo));
            Mapper.CreateMap<EnterpriseApplicationVM, EnterpriseApplication>();

            Mapper.CreateMap<ParameterVM, Parameter>().ReverseMap();
        }
    }

    public class ToUsingServices : ValueResolver<ICollection<Workflow>, Collection<Tuple<int, string>>>
    {
        protected override Collection<Tuple<int, string>> ResolveCore(ICollection<Workflow> source)
        {
            Collection<Tuple<int, string>> result = new Collection<Tuple<int, string>>();
            foreach (var wf in source)
            {
                Tuple<int, string> tuple = new Tuple<int, string>(wf.ServiceVersionId, String.Format("{0} {1}", wf.ServiceVersion.Service.ServiceName, wf.ServiceVersion.Version));
                result.Add(tuple);
            }

            return result;
        }
    }

    public class ToServiceLogo : ValueResolver<byte[], string>
    {
        protected override string ResolveCore(byte[] source)
        {
            if (source == null)
            {
                Bitmap image = new Bitmap(Resources.Resources.NoImage);
                byte[] imageByte = (byte[])((new ImageConverter()).ConvertTo(image, typeof(byte[])));
                return Convert.ToBase64String(imageByte);
            }
            else
            {
                return Convert.ToBase64String(source);
            }
        }
    }
}