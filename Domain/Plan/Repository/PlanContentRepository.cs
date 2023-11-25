using Microsoft.EntityFrameworkCore;
using PetsServer.Domain.Organization.Model;
using PetsServer.Domain.Plan.Model;
using PetsServer.Infrastructure;
using PetsServer.Infrastructure.Context;
using System;

namespace PetsServer.Domain.Plan.Repository;

public class PlanContentRepository : BaseRepository<PlanContentModel>
{
    public PlanContentRepository() : base()
    {
    }

    public override PlanContentModel? Get(int id)
    {
        return _context.PlanContents
            .Where(c => c.Id == id)
            .Include(cc => cc.Locality)
            .Include(cc => cc.Act)
            .FirstOrDefault();
    }

    public override IQueryable<PlanContentModel> Get()
    {
        return _context.PlanContents
            .Include(cc => cc.Locality)
            .Include(cc => cc.Act);
    }



    //private PetsContext _context;

    //public PlanContentRepository()
    //{
    //    _context = new PetsContext();
    //}

    //public PlanContentModel? GetOne(int id)
    //{
    //    return _context.ContractContents
    //        .Where(c => c.Id == id)
    //        .Include(cc => cc.Locality)
    //        .Include(cc => cc.Act)
    //        .FirstOrDefault();
    //}

    //public void Create(PlanContentModel entity)
    //{
    //    _context.Add(entity);
    //    _context.SaveChanges();
    //}

    //public void Update(PlanContentModel entity)
    //{
    //    _context.Update(entity);
    //    _context.SaveChanges();
    //}

    //public void Delete(PlanContentModel entity)
    //{
    //    _context.Remove(entity);
    //    _context.SaveChanges();
    //}
}
