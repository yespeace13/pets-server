﻿using AutoMapper;
using ModelLibrary.Model.Organization;
using PetsServer.Organization.Model;

namespace PetsServer
{
    public class AutoMapper : Profile
    {
        /**
         * Автомаппер для классов
         */
        public AutoMapper()
        {
            // Из модели во view
            CreateMap<OrganizationModel, OrganizationViewList>()
                .ForMember(dest => dest.TypeOrganization, opt => opt.MapFrom(src => src.TypeOrganization.Name))
                .ForMember(dest => dest.LegalType, opt => opt.MapFrom(src => src.LegalType.Name))
                .ForMember(dest => dest.Locality, opt => opt.MapFrom(src => src.Locality.Name));

            // Из wiew для создания/изменения в модель
            CreateMap<OrganizationEdit, OrganizationModel>();
        }
    }
}
