using Buma.Domain.Enums;
using Buma.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buma.Domain.Infrastructure
{
    public interface IOrderManager
    {
        bool OrderRefernceExists(string reference);

        IEnumerable<TResult> GetOrdersByStatus<TResult>(OrderStatus atatus, Func<Order, TResult> selector);
        TResult GetOrderById<TResult>(int id, Func<Order, TResult> selector);
        TResult GetOrderByReference<TResult>(string reference, Func<Order, TResult> selector);

        Task<int> CreateOrder(Order order);
        Task<int> AdvanceOrder(int id);
    }
}
 