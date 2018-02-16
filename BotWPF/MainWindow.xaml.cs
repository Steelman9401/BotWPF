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
            Urls = rep.GetUrls();
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
            IEnumerable<HtmlNode> liCountry = document.DocumentNode.SelectNodes("//ul[@id='block_hottest_videos_by_country']").First().ChildNodes.ToList();
            IEnumerable<HtmlNode> liRecent = document.DocumentNode.SelectNodes("//ul[@id='most_recent_videos']").First().ChildNodes.ToList();
            IEnumerable<HtmlNode> liComb = liCountry.Concat(liRecent).Where(x=>x.Name=="li");
            foreach (HtmlNode item in liComb)
            {
                var a = item.ChildNodes[1].ChildNodes[3];
                if (a.Name == "a")
                {
                    var Url = a.GetAttributeValue("href", string.Empty).Substring(1);
                    if (!IfExists("https://embed.redtube.com/?id=" + Url))
                    {
                        VideoDTO video = new VideoDTO();
                        video.Url = Url;
                        video.Title = a.ChildNodes[3].InnerHtml.Replace("  ", "").Substring(1);
                        video.Img = a.ChildNodes[1].ChildNodes[1].GetAttributeValue("data-thumb_url", string.Empty);
                        video.Preview = a.ChildNodes[1].ChildNodes[1].GetAttributeValue("data-mediabook", string.Empty);
                        VideoList.Add(video);
                    }
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
