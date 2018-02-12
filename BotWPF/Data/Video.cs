using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotWPF.Data
{
    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Desc { get; set; }
        public string Img { get; set; }
        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
