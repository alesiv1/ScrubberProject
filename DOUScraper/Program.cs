using DOUScraper.RabbitMQ;
using DOUScraper.Scrapers;
using System;
using System.Threading;

namespace DOUScraper
{
	public class Program
	{
		static void Main(string[] args)
		{
			PostScraper scraper = new PostScraper();
			RebbitMQManager manager = new RebbitMQManager();
			var data = scraper.GetNewsOnPage(1);
			foreach (var item in data)
			{
				try
				{
					manager.SendMessage(item);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					break;
				}
				Console.WriteLine("_________________________");
				Console.WriteLine(item.Title + "\n" + item.Author + "\n" + item.Description + "\n" + item.Url + "\n" + item.Date + "\n" + item.Type + "\n" + item.Topics + "\n" + item.NumberOfViews);
				Thread.Sleep(10000);
			}
			Console.WriteLine(data.Count);
			Console.ReadKey();
		}
	}
}
