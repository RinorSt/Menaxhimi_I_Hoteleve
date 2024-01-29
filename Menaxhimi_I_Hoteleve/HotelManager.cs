using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menaxhimi_I_Hoteleve
{
    
    public class HotelManager : HotelManagerBase
    {
        private int lastIndex;

        public HotelManager(string filePath) : base(filePath)
        {
        }

        public override void RegisterGuest()
        {
            try
            {
                Console.Write("Shkruaj emrin : ");
                string emer = Console.ReadLine();

                Console.Write("Shkruaj mbiemrin : ");
                string mbiemer = Console.ReadLine();

                Console.Write("Shkruaj numerin e telefonit : ");
                string numerTelefoni = Console.ReadLine();

                Console.Write("Shkruaj numerin e dhomes: ");
                int numerDhomes = Convert.ToInt32(Console.ReadLine());

                if (numerDhomes < 1 || numerDhomes > 100)
                {
                    throw new HotelManagementException("Numri i dhomes eshte jashtë rangut te lejuar (1-100).");
                }

                
                if (guests.Any(guest => guest != null && guest.NumerDhomes == numerDhomes))
                {
                    throw new HotelManagementException($"Dhoma {numerDhomes} është tashmë e zënë.");
                }

                guests[lastIndex++] = new Guest
                {
                    Emri = emer,
                    Mbiemri = mbiemer,
                    NumerTelefoni = numerTelefoni,
                    NumerDhomes = numerDhomes
                };

                SaveGuestsToFile();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Guest {emer} {mbiemer} u regjistrua me sukses ne dhomen {numerDhomes}.");
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Format i pavlefshëm. Ju lutem shkruani një numër për dhomën.");
            }
            catch (HotelManagementException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Gabim: {ex.Message}");
            }
            finally
            {
                Console.ResetColor();
            }
        }

        public override void CheckoutGuest()
        {
            try
            {
                Console.Write("Shkruaj numrin e dhomes per te bere checkout: ");
                int numerDhomes = Convert.ToInt32(Console.ReadLine());

                Guest guestToCheckout = FindGuestByRoomNumber(numerDhomes);

                if (guestToCheckout != null)
                {
                    guests[Array.IndexOf(guests, guestToCheckout)] = null;
                    SaveGuestsToFile();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Guest {guestToCheckout.Emri} {guestToCheckout.Mbiemri} u largua me sukses.");
                }
                else
                {
                    throw new HotelManagementException($"Nuk u gjet guest me numer te dhomes {numerDhomes}.");
                }
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Format i pavlefshëm. Ju lutem shkruani një numër për dhomën.");
            }
            catch (HotelManagementException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Gabim: {ex.Message}");
            }
            finally
            {
                Console.ResetColor();
            }
        }

        public override void ShowGuests()
        {
            Console.WriteLine("Lista e guests te regjistruar:");

            foreach ( var guest in guests)
            {
                if (guest != null)
                {
                    Console.ForegroundColor = GetRandomColor();
                    Console.WriteLine($"{guest.Emri} {guest.Mbiemri} - Dhoma {guest.NumerDhomes}");
                    Console.ResetColor();
                }
            }
        }

        protected override void SaveGuestsToFile()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    foreach (var guest in guests)
                    {
                        if (guest != null)
                        {
                            sw.WriteLine($"\n\nEmri: {guest.Emri}\nMbiemri: {guest.Mbiemri}\nNumeri i telefonit: {guest.NumerTelefoni}\nNumeri i dhomes: {guest.NumerDhomes}\n\n");
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Gabim gjatë shkrimit në file: {ex.Message}");
            }
            finally
            {
                Console.ResetColor();
            }
        }

        protected override void LoadGuestsFromFile()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string[] lines = File.ReadAllLines(filePath);

                    foreach (var line in lines)
                    {
                        string[] parts = line.Split(',');

                        guests[lastIndex++] = new Guest
                        {
                            Emri = parts[0],
                            Mbiemri = parts[1],
                            NumerTelefoni = parts[2],
                            NumerDhomes = int.Parse(parts[3])
                        };
                    }
                }
            }
            catch (IOException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Gabim gjatë leximit nga file: {ex.Message}");
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Format i pavlefshëm në file. Kontrolloni të dhënat e file.");
            }
            finally
            {
                Console.ResetColor();
            }
        }
    }
}
