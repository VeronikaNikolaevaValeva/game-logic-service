using GameLogicService.Messaging.Interfaces;
using GameLogicService.Models.Entity;
using GameLogicService.Models.Messaging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net.Mail;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;

namespace GameLogicService.Messaging
{
    public class MessageSender : IMessageSender
    {
        private readonly IConnectionFactory connectionFactory;
        private bool deletionResult = false;
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

        //public void DeleteUserData(string emailAddress)
        //{
        //    using (IConnection connection = connectionFactory.CreateConnection())
        //    {
        //        IModel channel = connection.CreateModel();
        //        channel.ExchangeDeclare("DeleteUserExchange", ExchangeType.Topic, true);

        //        var options = new JsonSerializerOptions
        //        {
        //            WriteIndented = true
        //        };
        //        var jsonString = JsonSerializer.Serialize(emailAddress, options);
        //        byte[] body = Encoding.Unicode.GetBytes(jsonString);
        //        Console.WriteLine(body);
        //        channel.BasicPublish("DeleteUserExchange", "DeleteUserRoutingKey", null, body);
        //    }
        //}

        //public bool DeletedUserData()
        //{
        //    using (IConnection connection = connectionFactory.CreateConnection())
        //    {
        //        IModel channel = connection.CreateModel();
        //        channel.ExchangeDeclare("DeletedUserExchange", ExchangeType.Topic, true);

        //        channel.QueueDeclare("DeletedUserQueue", true, false, false, null);
        //        channel.QueueBind("DeletedUserQueue", "DeletedUserExchange", "DeletedUserRoutingKey");

        //        var consumer = new EventingBasicConsumer(channel);
        //        consumer.Received += Consumer_Received_DeleteUserData;
        //        channel.BasicConsume("DeletedUserQueue", true, consumer);
        //    }
        //    return deletionResult;
        //}

        //private void Consumer_Received_DeleteUserData(object? sender, BasicDeliverEventArgs e)
        //{
        //    byte[] body = e.Body.ToArray();
        //    string message = Encoding.Unicode.GetString(body);
        //    Console.WriteLine("Any messages about deleted users?: " + message);
        //    deletionResult = JsonSerializer.Deserialize<bool>(message);
        //}
    }
}
