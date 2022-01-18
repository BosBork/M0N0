using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common
{
    public interface IPagedList //IPagedList samo za _FooterPagingPartial, radi i dynamic, vidit View Components
    {
        int CurrentPage { get; }
        int TotalPages { get; }
        bool HasPrevious { get; }
        bool HasNext { get; }
        int StartPage { get; }
        int EndPage { get; }
    }
}
