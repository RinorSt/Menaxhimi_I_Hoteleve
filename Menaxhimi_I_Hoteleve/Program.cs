using System.Media;

namespace Menaxhimi_I_Hoteleve
{
    
    public class HotelManagementException : Exception
    {
        public HotelManagementException(string message) : base(message)
        {
        }
    }
    class Program 
    {
       

        static void Main()
        {

            PlayBackgroundMusic("example1.wav");

            static void PlayBackgroundMusic(string filePath)
            {
                try
                {
                    SoundPlayer player = new SoundPlayer(filePath);
                    player.PlayLooping();  
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error playing background music: {ex.Message}");
                }
            }



                IHotelManager hotelManager = new HotelManager("guests.txt");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green ;
                Console.WriteLine("\nZgjedhni opsionin:");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("1. Regjistro Guest");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("2. Checkout Guest");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("3. Shfaq Guests");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("4. Dil");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("Zgjedhja juaj: ");
                

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Format i pavlefshëm. Ju lutem shkruani një numër.");
                    Console.ResetColor();
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        hotelManager.RegisterGuest();
                        break;
                    case 2:
                        hotelManager.CheckoutGuest();
                        break;
                    case 3:
                        hotelManager.ShowGuests();
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Zgjedhja nuk eshte valide.");
                        Console.ResetColor();
                        break;
                }
            }
        }
    }

}