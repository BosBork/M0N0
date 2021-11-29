using EntitiesCL.DataAccess;
using ServicesCL.Interfaces;
using ServicesCL.Interfaces.UOW;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesCL.Repo.UOW
{
    public class RepoWrapper : IRepoWrapper
    {
        private readonly ApplicationDbContext _context;
        private readonly IMakeRepo _make;
        private readonly IModelRepo _model;

        public IMakeRepo VehicleMake => _make ?? new MakeRepo(_context);
        public IModelRepo VehicleModel => _model ?? new ModelRepo(_context);

        #region test
        //public IMakeRepo VehicleMake => _make==null ? new MakeRepo(_context) : _make;
        //public IMakeRepo VehicleMake
        //{
        //    get
        //    {
        //        if (_make == null)
        //        {
        //            _make = new MakeRepo(_context);
        //        }
        //        return _make;
        //    }
        //}


        //public IModelRepo VehicleModel
        //{
        //    get
        //    {
        //        if (_model == null)
        //        {
        //            _model = new ModelRepo(_context);
        //        }
        //        return _model;
        //    }
        //} 
        #endregion

        public RepoWrapper(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
