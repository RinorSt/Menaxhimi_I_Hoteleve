using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menaxhimi_I_Hoteleve
{
    
    public interface IHotelManager
    {
        void RegisterGuest();
        void CheckoutGuest();
        void ShowGuests();
    }
}
