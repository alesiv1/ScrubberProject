﻿using System;

namespace WpfClient.Model
{
	public class Post
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public string Url { get; set; }
		public string Description { get; set; }
		public string Date { get; set; }
		public string Type { get; set; }
		public string Topics { get; set; }
		public int NumberOfViews { get; set; }
		public bool Like { get; set; }
	}
}
