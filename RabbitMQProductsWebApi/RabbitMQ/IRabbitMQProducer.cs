namespace RabbitMQProductsWebApi.RabbitMQ
{
    public interface IRabbitMQProducer
    {
        public void SendProductMessage<T> (T message);
    }
}
