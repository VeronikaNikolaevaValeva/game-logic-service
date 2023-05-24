using GameLogicService.Messaging.Interfaces;
using GameLogicService.Models.Entity;
using GameLogicService.Models.Messaging;
using RabbitMQ.Client;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;

namespace GameLogicService.Messaging
{
    public class MessageSender : IMessageSender
    {
        private readonly IConnectionFactory connectionFactory;
        public MessageSender() {
            connectionFactory = new ConnectionFactory()
            {
                HostName = "74.234.106.65",
                Port = 5672,
                UserName = "user",
                Password = "GPPpwQKWK@X8"
            };
        }  
        public void NewRegisteredUser(NewPlayerScoreEntity newPlayerScoreEntity)
        {
            using (IConnection connection = connectionFactory.CreateConnection())
            {
                IModel channel = connection.CreateModel();
                channel.ExchangeDeclare("NewRegisteredUserExchange", ExchangeType.Topic, true);

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                var jsonString = JsonSerializer.Serialize(newPlayerScoreEntity, options);
                byte[] body = Encoding.Unicode.GetBytes(jsonString);
                Console.WriteLine(jsonString);
                channel.BasicPublish("NewRegisteredUserExchange", "NewUserRoutingKey", null, body);
            }
        }
        
        public void UpdateUserScore(UpdateUserScore updateUserScore)
        {
            using (IConnection connection = connectionFactory.CreateConnection())
            {
                IModel channel = connection.CreateModel();
                channel.ExchangeDeclare("UpdateUserScoreExchange", ExchangeType.Topic, true);

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                var jsonString = JsonSerializer.Serialize(updateUserScore, options);
                byte[] body = Encoding.Unicode.GetBytes(jsonString);
                Console.WriteLine(body);
                channel.BasicPublish("UpdateUserScoreExchange", "UpdateScoreRoutingKey", null, body);
            }
        }

        

    }
}
