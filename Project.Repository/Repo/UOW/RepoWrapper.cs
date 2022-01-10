using Project.DAL.DataAccess;
using Project.DAL;
using Project.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Project.Repository.Common.Interfaces.UOW;
using Project.Repository.Common.Interfaces;
using AutoMapper;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Project.Repository.Repo.UOW
{
    public class RepoWrapper : IRepoWrapper
    {
        private readonly ISortHelper<VehicleMake> _makeSortHelper;
        private readonly ISortHelper<VehicleModel> _modelSortHelper;
        private readonly IMapper _mapper;

        private readonly ApplicationDbContext _context;
        public RepoWrapper(ApplicationDbContext context, ISortHelper<VehicleMake> makeSortHelper, ISortHelper<VehicleModel> modelSortHelper, IMapper mapper)
        {
            _context = context;
            _makeSortHelper = makeSortHelper;
            _modelSortHelper = modelSortHelper;
            _mapper = mapper;
        }

        private IMakeRepo makeRepo;
        public IMakeRepo VehicleMake
        {
            get
            {
                if (makeRepo == null)
                {
                    makeRepo = new MakeRepo(_context, _makeSortHelper, _mapper);
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
                    modelRepo = new ModelRepo(_context, _modelSortHelper, _mapper);
                }
                return modelRepo;
            }
            //set { modelRepo = value; }
        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        #region Test
        //public async Task<bool> FindIfExistsX<T>(Expression<Func<T, bool>> selector) where T : class
        //{
        //    var expression = _mapper.Map<Expression<Func<T, bool>>>(selector);
        //    var result = await _context.Set<T>().AnyAsync(expression);
        //    return result;
        //} 
        #endregion
        #region Test
        //public void Save() => _context.SaveChanges();

        //private readonly IMakeRepo _make;
        //private readonly IModelRepo _model;

        //public IMakeRepo VehicleMake => _make ?? new MakeRepo(_context);
        //public IModelRepo VehicleModel => _model ?? new ModelRepo(_context);
        //public IMakeRepo VehicleMake => _make==null ? new MakeRepo(_context) : _make;  
        #endregion
    }
}
