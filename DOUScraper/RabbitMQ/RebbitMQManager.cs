using DOUScraper.Models;
using RabbitMQ.Client;
using System;
using System.Text;

namespace DOUScraper.RabbitMQ
{
	public class RebbitMQManager
	{
		private readonly string _queueName = "data";
		private readonly string _hostName = "localhost";
		private readonly int _port = 5672;

		public void SendMessage(PostInfo model)
		{
			var factory = new ConnectionFactory() { HostName = _hostName, Port = _port };
			using (var connection = factory.CreateConnection())
			{
				using (var channel = connection.CreateModel())
				{
					channel.QueueDeclare(queue: _queueName,
										 durable: false,
										 exclusive: false,
										 autoDelete: false,
										 arguments: null);

					channel.BasicPublish(exchange: string.Empty,
										 routingKey: _queueName,
										 basicProperties: null,
										 body: Encoding.UTF8.GetBytes($"{model.Title}*{model.Author}*{model.Url}*{model.Description}*{model.Date}*{model.Type}*{model.Topics}*{model.NumberOfViews}"));
				}
			}
		}
		public void SendMessage(string message)
		{
			var factory = new ConnectionFactory() { HostName = _hostName, Port = _port };
			using (var connection = factory.CreateConnection())
			{
				using (var channel = connection.CreateModel())
				{
					channel.QueueDeclare(queue: _queueName,
										 durable: false,
										 exclusive: false,
										 autoDelete: false,
										 arguments: null);

					channel.BasicPublish(exchange: string.Empty,
										 routingKey: _queueName,
										 basicProperties: null,
										 body: Encoding.UTF8.GetBytes($"{message}"));
				}
			}
		}
	}
}
