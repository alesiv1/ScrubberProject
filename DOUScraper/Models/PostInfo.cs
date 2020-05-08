using System;
using System.Collections.Generic;
using System.Text;

namespace DOUScraper.Models
{
	public class PostInfo
	{
		public string Title { get; set; }
		public string Author { get; set; }
		public string Url { get; set; }
		public string Description { get; set; }
		public string Date { get; set; }
		public string Type { get; set; }
		public string Topics { get; set; }
		public int NumberOfViews { get; set; }
	}
}
