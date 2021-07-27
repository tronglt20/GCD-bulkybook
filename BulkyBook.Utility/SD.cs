using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Utility
{
    public class SD
    {
        public const string Role_User_Indi = "Individual Customer";
        public const string Role_Admin = "Admin";

        public static double GetPriceBaseOnQuantity(double quantity, double price)
        {
            if (quantity<50)
            {
                return price;
            }
            else
            {
                return price;
            }
        }
    }
}
