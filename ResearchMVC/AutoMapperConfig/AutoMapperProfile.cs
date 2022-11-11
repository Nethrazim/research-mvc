using AutoMapper;
using ResearchMVC.DTOs;
using ResearchMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResearchMVC.AutoMapperConfig
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Person, PersonDTO>()
                .ForMember(dst => dst.ContactType, src => src.MapFrom(x => x.BusinessEntityContacts != null ? x.BusinessEntityContacts.FirstOrDefault().ContactType : null))
                .ForMember(dst => dst.ContactTypeDescription, x => x.Ignore());
            CreateMap<PersonDTO, Person>();
            CreateMap<PersonPhone, PersonPhoneDTO>();
            CreateMap<EmailAddress, EmailAddressDTO>();
            CreateMap<Address, AddressDTO>();
            CreateMap<AddressType, AddressTypeDTO>();
            CreateMap<BusinessEntityAddress, BusinessEntityAddressDTO>();
            CreateMap<BusinessEntity, BusinessEntityDTO>();
            CreateMap<CountryRegion, CountryRegionDTO>();
            CreateMap<BusinessEntityContact, PersonDTO>()
                .ForMember(source => source.FirstName, dst => dst.MapFrom(x => x.Person.FirstName))
                .ForMember(source => source.MiddleName, dst => dst.MapFrom(x => x.Person.MiddleName))
                .ForMember(source => source.LastName, dst => dst.MapFrom(x => x.Person.LastName))
                .ForMember(source => source.AdditionalContactInfo, dst => dst.MapFrom(x => x.Person.AdditionalContactInfo))
                .ForMember(source => source.EmailPromotion, dst => dst.MapFrom(x => x.Person.EmailPromotion))
                .ForMember(source => source.PersonType, dst => dst.MapFrom(x => x.Person.EmailPromotion))
                .ForMember(source => source.Title, dst => dst.MapFrom(x => x.Person.Title))
                .ForMember(source => source.ContactType, dst => dst.MapFrom(x => x.ContactType));
            CreateMap<Person, SimplePersonDTO>();
            CreateMap<SimplePersonDTO, PersonDTO>().ReverseMap();
            CreateMap<ContactType, ContactTypeDTO>();
            CreateMap<BusinessEntityContact, BusinessEntityContactDTO>();

        }
    }
}