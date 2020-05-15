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
using WpfClient.Model;
using WpfClient.Services;

namespace WpfClient
{
	public partial class MainWindow : Window
	{
		private readonly PostService _postService = new PostService();
		private List<Post> _posts = new List<Post>();

		public MainWindow()
		{
			InitializeComponent();
			_posts = _postService.GetAllPost();
			UpdateListView();
		}

		private async void Button_Click(object sender, RoutedEventArgs e)
		{
			var button = (sender as Button);
			var id = Convert.ToInt32(button.Tag.ToString());
			var post = _posts.FirstOrDefault(x => x.Id == id);

			await _postService.FeedbackAsync(id, !post.Like);
			_posts.FirstOrDefault(x => x.Id == id).Like = !post.Like;

			button.Background = post.Like ? Brushes.Green : Brushes.OrangeRed;
			button.Name = post.Like ? "Like" : "Dislike";
			button.Content = post.Like ? "Like" : "Dislike";
		}

		private void UpdateListView()
		{
			this.postsList.Items.Clear();
			foreach (var post in _posts)
			{
				this.postsList.Items.Add(GetPostData(post));
				var title = post.Like ? "Like" : "Dislike";
				var color = post.Like ? Brushes.Green : Brushes.OrangeRed;
				Button likeButton = new Button() { Name = title, Width = 50, Height = 20, Content = title, Tag = post.Id, Background = color };
				likeButton.Click += new RoutedEventHandler(Button_Click);
				this.postsList.Items.Add(likeButton);
			}
		}

		private string GetPostData(Post post)
		{
			return string.Format("------------------------------------------------------------------------------------------------ №{0} ------------------------------------------------------------------------------------------------ \n Title: {1} \n Author: {2} \n Date: {3} \n Type: {4} \n Topics: {5} \n Views: {6}",
						post.Id,
						post.Title,
						post.Author,
						post.Date,
						post.Type,
						post.Topics,
						post.NumberOfViews);
		}
	}
}
