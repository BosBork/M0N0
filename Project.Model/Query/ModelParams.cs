namespace Project.Model.Query
{
    public class ModelParams : PagingParamsBase
    {
        public ModelSort ModelSort { get; set; } = new ModelSort(); //default
        public ModelFilter ModelFilter { get; set; }
    }

    public class ModelSort
    {
        public string OrderBy { get; set; } = "name"; //default
    }

    public class ModelFilter
    {
        public string Name { get; set; }

        public int? MakeIdFilterSelected { get; set; }
    }
}
