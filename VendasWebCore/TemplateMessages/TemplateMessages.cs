using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendasWebCore.DefaultMessages
{
    public static class TemplateMessages
    {
        #region password mail
        public const string PasswordChangeTitle = "Password Change";
        public const string EmailPasswordRetrieveChange = "Hello {0}! Your new password is {1}";
        public const string EmailPasswordChange = "Hello {0}! Your email has been changed";
        #endregion

        #region Order
        public const string OrderCreatedTitle = "Order Notification";
        public const string OrderCreated = "Hello {0}! Your order has been created. Your order id is {1}";
        #endregion        
    }
}
