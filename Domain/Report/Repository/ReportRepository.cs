using Microsoft.EntityFrameworkCore;
using PetsServer.Domain.Act.Model;
using PetsServer.Domain.Report.Model;
using PetsServer.Infrastructure.Context;

namespace PetsServer.Domain.Report.Repository;

public class ReportRepository
{
    private readonly PetsContext _context = new();

    public ReportModel? Get(int id)
    {
        return _context.Reports
            .Include(r => r.ReportContent)
            .ThenInclude(r => r.Locality)
            .FirstOrDefault(r => r.Id == id);
    }

    public IQueryable<ReportModel> Get()
    {
        return _context.Reports;
    }

    public void Create(ReportModel report)
    {
        _context.Reports.Add(report);
        _context.SaveChanges();

    }
    public void Delete(ReportModel report)
    {
        _context.Remove(report);
        _context.SaveChanges();
    }

    public IQueryable<ActModel> GetActs()
    {
        return _context.Acts
        .Include(a => a.Animal)
        .Include(a => a.Contract)
        .ThenInclude(c => c.ContractContent);
    }
}
