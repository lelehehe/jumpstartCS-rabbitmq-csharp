using System;
using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory() { HostName = "localhost" };

using var connection = factory.CreateConnection();

using var channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "routing", type: ExchangeType.Direct);

var message = "This message needs to be routed";
var message2 = "This message routed for both";

var body = Encoding.UTF8.GetBytes(message);
var body2 = Encoding.UTF8.GetBytes(message2);


channel.BasicPublish(exchange: "routing", routingKey: "analyticsonly", null, body);
channel.BasicPublish(exchange: "routing", routingKey: "both", null, body2);

Console.WriteLine($"Send message: {message}");
Console.WriteLine($"Send message: {message2}");