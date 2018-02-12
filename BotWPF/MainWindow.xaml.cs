using BotWPF.Data;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
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
        public List<Video> VideoList { get; set; } = new List<Video>();
        public MainWindow()
        {
            InitializeComponent();
            FillList();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {

        }
        public void FillList()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load("https://www.redtube.com/");
            var nodes = document.DocumentNode.SelectNodes("//a").Skip(47).Take(32).ToArray();
            foreach (HtmlNode item in nodes)
            {
                var children = item.ChildNodes;
                var title = children[3].InnerText.Replace("  ", "").Substring(1);
                var img = children[1].ChildNodes[1].GetAttributeValue("src", string.Empty);
                Video video = new Video();
                video.Url = item.GetAttributeValue("href", string.Empty).Substring(1);
                video.Title = title;
                video.Img = img;
                VideoList.Add(video);
                lstBoxPorn.Items.Add(title);
            }
        }

        private void lstBoxPorn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Video video = VideoList.Where(x => x.Title == lstBoxPorn.SelectedItem.ToString()).First();
            VideoDetail w = new VideoDetail(video);
            w.Show();
        }
    }
}
