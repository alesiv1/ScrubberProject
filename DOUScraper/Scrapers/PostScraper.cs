using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using DOUScraper.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DOUScraper.Scrapers
{
	public class PostScraper
	{
		private string _baseSiteUrl = "https://dou.ua/lenta/";

		public List<PostInfo> GetNewsOnPage(int pageNumber = 0)
		{
			IHtmlDocument document = ScrapeWebPage(pageNumber).Result;
			List<PostInfo> posts = new List<PostInfo>();
			IEnumerable<IElement> topics = document.All.Where(x => x.ClassName == "b-postcard ");
			foreach (var topic in topics)
			{
				posts.Add(GetPost(topic));
			}
			return posts;
		}

		#region Private Methida
		private async Task<IHtmlDocument> ScrapeWebPage(int pageNumber = 0)
		{
			var parameters = "";
			if (pageNumber > 0)
			{
				parameters = $"page/{pageNumber}/";
			}
			CancellationTokenSource cancellationToken = new CancellationTokenSource();
			HttpClient httpClient = new HttpClient();
			HttpResponseMessage request = await httpClient.GetAsync(_baseSiteUrl + parameters);
			cancellationToken.Token.ThrowIfCancellationRequested();

			Stream response = await request.Content.ReadAsStreamAsync();
			cancellationToken.Token.ThrowIfCancellationRequested();

			HtmlParser parser = new HtmlParser();
			IHtmlDocument document = parser.ParseDocument(response);
			return document;
		}
		private PostInfo GetPost(IElement element)
		{
			PostInfo post = new PostInfo();
				post.Title = GetTitle(ref element);
				post.Author = GetAuthor(ref element);
				post.Url = GetUrl(ref element);
				post.Description = GetDescription(ref element);
				post.Date = GetDate(ref element);
				post.Type = GetType(ref element);
				post.Topics = GetTopics(ref element);
				post.NumberOfViews = GetNumberOfViews(ref element);
			return post;
		}
		private string GetTitle(ref IElement element)
		{
			var title = element.Children
				.FirstOrDefault(x => x.ClassName == "title")
				.TextContent
				.Replace("\n", "")
				.Replace("\t","");
			return title;
		}
		private string GetAuthor(ref IElement element)
		{
			var author = element.Children
				.FirstOrDefault(x => x.ClassName == "b-info")
				.Children
				.FirstOrDefault(x => x.ClassName == "author")
				.TextContent;
			return author;
		}
		private string GetUrl(ref IElement element)
		{
			var innerHtml = element.Children
				.FirstOrDefault(x => x.ClassName == "title");
			var subString = innerHtml.InnerHtml.Substring(innerHtml.InnerHtml.IndexOf("\"") + 1);
			var url = subString
				.Substring(0, subString.IndexOf("\""));
			return url;
		}
		private string GetDescription(ref IElement element)
		{
			var description = element.Children
				.FirstOrDefault(x => x.ClassName == "b-typo")
				.TextContent
				.Replace("\n", "")
				.Replace("\t", "");
			var strDescription = description.Substring(0, description.LastIndexOf(".") + 1);
			return strDescription;
		}
		private string GetDate(ref IElement element)
		{
			var date = element.Children
				.FirstOrDefault(x => x.ClassName == "b-info")
				.Children
				.FirstOrDefault(x => x.ClassName == "date")
				.TextContent;
			return date;
		}
		private string GetType(ref IElement element)
		{
			var type = element.Children
				.FirstOrDefault(x => x.ClassName == "more")
				.Children
				.FirstOrDefault(x => x.ClassName == "topic")
				.TextContent;
			return type;
		}
		private string GetTopics(ref IElement element)
		{
			var topics = element.Children
				.FirstOrDefault(x => x.ClassName == "more")
				.Children
				.Where(x => x.ClassName == null)
				.Select(x => x.TextContent)
				.ToList();
			var strTopics = String.Join(", ", topics);
			return strTopics;
		}
		private int GetNumberOfViews(ref IElement element)
		{
			var number = 0;
			var strNumber = element.Children
				.FirstOrDefault(x => x.ClassName == "b-info")
				.Children
				.FirstOrDefault(x => x.ClassName == "pageviews")
				.TextContent;
			Int32.TryParse(strNumber, out number);
			return number;
		}
		#endregion
	}
}
