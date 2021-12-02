using EntitiesCL.EFModels;
using EntitiesCL.OtherModels.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntitiesCL.OtherModels.Query
{
    public class MakeParams : QueryStringParamsBase
    {
        public MakeParams()
        {
            OrderBy = "name";
        }

        public string First { get; set; }/* = "All";*/

        public string Name { get; set; }

        //public string Sort { get; set; } = "name asc";
    }
}
