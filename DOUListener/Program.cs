﻿using DOUListener.RebbitMQ;
using System;
using System.Threading;

namespace DOUListener
{
	class Program
	{
		static void Main(string[] args)
		{
			RebbitMQManager rebbitMQ = new RebbitMQManager();
			while (true)
			{
				Thread.Sleep(10000);
				try
				{
					rebbitMQ.ReadMesage();
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					break;
				}
			}
			Console.ReadKey();
		}
	}
}
