using AutoMapper;
using Project.DAL.DataAccess;
using Project.Repository.Common.Interfaces;
using Project.Repository.Common.Interfaces.UOW;
using Project.Repository.Repo;
using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class ServicesWrapper : IServicesWrapper
    {
        private readonly IRepoWrapper _repoWrapper;
        public ServicesWrapper(IRepoWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }

        private IMakeService makeService;
        public IMakeService VehicleMake
        {
            get
            {
                if (makeService == null)
                {
                    makeService = new MakeService(_repoWrapper.VehicleMake);
                }
                return makeService;
            }
            set { makeService = value; }
        }

        private IModelService modelService;
        public IModelService VehicleModel
        {
            get
            {
                if (modelService == null)
                {
                    modelService = new ModelService(_repoWrapper.VehicleModel);
                }
                return modelService;
            }
            set { modelService = value; }
        }

        //public async Task SaveAsync()
        //{
        //    await _repoWrapper.SaveAsync();
        //}

    }
}
