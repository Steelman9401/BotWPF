using BotWPF.Data;
using BotWPF.Repositories;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BotWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<VideoDTO> VideoList { get; set; } = new List<VideoDTO>();
        public IEnumerable<string> Urls { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            PornRepository rep = new PornRepository();
            Urls = rep.GetTitles();
            FillList();
            dataGridPorn.ItemsSource = VideoList;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            VideoDTO video = (VideoDTO)dataGridPorn.SelectedItem;
            if(video!=null)
            {
            VideoDetail w = new VideoDetail(video);
            if (w.ShowDialog() == true)
            {
                VideoList.Remove(video);
                dataGridPorn.ItemsSource = null;
                dataGridPorn.ItemsSource = VideoList;
            }
            }
        }
        public void FillList()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load("https://www.redtube.com/");
            var nodes = document.DocumentNode.SelectNodes("//a").Skip(48).Take(32).ToArray();
            foreach (HtmlNode item in nodes)
            {
                var children = item.ChildNodes;
                var title = children[3].InnerText.Replace("  ", "").Substring(1);
                var url = item.GetAttributeValue("href", string.Empty).Substring(1);
                if (!IfExists(url))
                {
                    var img = children[1].ChildNodes[1].GetAttributeValue("data-thumb_url", string.Empty);
                    VideoDTO video = new VideoDTO();
                    video.Url = url;
                    video.Title = title;
                    video.Img = img;
                    VideoList.Add(video);
                }
            }
        }

        private bool IfExists(string Url)
        {
            int count = Urls.Where(x => x == Url).Count();
            if (count != 0)
                return true;
            else
                return false;
        }

       
    }
}
