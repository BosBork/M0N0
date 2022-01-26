using Project.Model.Common.Query.Make;

namespace Project.Model.Query.Make
{
    public class MakeParams : PagingParamsBase, IMakeParams
    {
        public string Name { get; set; }
        public string OrderBy { get; set; } = "name desc";

        //public MakeSort MakeSort { get; set; } = new MakeSort();
        //public MakeFilter MakeFilter { get; set; }
    }
}
