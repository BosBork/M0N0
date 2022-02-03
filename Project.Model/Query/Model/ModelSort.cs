using Project.Model.Common.Query.Model;

namespace Project.Model.Query.Model
{
    public class ModelSort : IModelSort
    {
        public string OrderBy { get; set; } = "name"; //default
    }
}
