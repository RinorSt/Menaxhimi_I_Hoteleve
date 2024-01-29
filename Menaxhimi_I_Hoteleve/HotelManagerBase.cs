using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menaxhimi_I_Hoteleve
{
    
    public abstract class HotelManagerBase : IHotelManager
    {
        protected Guest[] guests;
        protected string filePath;
        protected ConsoleColor[] colors = { ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Yellow, ConsoleColor.Cyan, ConsoleColor.Magenta };

        public HotelManagerBase(string filePath)
        {
            this.filePath = filePath;
            guests = new Guest[100]; 
            LoadGuestsFromFile();
        }

        public abstract void RegisterGuest();
        public abstract void CheckoutGuest();
        public abstract void ShowGuests();

        protected abstract void SaveGuestsToFile();
        protected abstract void LoadGuestsFromFile();

        protected ConsoleColor GetRandomColor()
        {
            Random random = new Random();
            return colors[random.Next(colors.Length)];
        }

        protected Guest FindGuestByRoomNumber(int numerDhomes)
        {
            foreach (var guest in guests)
            {
                if (guest != null && guest.NumerDhomes == numerDhomes)
                {
                    return guest;
                }
            }
            return null;
        }
    }
}
