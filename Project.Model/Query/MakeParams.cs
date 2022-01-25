namespace Project.Model.Query
{
    public class MakeParams : PagingParamsBase
    {
        public MakeSort MakeSort { get; set; } = new MakeSort();
        public MakeFilter MakeFilter { get; set; }
    }

    public class MakeSort
    {
        public string OrderBy { get; set; } = "name desc";
    }

    public class MakeFilter
    {
        public string Name { get; set; }
    }
}
