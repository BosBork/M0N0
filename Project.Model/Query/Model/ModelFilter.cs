using Project.Model.Common.Query.Model;

namespace Project.Model.Query.Model
{
    public class ModelFilter : IModelFilter
    {
        public string Name { get; set; }

        public int? MakeIdFilterSelected { get; set; }
    }
}
