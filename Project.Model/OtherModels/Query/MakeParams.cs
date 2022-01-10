namespace Project.Model.OtherModels.Query
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
