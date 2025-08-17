using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Restaurant.Persistence.Entity;

//using Restaurant.Persistence.Entity;
using Restaurant.Services.Interfaces;
using Restaurant.DTO;
using static restaurant.DTO.Payment.payment;
using restaurant.utils;
using Restaurant.Persistence;

namespace Restaurant.Services.Implementation
{
    public class manager_Payment : IManagerPaymentServices
    {
        
        private readonly AppDBContext _appDBContext;

        private readonly order_Status _orderStatus;

        public manager_Payment(AppDBContext appDBContext1){
            _appDBContext = appDBContext1;

            
        }

        public user_Payment_Responce create_Manager_Payment(payment_Request newUserPaymentRequest)
        {
             //long userId=(from user in _appDBContext.User where user.Name == newUserPaymentRequest.userName && user.Address == newUserPaymentRequest.userAddress select user.Id).FirstOrDefault();
             long userId=_appDBContext.Order.Find(newUserPaymentRequest.orderId).UserId;

             string status=_orderStatus.status_Order3();
              Payment newPayment=new Payment(

                newUserPaymentRequest.orderId,
                userId,
                //newUserPaymentRequest.userName,
                newUserPaymentRequest.userAddress,
                status,
                _appDBContext.Order.Find(newUserPaymentRequest.orderId).DeliveryCost
            );
             _appDBContext.Payment.Add(newPayment);
             _appDBContext.SaveChanges();
             return new user_Payment_Responce(
                newPayment.Id,
                newPayment.orderId,
                newPayment.userId,
              //  newPayment.userName,
                newPayment.userAddress,
                newPayment.orderStatus,
                newPayment.paymentCost
             );
        }

        public void delete_Manager_Payment(long id)
        {
            var payment=_appDBContext.Payment.Find(id);
            _appDBContext.Payment.Remove(payment);
            _appDBContext.SaveChanges();
            Console.WriteLine("successful deleted payment");
        }

        public List<Payment> get_Payments(long id)
        {
             List<Payment> paymentList=(from payments in _appDBContext.Payment where payments.Id==id select payments).ToList();
             if(paymentList is null){
                 return (List<Payment>)Results.NotFound("this user not found ");
            }
            return paymentList;
        }

        public user_Payment_Responce update_Manager_Payment(Payment updatedUserPaymentRequest)
        {
             var existedpayment = _appDBContext.Payment.Find(updatedUserPaymentRequest.Id);
            if (existedpayment is null)
            {
                return (user_Payment_Responce)Results.NotFound("this user not found ");
            }

            string status=_orderStatus.status_Order3();
            updatedUserPaymentRequest.orderStatus=status;
            _appDBContext.Payment.Update(updatedUserPaymentRequest);
            _appDBContext.SaveChanges();

            return new user_Payment_Responce(
                 updatedUserPaymentRequest.Id,
                 updatedUserPaymentRequest.orderId,
                 updatedUserPaymentRequest.userId,
              //   updatedUserPaymentRequest.userName,
                 updatedUserPaymentRequest.userAddress,
                 updatedUserPaymentRequest.orderStatus,
                 updatedUserPaymentRequest.paymentCost

            );
        }
    }
}