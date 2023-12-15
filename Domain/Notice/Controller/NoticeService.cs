
using PetsServer.Auth.Authorization.Model;
using PetsServer.Infrastructure.Context;

namespace PetsServer.Domain.Notice.Controller;

internal class NoticeService
{
    private readonly PetsContext _context = new();
    internal string? GetReportsNotice(UserModel user)
    {
        var role = user.Role?.Name;
        if (role == null) return null;
        if(role == "Оператор ОМСУ")
        {
            if (_context.Reports.Any(r => r.Status.StatusName == "Доработка"))
                return "У вас есть отчеты, которые требуют доработки";
        }
        if(role == "Куратор ОМСУ")
        {
            if (_context.Reports.Any(r => r.Status.StatusName == "Согласование у исполнителя Муниципального Контракта"
                    || r.Status.StatusName == "Утвержден у исполнителя Муниципального Контракта"))
                return "У вас есть отчеты, которые требуют проверки";
        }

        if (role == "Подписант ОМСУ")
        {
            if (_context.Reports.Any(r => r.Status.StatusName == "Согласован у исполнителя Муниципального Контракта"))
                return "У вас есть отчеты, которые требуют проверки";
        }
        return null;
    }
}