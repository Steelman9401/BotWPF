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
        public Video video { get; set; }
        public int Index { get; set; }
        public List<CategoryDTO> Categories { get; set; }
        public VideoDetail(Video video)
        {
            InitializeComponent();
            this.video = video;
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
            HtmlDocument document = web.Load("https://www.redtube.com/" + video.Url);
            var category = document.DocumentNode.SelectNodes("//div").Where(x => x.InnerHtml == "Categories").First();
            var tagList = category.NextSibling.NextSibling.ChildNodes.Where(x => x.Name == "a");
            foreach (HtmlNode item in tagList)
            {
                lstBoxCat.Items.Add(item.InnerText);
            }
        }

        private void cmBoxCategories_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cmBoxCategories.IsDropDownOpen = true;
            //cmBoxCategories.ItemsSource = list.Select(p=> p.Title).Where(p => p.Contains(e.Text)).ToList();
        }

        private void btnRename_Click(object sender, RoutedEventArgs e)
        {
            lstBoxCat.Items[Index] = cmBoxRename.Text;
            cmBoxRename.Text = "";
        }

        private void btnAddCat_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddVideo_Click(object sender, RoutedEventArgs e)
        {
            video.Desc = new TextRange(txtBoxDesc.Document.ContentStart, txtBoxDesc.Document.ContentEnd).Text;
            video.Title = txtBoxName.Text;
            PornRepository rep = new PornRepository();
            List<Category> listCat = new List<Category>();
            foreach (string item in lstBoxCat.Items)
            {
                Category cat = new Category();
                cat.Name = item;
                listCat.Add(cat);
            }
            rep.AddPorn(video, listCat);
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
