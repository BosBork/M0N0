using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common
{
    public static class PagedListAutoMapperExtension
    {
        public static async Task<PagedList<Td>> ToMappedPagedListAsync<Td>
            (this IQueryable<object> source, int pageNumber, int pageSize, IMapper _mapper) 
            where Td : class
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var result = new PagedList<object>(items.ToList(), count, pageNumber, pageSize);
            var mapped = _mapper.Map<PagedList<Td>>(result);
            return mapped;
        }
    }

    public class PagedList<T> : List<T>, IPagedList
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public int StartPage { get; private set; }
        public int EndPage { get; private set; }

        //prikazi po 10 e.g. 1 2 3 4 5 6 7 8 9 10
        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            var startPage = this.CurrentPage - 5; //e.g. current page 2 = -3
            var endPage = this.CurrentPage + 4; //current page 2 = 6

            #region Ako Prvih 5 ili Zadnjih 5
            if (startPage <= 0) //-3 <= 0 = true
            {
                endPage -= (startPage - 1); //10 https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/arithmetic-operators#unary-plus-and-minus-operators
                startPage = 1;
            }

            if (endPage > TotalPages) // 10 < 132
            {
                endPage = TotalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }
            #endregion

            StartPage = startPage; //1
            EndPage = endPage; //10

            AddRange(items);
        }

        public static async Task<PagedList<T>> ToPagedListAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            #region PageNumberLimiterTest
            //var totalPages = (int)Math.Ceiling(count / (double)pageSize);
            //PageNumberLimiterTest(ref pageNumber, ref count, ref totalPages);
            #endregion
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
        #region PageNumberLimiterTest
        //public static void PageNumberLimiterTest(ref int pageNumber, ref int count, ref int totalPages)
        //{
        //    if (pageNumber > totalPages || pageNumber < 1)
        //    {
        //        if (pageNumber > totalPages)
        //        {
        //            pageNumber = totalPages;
        //            return;
        //        }
        //        if (pageNumber < 1)
        //        {
        //            pageNumber = 1;
        //            return;
        //        }
        //    }
        //    else
        //    {
        //        return;
        //    }
        //    //pageNumber = (pageNumber > totalPages || pageNumber < 1) ? 1 : pageNumber;
        //    //just to show first page if pagenumber=overMAX or underMIN
        //}
        #endregion
    }
}
