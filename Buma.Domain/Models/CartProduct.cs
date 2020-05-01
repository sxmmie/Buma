﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buma.Domain.Models
{
    // List of cart product
    public class CartProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int StockId { get; set; }
        public int Qty { get; set; }
        public decimal Value { get; set; }
    }
}
