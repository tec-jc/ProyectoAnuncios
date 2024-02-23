using AdsProject.BussinessEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdsProject.DataAccessLogic
{
    public class CategoryDAL
    {
        public static async Task<int> CreateAsync(Category category)
        {
            int result = 0;
            using(var bdContexto = new ContextoDB())
            {
                bdContexto.Category.Add(category);
                result = await bdContexto.SaveChangesAsync();
            }

            return result;
        }

        public static async Task<int> UpdateAsync(Category category)
        {
            int result = 0;
            using(var bdContexto = new ContextoDB())
            {
                var categoryDB = await bdContexto.Category.FirstOrDefaultAsync(c => c.Id  == category.Id);
                if (categoryDB != null)
                {
                    categoryDB.Name = category.Name;
                    bdContexto.Update(categoryDB);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }
        }

        public static async Task<int> DeleteAsync(Category category)
        {
            int result = 0;
            using(var bdContexto = new ContextoDB())
            {
                var categoryDB = await bdContexto.Category.FirstOrDefaultAsync(c => c.Id == category.Id);
                if (categoryDB != null)
                {
                    bdContexto.Category.Remove(categoryDB);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }
        }

        public static async Task<Category> GetByIdAsync(Category category)
        {
            var categoryDB = new Category();
            using(var bdContexto = new ContextoDB())
            {
                categoryDB = await bdContexto.Category.FirstOrDefaultAsync(c => c.Id == category.Id);
            }
            return categoryDB;
        }

        public static async Task<List<Category>> GetAllAsync()
        {
            var categories = new List<Category>();
            using(var bdContexto = new ContextoDB())
            {
                categories = await bdContexto.Category.ToListAsync();
            }
            return categories;
        }

        internal static IQueryable<Category> QuerySelect(IQueryable<Category> query,
            Category category)
        {
            if(category.Id > 0)
            {
                query = query.Where(c => c.Id == category.Id);
            }

            if(!string.IsNullOrWhiteSpace(category.Name))
            {
                query = query.Where(c => c.Name.Contains(category.Name));
            }

            query = query.OrderByDescending(c => c.Id);

            if(category.Top_Aux > 0)
            {
                query = query.Take(category.Top_Aux).AsQueryable();
            }

            return query;
        }

        public static async Task<List<Category>> SearchAsync(Category category)
        {
            var categories = new List<Category>();
            using(var bdContexto = new ContextoDB())
            {
                var select = bdContexto.Category.AsQueryable();
                select = QuerySelect(select, category);
                categories = await select.ToListAsync();
            }
            return categories;
        }
    }
}
