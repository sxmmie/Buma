using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buma.Domain.Infrastructure
{
    public static class DecimalExtensions
    {
        public static string GetValueString(this decimal value)
        {
            return $"$ {value.ToString("N2")}";
        }
    }
}
