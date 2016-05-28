using FoodOrderingBuddy.Helpers.DatabaseHelpers;
using FoodOrderingBuddy.NotifyMeService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace FoodOrderingBuddy.Helpers.NotifyMeServiceWcfHelpers
{
    public class NotifyMeServiceWcfHelper
    {
        private IOrder order;

        public NotifyMeServiceWcfHelper()
        {
            order = new OrderHelper();
        }

        public bool SendaSnailMail(int orderId)
        {
            bool isSent = false;
            StringBuilder builderMessage = new StringBuilder();
            string emailtoSend = "generic@gmail.com";
            EmailResponse emailResponse = new EmailResponse();

            var currenOrder = order.GetOrderbyId(orderId);

            if (currenOrder != null)
            {
                NotifyMeService.NotifyMeServiceClient soapClient = new NotifyMeServiceClient();
                NotifyMeService.EmailRequest emailRequest = new EmailRequest();
                emailRequest.ClientId = "FoodOrderingBuddyMvc.001";
                emailRequest.DateCreated = DateTime.Now;
                emailRequest.EmailAddress = emailtoSend;
                emailRequest.Subject = "Food Ordering Buddy Receipt for Order Number: " + orderId.ToString();

                builderMessage.Append("Hi, ");
                builderMessage.Append("<br/><br/>");
                builderMessage.Append("Please find details for your order below: " + "<br/>");
                builderMessage.Append("Username: " + currenOrder.Username + "<br/>");
                builderMessage.Append("Order Date: " + currenOrder.DateCreated.ToString() + "<br/>");
                builderMessage.Append("Number of Menu Items: " + currenOrder.MenuItemsTotal.ToString() + "<br/>");
                builderMessage.Append("Total: " + currenOrder.Total.ToString() + "<br/><br/>");
                builderMessage.Append("Kind Regards, " + "<br/>");
                builderMessage.Append("The Food Buddies");

                emailRequest.Message = builderMessage.ToString();

                emailResponse = soapClient.NotifymeThankyoubyEmail(emailRequest);

                if (emailResponse.Success)
                {
                    isSent = true;

                    try
                    {
                        using (FoodOrderingBuddy.Data.FoodOrderingBuddyEntities entity = new Data.FoodOrderingBuddyEntities())
                        {
                            var request = CommonUtilityHelper.SerializeObject(emailRequest);
                            var response = CommonUtilityHelper.SerializeObject(emailResponse);

                            FoodOrderingBuddy.Data.NotifyMeService notifyMeService = new Data.NotifyMeService();
                            notifyMeService.OrderId = orderId;
                            notifyMeService.DateCreated = DateTime.Now;
                            notifyMeService.NotifiyMeRequest = request;
                            notifyMeService.NotifyMeResponse = response;

                            entity.NotifyMeServices.Add(notifyMeService);
                            entity.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        LogFilterHelper.Log(ex);
                    }
                }
            }
            else
            {
                isSent = false;
            }

            return isSent;
        }
    }
}