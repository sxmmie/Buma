using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buma.Application.Orders;
using Buma.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Buma.UI.Pages
{
    public class OrderModel : PageModel
    {
        public GetOrder.Response Order { get; set; }

        public void OnGet(string reference, [FromServices] GetOrder getOrder)
        {
            Order = getOrder.Do(reference);
        }
    }
}
