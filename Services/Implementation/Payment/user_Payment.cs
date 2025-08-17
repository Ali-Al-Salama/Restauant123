using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Restaurant.Persistence.Entity;
using Restaurant.Services.Interfaces;
using Restaurant.DTO;
using static restaurant.DTO.Payment.payment;
using restaurant.utils;
using Restaurant.Persistence;

namespace Restaurant.Services.Implementation
{
     
    public class user_Payment : IUserPaymentServices
    {
        private readonly AppDBContext _appDBContext;

        private readonly order_Status _orderStatus;

        public user_Payment(AppDBContext appDBContext1){
            _appDBContext = appDBContext1;
        }

        public user_Payment_Responce create_User_Payment(payment_Request new_User_Payment_Request)
        {
            //long userId=(from user in _appDBContext.User where user.Name == newUserPaymentRequest.userName && user.Address == newUserPaymentRequest.userAddress select user.Id).FirstOrDefault();
               long userId=_appDBContext.Order.Find(new_User_Payment_Request.orderId).UserId;
              
             string status=_orderStatus.status_Order2();
              Payment newPayment=new Payment(

                new_User_Payment_Request.orderId,
                userId,
              // newUserPaymentRequest.userName,
                new_User_Payment_Request.userAddress,
                 status,
                _appDBContext.Order.Find(new_User_Payment_Request.orderId).DeliveryCost
            );
             _appDBContext.Payment.Add(newPayment);
             _appDBContext.SaveChanges();
             return new user_Payment_Responce(
                newPayment.Id,
                newPayment.orderId,
                newPayment.userId,
               // newPayment.userName,
                newPayment.userAddress,
                newPayment.orderStatus,
                newPayment.paymentCost
             );
        }

        public void delete_User_Payment(long id)
        {
            var payment=_appDBContext.Payment.Find(id);
            _appDBContext.Payment.Remove(payment);
            _appDBContext.SaveChanges();
            Console.WriteLine("successful deleted payment");

        }

        public List<Payment> get_Payments(long id)
        {
            List<Payment> paymentList=[];
             long userId=_appDBContext.Payment.Find(id).userId;
            if(_appDBContext.User.Find(userId) != null){
                  paymentList=(from payments in _appDBContext.Payment where payments.Id==id select payments).ToList();
            }
               
             if(paymentList is null){
                 return (List<Payment>)Results.NotFound("this user not found ");
            }
            else{
                return paymentList;
            }
             
            
            
           
           
            
            
            
        }

        public user_Payment_Responce update_User_Payment(Payment updated_User_Payment)
        {
            var existedOrder = _appDBContext.Payment.Find(updated_User_Payment.Id);
            if (existedOrder is null)
            {
                return (user_Payment_Responce)Results.NotFound("this user not found ");
            }
            updated_User_Payment.orderStatus="Not-Receipt-Yet";
            _appDBContext.Payment.Update(updated_User_Payment);
            _appDBContext.SaveChanges();

            return new user_Payment_Responce(
                updated_User_Payment.Id,
                updated_User_Payment.orderId,
                updated_User_Payment.userId,
             //   updatedUserPayment.userName,
                updated_User_Payment.userAddress,
                updated_User_Payment.orderStatus,
                updated_User_Payment.paymentCost

            );


        }
    }
}