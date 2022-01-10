using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Model.OtherModels.Query
{
    public class ModelParams : QueryStringParamsBase
    {
        public ModelParams()
        {
          OrderBy = "name";
        }

        public string First { get; set; }

        public string Name { get; set; }

        public int? MakeIdFilterSelected { get; set; }
    }
}
