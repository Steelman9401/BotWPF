using BotWPF.Data;
using BotWPF.Repositories;
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
using System.Windows.Shapes;

namespace BotWPF
{
    /// <summary>
    /// Interaction logic for VideoDetail.xaml
    /// </summary>
    public partial class VideoDetail : Window
    {
        public VideoDTO Video { get; set; }
        public int Index { get; set; }
        public List<CategoryDTO> Categories { get; set; }
        public VideoDetail(VideoDTO video)
        {
            InitializeComponent();
            this.Video = video;
            txtBlockTitle.Text = video.Title;
            txtBlockTitle.TextAlignment = TextAlignment.Center;
            txtBoxDesc.Document.Blocks.Clear();
            webBrowser.Navigate("https://embed.redtube.com/?id=" + video.Url);
            LoadCategories();
            GetTags();
        }

        private void lstBoxCat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstBoxCat.SelectedItem != null)
            {
                cmBoxRename.Text = lstBoxCat.SelectedItem.ToString();
                Index = lstBoxCat.SelectedIndex;
            }
        }
        private void GetTags()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load("https://www.redtube.com/" + Video.Url);
            var category = document.DocumentNode.SelectNodes("//div").Where(x => x.InnerHtml == "Categories").FirstOrDefault();
            if (category != null)
            {
                var tagList = category.NextSibling.NextSibling.ChildNodes.Where(x => x.Name == "a");
                foreach (HtmlNode item in tagList)
                {
                    lstBoxCat.Items.Add(item.InnerText);
                }
            }
        }

        private void cmBoxCategories_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cmBoxCategories.IsDropDownOpen = true;
            cmBoxCategories.ItemsSource = Categories.Select(p => p.Name).Where(p => p.Contains(e.Text)).ToList();
        }

        private void btnRename_Click(object sender, RoutedEventArgs e)
        {
            lstBoxCat.Items[Index] = cmBoxRename.Text;
            cmBoxRename.Text = "";
        }

        private void btnAddCat_Click(object sender, RoutedEventArgs e)
        {
            lstBoxCat.Items.Add(cmBoxCategories.Text);
        }

        private void btnAddVideo_Click(object sender, RoutedEventArgs e)
        {
            Video video = new Video();
            video.Description = new TextRange(txtBoxDesc.Document.ContentStart, txtBoxDesc.Document.ContentEnd).Text;
            video.Title = txtBoxName.Text;
            video.Url = "https://embed.redtube.com/?id=" + Video.Url;
            video.Img = Video.Img;
            video.Date = DateTime.Now;
            PornRepository rep = new PornRepository();
            List<Category> listCat = new List<Category>();
            foreach (string item in lstBoxCat.Items)
            {
                Category cat = new Category();
                cat.Name = item;
                listCat.Add(cat);
            }
            rep.AddPorn(video, listCat);
            this.DialogResult = true;
            this.Close();
        }

        private void LoadCategories()
        {
            PornRepository rep = new PornRepository();
            Categories = rep.GetCategories();
        }

        private void cmBoxRename_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cmBoxRename.IsDropDownOpen = true;
            cmBoxRename.ItemsSource = Categories.Select(p=> p.Name).Where(p => p.Contains(e.Text)).ToList();
        }
    }
}
