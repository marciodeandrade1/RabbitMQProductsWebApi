using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQProductsWebApi.RabbitMQ
{
    public class RabbitMQProducer : IRabbitMQProducer
    {
        public void SendProductMessage<T>(T message)
        {
            //Aqui nós especificamos o Rabbit MQ Server. 
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            //Criação da RabbitMQ connection usando a connection factory mencionada acima
            var connection = factory.CreateConnection();
            //Aqui nós criamos o canal com a sessão e model
            using var channel = connection.CreateModel();
            //declarar a fila depois de mencionar o nome e algumas propriedades relacionadas a isso
            channel.QueueDeclare("product", exclusive: false);
            //Serializa a mensagem
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            //colocar os dados na fila de produtos
            channel.BasicPublish(exchange: "", routingKey: "product", body: body);
        }
    }
}
