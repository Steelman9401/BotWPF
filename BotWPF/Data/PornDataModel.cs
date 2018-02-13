using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotWPF.Data
{
    public class PornDataModel
    {
        public BindingList<Video> _data { get; set; }
        public bool ContainsListCollection
        {
            get
            {
                return true;
            }
        }

        public IList GetList()
        {
            return _data;
        }
    }
}
