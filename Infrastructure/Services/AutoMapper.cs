using AutoMapper;
using ModelLibrary.Model.Act;
using ModelLibrary.Model.Animal;
using ModelLibrary.Model.Authentication;
using ModelLibrary.Model.Contract;
using ModelLibrary.Model.Etc;
using ModelLibrary.Model.Organization;
using ModelLibrary.Model.LogInformation;
using ModelLibrary.Model.Plan;
using ModelLibrary.Model.Report;
using PetsServer.Auth.Authorization.Model;
using PetsServer.Domain.Act.Model;
using PetsServer.Domain.Contract.Model;
using PetsServer.Domain.Locality.Model;
using PetsServer.Domain.Log.Model;
using PetsServer.Domain.Organization.Model;
using PetsServer.Domain.Plan.Model;
using PetsServer.Domain.Report.Model;

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

            // Auth
            CreateMap<EntityPossibilities, UserPossibilities>();

            // Report
            CreateMap<ReportModel, ReportViewList>();

            CreateMap<ReportContentModel, ReportContentView>()
                .ForMember(dest => dest.Locality, opt => opt.MapFrom(src => src.Locality.Name));

            CreateMap<ReportModel, ReportViewOne>();

            // Plan
            CreateMap<PlanModel, PlanViewList>()
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.StatusName));

            CreateMap<PlanContentModel, PlanContentView>();

            CreateMap<PlanModel, PlanViewOne>()
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.StatusName));

            CreateMap<PlanEdit, PlanModel>();

            CreateMap<PlanContentEdit, PlanContentModel>();

            CreateMap<LogModel, LogViewList>()
                .ForMember(dest => dest.UserLastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.UserFirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.UserPatronymic, opt => opt.MapFrom(src => src.User.Patronymic))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.Phone))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.NameOrganization, opt => opt.MapFrom(src => src.User.Organization.NameOrganization))
                .ForMember(dest => dest.StructuralUnit, opt => opt.MapFrom(src => src.User.Department))
                .ForMember(dest => dest.Post, opt => opt.MapFrom(src => src.User.Position))
                .ForMember(dest => dest.WorkPhoneNumber, opt => opt.MapFrom(src => src.User.Organization.Phone))
                .ForMember(dest => dest.WorkEmail, opt => opt.MapFrom(src => src.User.Organization.Email))
                .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.User.Login))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.EntityDescription));
        }
    }
}
