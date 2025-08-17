using Microsoft.EntityFrameworkCore;
using restaurant.utils;
using Restaurant.DTO;
using Restaurant.Persistence;
using Restaurant.Persistence.Entity;
namespace Restaurant.Services
{
    public class ManagerOrderServices : IManagerServices
    {
        private readonly AppDBContext appDBContext;
        private readonly order_Status _orderStatus;
        public ManagerOrderServices(AppDBContext appDBContext1,order_Status orderStatus1)
        {
            appDBContext = appDBContext1;
            _orderStatus = orderStatus1;
        }
        public List<ManagerOrderResponse> MangerGet()
        {
            List<ManagerOrderResponse> orderResponse = appDBContext.Order.Select(order=>new ManagerOrderResponse(
                order.Id,
                order.Item.Name,
                order.Item.Category,
                order.Item.Price,
                order.Quantity,
                order.RequestDate,
                order.ReceiptDate,
                order.DeliveryCost,
                order.UserId,
                order.User.Name,
                order.User.Email,
                order.User.Phone,
                order.User.Address
            )).ToList();
            return orderResponse;
        }
        public List<ManagerOrderResponse> MangerGet(long Id)
        {
            List<ManagerOrderResponse> orderResponse = (from order in appDBContext.Order where order.UserId == Id select new ManagerOrderResponse(
                order.Id,
                order.Item.Name,
                order.Item.Category,
                order.Item.Price,
                order.Quantity,
                order.RequestDate,
                order.ReceiptDate,
                order.DeliveryCost,
                order.UserId,
                order.User.Name,
                order.User.Email,
                order.User.Phone,
                order.User.Address
            )).ToList();
            return orderResponse;
        }
        public OrderResponse? MangerCreate(ManagerOrderRequestForCreate orderRequest)
        {
            if(orderRequest == null)
                return null;
            User? User = (from u in appDBContext.User where u.Id == orderRequest.UserId select u).FirstOrDefault();
            Item? item = appDBContext.Items.Find(orderRequest.ItemId);

            string status=_orderStatus.status_Order1();

            if(User == null||item == null)
                return null;
            Order newOrder = new Order(
                User.Id,
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
            where u.Id == User.Id
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
        public OrderResponse? ManagerUpdate(OrderRequest orderRequest,long UserId)
        {
            Order? order = appDBContext.Order.Find(orderRequest.Id);
            Item? item = appDBContext.Items.Find(orderRequest.ItemId);
            if(order == null||item == null)
                return null;
            /*var orderResponse = (from u in appDBContext.User
            where u.Id == UserId
            join ord in appDBContext.Order
            on u.Id equals UserId
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
        public bool ManagerDelete(long orderId)
        {
            var order = appDBContext.Order.Find(orderId);
            if (order == null)
                return false;
            appDBContext.Order.Remove(order);
            appDBContext.SaveChanges();
            return true;
        }
        public void ManagerCreate()
        {
            throw new NotImplementedException();
        }
        public void ManagerDelete()
        {
            throw new NotImplementedException();
        }
        public void ManagerUpdate()
        {
            throw new NotImplementedException();
        }
    }
}