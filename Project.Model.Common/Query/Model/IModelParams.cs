using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Model.Common.Query.Model
{
    public interface IModelParams : IPagingParamsBase, IModelFilter, IModelSort
    {
    }
}
