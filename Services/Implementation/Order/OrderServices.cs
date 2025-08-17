using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restaurant.utils;
using Restaurant.DTO;
using Restaurant.Persistence;
using Restaurant.Persistence.Entity;
using Restaurant.utils;

namespace Restaurant.Services{
    public class OrderServices:IOrderServices
    {
        private readonly AppDBContext appDBContext;
        private readonly ConvertString2Long convertString2Long;

        private readonly order_Status _orderStatus;
        public OrderServices(AppDBContext appDBContext1,ConvertString2Long convertString2Long1,order_Status orderStatus)
        {
            appDBContext = appDBContext1;
            convertString2Long = convertString2Long1;
            _orderStatus = orderStatus;
        }   
        public OrderResponse CreateOrder(OrderRequestForCreate orderRequest,string UserId)
        {
            Item? item = appDBContext.Items.Find(orderRequest.ItemId);
            if(item == null)
                return null;
            long Id = convertString2Long.ConvertId2Long(UserId);
            string status=_orderStatus.status_Order1();
            Order newOrder = new Order(
                Id,
                orderRequest.ItemId,
                orderRequest.Quantity,
                DateTime.Now,
                orderRequest.ReceiptDate,
                status,
                orderRequest.Quantity*item.Price
            );
            appDBContext.Order.Add(newOrder);
            appDBContext.SaveChanges();
            /*var orderResponse = (from u in appDBContext.User
            where u.Id == Id
            join ord in appDBContext.Order
            on u.Id equals ord.UserId
            where ord.Id == newOrder.Id
            join i in appDBContext.Items
            on ord.ItemId equals i.Id
            select new OrderResponse(
                ord.Id,
                i.Name,
                i.Category,
                i.Price,
                u.Name,
                u.Email,
                u.Address,
                ord.Quantity,
                ord.RequestDate,
                ord.ReceiptDate,
                ord.DeliveryCost
            )).FirstOrDefault();
            return orderResponse;*/
            var orderResponse1 = new OrderResponse(
                newOrder.Id,
                item.Name,
                item.Category,
                item.Price,
                orderRequest.Quantity,
                DateTime.Now,
                orderRequest.ReceiptDate,
                newOrder.DeliveryCost
            );
            return orderResponse1;
        }
        public List<OrderResponse> GetOrders(string UserId)
        {
            long Id = convertString2Long.ConvertId2Long(UserId);
            /*var orderResponse = (from u in appDBContext.User
            where u.Id == Id
            join ord in appDBContext.Order
            on u.Id equals ord.UserId
            join i in appDBContext.Items
            on ord.ItemId equals i.Id
            select new OrderResponse(
                ord.Id,
                i.Name,
                i.Category,
                i.Price,
                u.Name,
                u.Email,
                u.Address,
                ord.Quantity,
                ord.RequestDate,
                ord.ReceiptDate,
                ord.DeliveryCost
            )).ToList();
            return orderResponse;*/
            List<OrderResponse> orderResponse = appDBContext.Order.Select(order=>new OrderResponse(
                order.Id,
                order.Item.Name,
                order.Item.Category,
                order.Item.Price,
                order.Quantity,
                order.RequestDate,
                order.ReceiptDate,
                order.DeliveryCost
            )).ToList();
            return orderResponse;
        }
        public OrderResponse UpdateOrder(OrderRequest orderRequest,string UserId){
            Order? order = appDBContext.Order.Find(orderRequest.Id);
            Item? item = appDBContext.Items.Find(orderRequest.ItemId);
            if(order == null|| item == null)
                return null;
            long userId = convertString2Long.ConvertId2Long(UserId);
            /*var orderResponse = (from u in appDBContext.User
            where u.Id == userId
            join ord in appDBContext.Order
            on userId equals ord.UserId
            where ord.Id == order.Id
            join i in appDBContext.Items
            on orderRequest.ItemId equals i.Id
            select new OrderResponse(
                ord.Id,
                i.Name,
                i.Category,
                i.Price,
                u.Name,
                u.Email,
                u.Address,
                orderRequest.Quantity,
                ord.RequestDate,
                orderRequest.ReceiptDate,
                orderRequest.Quantity*i.Price
            )).FirstOrDefault();*/
            order.ItemId = orderRequest.ItemId;
            order.Quantity = orderRequest.Quantity;
            order.ReceiptDate = orderRequest.ReceiptDate;
            order.DeliveryCost = orderRequest.Quantity*item.Price;
            appDBContext.Order.Entry(order).State = EntityState.Modified;
            appDBContext.SaveChanges();
            var orderResponse1 = new OrderResponse(
                order.Id,
                item.Name,
                item.Category,
                item.Price,
                orderRequest.Quantity,
                DateTime.Now,
                orderRequest.ReceiptDate,
                order.DeliveryCost
            );
            return orderResponse1;
        }
        public bool DeleteOrder(long orderId){
            var order = appDBContext.Order.Find(orderId);
            if (order == null)
                return false;
            appDBContext.Order.Remove(order);
            appDBContext.SaveChanges();
            return true;
        }

        public OrderResponse CreateOrder()
        {
            throw new NotImplementedException();
        }

        public List<Order> GetOrders()
        {
            throw new NotImplementedException();
        }

        public OrderResponse UpdateOrder()
        {
            throw new NotImplementedException();
        }

        public OrderResponse DeleteOrder()
        {
            throw new NotImplementedException();
        }
    }
}