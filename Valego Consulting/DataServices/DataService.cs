using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valego_Consulting.Model;

namespace Valego_Consulting.DataServices
{
    internal class DataService
    {
        public async Task<List<Announcement>> FetchBitMexAnnouncements()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync("https://www.bitmex.com/api/v1/announcement");
                var announcements = JsonConvert.DeserializeObject<List<Announcement>>(response);
                return announcements;
            }
        }

        public async Task StoreAnnouncements(List<Announcement> announcements)
        {
            using (AppDbContext db = new AppDbContext())
            {
                foreach (var announcement in announcements)
                {
                    var existingAnnouncement = await db.Announcements
                        .AsNoTracking()
                        .FirstOrDefaultAsync(a => a.Id == announcement.Id);

                    if (existingAnnouncement == null)
                    {
                        await db.Announcements.AddAsync(announcement);
                    }
                }

                await db.SaveChangesAsync();
            }
        }

        
    }
}
