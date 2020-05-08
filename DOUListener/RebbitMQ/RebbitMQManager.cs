using DOUListener.Data;
using DOUListener.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace DOUListener.RebbitMQ
{
	public class RebbitMQManager
	{
        private readonly string _queueName = "data";
        private readonly string _hostName = "localhost";
        private readonly int _port = 5672;
        private readonly ListenerDbContext _context = new ListenerDbContext();

        public RebbitMQManager()
        {
            _context = new ListenerDbContext();
        }

        public void ReadMesage()
        {
            var factory = new ConnectionFactory() { HostName = _hostName, Port = _port };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName,
                      durable: false,
                      exclusive: false,
                      autoDelete: false,
                      arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (eventModel, args) =>
                {
                    var body = args.Body;
                    var message = Encoding.UTF8.GetString(body.ToArray());
                    if (!string.IsNullOrEmpty(message))
                    {
                        Console.WriteLine(AddDataInDatabase(message));
                        Console.WriteLine();
                    }
                };
                channel.BasicConsume(queue: _queueName,
                                     autoAck: true,
                                     consumer: consumer);
            }
        }
        #region Private Methods
        private string AddDataInDatabase(string message)
        {
            var postInfo = GetPost(message);
            try
            {
                _context.Posts.Add(postInfo);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message} \n";
            }
            return "Post was added)";
        }

        private PostInfo GetPost(string message)
        {
            var data = message.Split('*');

            var post = new PostInfo();
                post.Title  = data[0];
                post.Author = data[1];
                post.Url = data[2];
                post.Description = data[3];
                post.Date = data[4];
                post.Type = data[5];
                post.Topics = data[6];

                int numberOfViews = 0;
                Int32.TryParse(data[7], out numberOfViews); Int32.TryParse(data[7], out numberOfViews);
                post.NumberOfViews = numberOfViews;

            return post;
        }
        #endregion
    }
}
