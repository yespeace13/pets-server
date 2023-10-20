using AutoMapper;
using ModelLibrary.Contract;
using ModelLibrary.Model.Authentication;
using ModelLibrary.Model.Organization;
using PetsServer.Authorization.Model;
using PetsServer.Contract.Model;
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
            // Организация
            // Из модели во view
            CreateMap<OrganizationModel, OrganizationViewList>()
                .ForMember(dest => dest.TypeOrganization, opt => opt.MapFrom(src => src.TypeOrganization.Name))
                .ForMember(dest => dest.LegalType, opt => opt.MapFrom(src => src.LegalType.Name))
                .ForMember(dest => dest.Locality, opt => opt.MapFrom(src => src.Locality.Name));

            // Из wiew для создания/изменения в модель
            CreateMap<OrganizationEdit, OrganizationModel>();

            // Пользователя
            // Из wiew для создания/изменения в модель
            CreateMap<UserEdit, UserModel>();

            // Контракт
            // Из модели во view
            CreateMap<ContractModel, ContractViewList>()
                .ForMember(dest => dest.Executor, opt => opt.MapFrom(src => src.Executor.NameOrganization))
                .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Client.NameOrganization));

            CreateMap<ContractContentModel, ContractContentView>();

            // Из модели во viewOne
            CreateMap<ContractModel, ContractViewOne>()
                .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Client))
                .ForMember(dest => dest.Executor, opt => opt.MapFrom(src => src.Executor));


        }
    }
}
