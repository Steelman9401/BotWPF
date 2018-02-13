using BotWPF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotWPF.Repositories
{
    public class PornRepository
    {
        public void AddPorn(Video video, List<Category> cat)
        {
            using (var db = new myDb())
            {
                foreach(Category item in cat)
                {
                    Category tag = db.Categories
                  .Where(x => x.Name == item.Name)
                  .FirstOrDefault();
                    if (tag == null)
                    {
                        video.Categories.Add(item);
                    }
                    else
                    {
                        video.Categories.Add(tag);
                    }
                }
                db.Videos.Add(video);
                db.SaveChanges();

            }
        }
        public IEnumerable<string> GetTitles()
        {
            using (var db = new myDb())
            {
                return db.Videos.OrderByDescending(p=>p.Id)
                    .Select(x => x.Url).Take(100).ToList();
                    
            }
        }

        public List<CategoryDTO> GetCategories()
        {
            using (var db = new myDb())
            {
                return db.Categories
                     .Select(a => new CategoryDTO()
                     {
                         Id = a.Id,
                         Name = a.Name
                     }).ToList();
            }
        }
    }
}
