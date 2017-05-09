using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace Assignment5
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            // Add map creation statements here
            // Mapper.CreateMap< FROM , TO >();

            // Disable AutoMapper v4.2.x warnings
#pragma warning disable CS0618

            // Attention 1 - AutoMapper create map statements

            /*
            Mapper.CreateMap<Models.Customer, Controllers.CustomerBase>();
            Mapper.CreateMap<Controllers.CustomerAdd, Models.Customer>();

            // Attention 2 - This automapper is used to show an employee name rather than plain support rep ID when doing a GET ALL
            Mapper.CreateMap<Models.Customer, Controllers.CustomerWithEmployeeName>();

            // Attention 3 - This automapper is used to show a customer name rather than plain customer ID when doing a GET ALL
            Mapper.CreateMap<Models.Invoice, Controllers.InvoiceWithCustomerName>();

            // Attention 4 - Automapper for link to match the Ids
            Mapper.CreateMap<Controllers.CustomerWithEmployeeName, Controllers.CustomerWithLink>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CustomerId));
            Mapper.CreateMap<Controllers.InvoiceWithCustomerName, Controllers.InvoiceWithLink>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InvoiceId));*/

            // Attention 16 - Customer create map statements

            Mapper.CreateMap<Models.Customer, Controllers.CustomerBase>();
            Mapper.CreateMap<Models.Customer, Controllers.CustomerWithData>();

            Mapper.CreateMap<Controllers.CustomerBase, Controllers.CustomerWithLink>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CustomerId));
            Mapper.CreateMap<Controllers.CustomerWithData, Controllers.CustomerWithLink>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CustomerId));

            Mapper.CreateMap<Controllers.CustomerAdd, Models.Customer>();

            // Attention 43 - Invoice create map statements

            Mapper.CreateMap<Models.Invoice, Controllers.InvoiceWithData>();

            Mapper.CreateMap<Controllers.InvoiceWithData, Controllers.InvoiceWithLink>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InvoiceId));

#pragma warning restore CS0618
        }
    }
}