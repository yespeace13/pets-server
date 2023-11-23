using AutoMapper;
using ModelLibrary.Model.Act;
using ModelLibrary.Model.Animal;
using ModelLibrary.Model.Authentication;
using ModelLibrary.Model.Contract;
using ModelLibrary.Model.Etc;
using ModelLibrary.Model.Organization;
using PetsServer.Auth.Authorization.Model;
using PetsServer.Domain.Act.Model;
using PetsServer.Domain.Animal.Model;
using PetsServer.Domain.Contract.Model;
using PetsServer.Domain.Locality.Model;
using PetsServer.Domain.Organization.Model;

namespace PetsServer.Infrastructure.Services
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

            CreateMap<LocalityModel, LocalityView>();

            // Контракт
            // Из модели во view
            CreateMap<ContractModel, ContractViewList>()
                .ForMember(dest => dest.Executor, opt => opt.MapFrom(src => src.Executor.NameOrganization))
                .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Client.NameOrganization));

            CreateMap<ContractContentModel, ContractContentView>();

            // Из модели во viewOne
            CreateMap<ContractModel, ContractViewOne>();

            CreateMap<ContractEdit, ContractModel>();

            CreateMap<ContractContentEdit, ContractContentModel>();

            // Акт
            // Из модели во view
            CreateMap<ActModel, ActViewList>()
                .AfterMap((src, dest) => dest.Executor = src.Executor.NameOrganization)
                .AfterMap((src, dest) => dest.Locality = src.Locality.Name)
                .AfterMap((src, dest) => dest.Contract = src.Contract.Number);

            // Из модели во viewOne
            CreateMap<ActModel, ActViewOne>();

            CreateMap<ActEdit, ActModel>();

            // Animal
            // Из модели во view
            CreateMap<AnimalModel, AnimalViewList>()
                .AfterMap((src, dest) =>
                {
                    if (!src.Sex.HasValue) dest.Sex = null;
                    else if (src.Sex.Value) dest.Sex = "Самка";
                    else if (!src.Sex.Value) dest.Sex = "Самец";
                });

            CreateMap<AnimalEdit, AnimalModel>();
        }
    }
}
