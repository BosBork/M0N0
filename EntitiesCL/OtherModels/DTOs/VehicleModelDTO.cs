using EntitiesCL.EFModels;
using EntitiesCL.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntitiesCL.OtherModels.DTOs
{
    public class VehicleModelDTO
    {
        public int VehicleModelId { get; set; }

        public string Name { get; set; }

        public string Abrv { get; set; }

        public int VehicleMakeId { get; set; }

        //public VehicleMake VehicleMake { get; set; }

        #region Test_1
        //public string VehicleMakeName { get; set; }
        #endregion
    }
}
