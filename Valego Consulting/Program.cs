using Valego_Consulting.DataServices;
using Valego_Consulting.Model;

namespace Valego_Consulting
{
    class Program
    {
        static async Task Main(string[] args)
        {
            DataService dataServices = new DataService();

            List<Announcement> announcements = await dataServices.FetchBitMexAnnouncements();

            if (announcements != null)
            {
                await dataServices.StoreAnnouncements(announcements);
                Console.WriteLine("Anuncios almacenados en la base de datos.");
            }
            else
            {
                Console.WriteLine("No se pudieron obtener anuncios.");
            }

            


        }
    }
}


