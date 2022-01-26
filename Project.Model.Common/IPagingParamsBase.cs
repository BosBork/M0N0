using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Model.Common
{
    public interface IPagingParamsBase
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
    }
}
