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
        public RepoWrapper(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Test
        //private readonly IMakeRepo _make;
        //private readonly IModelRepo _model;

        //public IMakeRepo VehicleMake => _make ?? new MakeRepo(_context);
        //public IModelRepo VehicleModel => _model ?? new ModelRepo(_context);
        //public IMakeRepo VehicleMake => _make==null ? new MakeRepo(_context) : _make;  
        #endregion

        private IMakeRepo makeRepo;
        public IMakeRepo VehicleMake
        {
            get
            {
                if (makeRepo == null)
                {
                    makeRepo = new MakeRepo(_context);
                }
                return makeRepo;
            }
            //set { makeRepo = value; }
        }

        private IModelRepo modelRepo;
        public IModelRepo VehicleModel
        {
            get
            {
                if (modelRepo == null)
                {
                    modelRepo = new ModelRepo(_context);
                }
                return modelRepo;
            }
            //set { modelRepo = value; }
        }

        public void Save() => _context.SaveChanges();

    }
}
