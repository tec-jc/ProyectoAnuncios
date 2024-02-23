using AdsProject.BussinessEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdsProject.DataAccessLogic
{
    public class AdImageDAL
    {
        public static async Task<int> CreateAsync(AdImage adImage)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                bdContexto.Add(adImage);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> UpdateAsync(AdImage adImage)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                var adImageDB = await bdContexto.AdImage.FirstOrDefaultAsync(s => s.Id == adImage.Id);
                if(adImageDB != null)
                {
                    adImageDB.IdAd = adImage.IdAd;
                    adImageDB.Path = adImage.Path;
                    bdContexto.Update(adImageDB);
                    result = await bdContexto.SaveChangesAsync();
                }                
            }
            return result;
        }

        public static async Task<int> DeleteAsync(AdImage adImage)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                var adImageDB = await bdContexto.AdImage.FirstOrDefaultAsync(s => s.Id == adImage.Id);
                if( adImageDB != null)
                {
                    bdContexto.AdImage.Remove(adImageDB);
                    result = await bdContexto.SaveChangesAsync();
                }                
            }
            return result;
        }

        public static async Task<AdImage> GetByIdAsync(AdImage adImage)
        {
            var adImageDB = new AdImage();
            using (var bdContexto = new ContextoDB())
            {
                adImageDB = await bdContexto.AdImage.FirstOrDefaultAsync(s => s.Id == adImage.Id);
            }
            return adImageDB;
        }

        public static async Task<List<AdImage>> GetAllAsync()
        {
            var images = new List<AdImage>();
            using (var bdContexto = new ContextoDB())
            {
                images = await bdContexto.AdImage.ToListAsync();
            }
            return images;
        }

        internal static IQueryable<AdImage> QuerySelect(IQueryable<AdImage> query, AdImage adImage)
        {
            if (adImage.Id > 0)
                query = query.Where(s => s.Id == adImage.Id);
            if (adImage.IdAd > 0)
                query = query.Where(s => s.IdAd == adImage.IdAd);
            if (!string.IsNullOrWhiteSpace(adImage.Path))
                query = query.Where(s => s.Path.Contains(adImage.Path));

            query = query.OrderByDescending(s => s.Id).AsQueryable();

            if (adImage.Top_Aux > 0)
                query = query.Take(adImage.Top_Aux).AsQueryable();

            return query;
        }

        public static async Task<List<AdImage>> SearchAsync(AdImage adImage)
        {
            var images = new List<AdImage>();
            using (var bdContexto = new ContextoDB())
            {
                var select = bdContexto.AdImage.AsQueryable();
                select = QuerySelect(select, adImage);
                images = await select.ToListAsync();
            }
            return images;
        }

        public static async Task<List<AdImage>> SearchIncludeAdAsync(AdImage adImage)
        {
            var images = new List<AdImage>();
            using (var bdContexto = new ContextoDB())
            {
                var select = bdContexto.AdImage.AsQueryable();
                select = QuerySelect(select, adImage).Include(s => s.Ad).AsQueryable();
                images = await select.ToListAsync();
            }
            return images;
        }
    }
}
