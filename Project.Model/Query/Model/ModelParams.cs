using Project.Model.Common.Query;
using Project.Model.Common.Query.Model;

namespace Project.Model.Query.Model
{
    public class ModelParams : PagingParamsBase, IModelParams
    {
        public string Name { get; set; }
        public string OrderBy { get; set; } = "name";
        public int? MakeIdFilterSelected { get; set; }

        //public ModelSort ModelSort { get; set; } = new ModelSort(); //default
        //public ModelFilter ModelFilter { get; set; }
    }
}
