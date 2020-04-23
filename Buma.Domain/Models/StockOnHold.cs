using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buma.Domain.Models
{
    public class StockOnHold
    {
        public int Id { get; set; }

        public int StockId { get; set; }
        public Stock Stock { get; set; }

        public int Qty { get; set; }
        public DateTimeOffset ExpiryDate { get; set; }
    }
}
