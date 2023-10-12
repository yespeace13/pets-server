using IS_5.Organization.Repository;
using ModelLibrary.Model.Organization;
using PetsServer.Interfaces;
using PetsServer.Locality.Repository;

namespace IS_5.Organization.Model
{
    public class OrganizationMapper : IMapper<OrganizationModel, OrganizationViewList>
    {
        private static OrganizationRepository _organizationRepository = new OrganizationRepository();

        private static LocalityRepository _localityRepository = new LocalityRepository();

        public OrganizationViewList FromModelToView(OrganizationModel model)
        {
            return new OrganizationViewList(
                model.Id,
                model.NameOrganization,
                model.Inn,
                model.KPP,
                model.Address,
                model.TypeOrganization.Name,
                model.LegalType.Name,
                model.Locality?.Name
                );
        }

        public OrganizationModel FromViewToModel(OrganizationViewList view)
        {
            if (view.Id != 0)
            {
                var organization = _organizationRepository.GetOne(view.Id);
                organization.NameOrganization = view.NameOrganization;
                organization.Address = view.Address;
                organization.Inn = view.INN;
                organization.KPP = view.KPP;
                organization.TypeOrganization = _organizationRepository.GetTypeOrganization(view.TypeOrganization);
                organization.LegalType = _organizationRepository.GetLegalType(view.LegalType);
                organization.Locality = _localityRepository.GetOne(view.Locality);
                return organization;
            }
            else
                return new OrganizationModel(
                    view.Id,
                    view.NameOrganization,
                    view.INN,
                    view.KPP,
                    view.Address,
                    _organizationRepository.GetTypeOrganization(view.TypeOrganization),
                    _organizationRepository.GetLegalType(view.LegalType),
                    _localityRepository.GetOne(view.Locality)
                );
        }

        public OrganizationModel FromViewToModel(OrganizationViewEdit view)
        {
            return new OrganizationModel(
                0,
                view.NameOrganization,
                view.INN,
                view.KPP,
                view.Address,
                _organizationRepository.GetTypeOrganization(view.TypeOrganization),
                _organizationRepository.GetLegalType(view.LegalType),
                _localityRepository.GetOne(view.Locality)
            );
        }
    }
}
