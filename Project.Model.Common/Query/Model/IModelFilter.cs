using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Model.Common.Query.Model
{
    public interface IModelFilter
    {
        string Name { get; set; }

        int? MakeIdFilterSelected { get; set; }
    }
}
