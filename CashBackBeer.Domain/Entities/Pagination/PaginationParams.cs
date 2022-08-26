using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashbackBeer.Domain.Pagination
{
    public class PaginationParams
    {
        public int Page { get; set; }
        public int Limit { get; set; }
        public int GetOffset() => (Page - 1) * Limit;
    }
}