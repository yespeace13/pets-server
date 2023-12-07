using FluentValidation;
using PetsServer.Domain.Contract.Model;
using PetsServer.Domain.Contract.Repository;

namespace PetsServer.Domain.Contract.Validator;

public class ContractValidator : AbstractValidator<ContractModel>
{
    private ContractRepository _repository = new ContractRepository();
    private ContractContentRepository _contentRepository = new ContractContentRepository();

    public ContractValidator()
    {
        RuleFor(c => c.Number).NotNull().NotEmpty()
            .WithMessage("Номер контракта должен быть заполнен");
        RuleFor(c => c.ClientId).NotEqual(c => c.ExecutorId)
            .WithMessage("Исполнитель и заказчик не могут быть одинаковыми.");
        RuleFor(c => IsUnique(c)).Equal(true)
            .WithMessage("Контракт уже существует.");
        RuleFor(c => IsUniqueContentLocality(c)).Equal(true)
            .WithMessage("В содержимом повторяются населенные пункты.");
        RuleFor(c => IsUniqueContent(c)).Equal(true)
            .WithMessage("Содержимое контракта уже существует.");

    }

    private bool IsUniqueContentLocality(ContractModel contract)
    {
        var contractContent = contract.ContractContent;
        if (contractContent == null) return true;
        return !contractContent.GroupBy(cc => cc.LocalityId).Any(g => g.Count() > 1);
    }

    private bool IsUniqueContent(ContractModel contract)
    {
        var contractContent = contract.ContractContent;
        if (contractContent == null) return true;
        foreach (var content in contractContent)
        {
            return !_contentRepository.GetAll().Any(
                c => c.LocalityId == content.LocalityId
                    && c.ContractId == contract.Id
                    && (content.Id == 0 || c.Id != content.Id)
                    );
        }
        return true;
    }

    private bool IsUnique(ContractModel contract)
    {
        return !_repository.GetAll().Any(
            c => c.Number == contract.Number
            && c.ClientId == contract.ClientId
            && c.ExecutorId == contract.ExecutorId
            && c.DateOfConclusion == contract.DateOfConclusion
            && (contract.Id == 0 || c.Id != contract.Id)
            );
    }
}
