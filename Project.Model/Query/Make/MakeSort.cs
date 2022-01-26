using Project.Model.Common.Query.Make;

namespace Project.Model.Query.Make
{
    public class MakeSort : IMakeSort
    {
        public string OrderBy { get; set; } = "name desc";
    }
}
