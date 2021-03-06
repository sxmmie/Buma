﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buma.Domain.Models
{
    // See product that links to the order
    public class OrderStock
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int StockId { get; set; }    // Link to the stock
        public Stock Stock { get; set; }

        public int Qty { get; set; }    // how much of the product have they purchased
    }
}
