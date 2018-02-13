using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BotWPF.Data
{
    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int Views { get; set; } = 0;
        public string Img { get; set; }
        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
