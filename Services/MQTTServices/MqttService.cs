using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;

namespace SmartOffice.Services.MQTTServices;

public class MqttService : IMqttService
{
    private readonly IServiceProvider _service;

    public MqttService(IServiceProvider service)
    {
        _service = service;
    }

    public async Task CreateAndConnectMqttClientWithUsername(string username)
    {
        var mqttFactory = new MqttFactory();

        using (var managedMqttClient = mqttFactory.CreateManagedMqttClient())
        {
            var mqttClientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer("broker.hivemq.com")
                .WithCredentials(username)
                .Build();

            var managedMqttClientOptions = new ManagedMqttClientOptionsBuilder()
                .WithClientOptions(mqttClientOptions)
                .Build();

            await managedMqttClient.StartAsync(managedMqttClientOptions);

            // The application message is not sent. It is stored in an internal queue and
            // will be sent when the client is connected.
            await managedMqttClient.EnqueueAsync("Topic", "Payload");

            Console.WriteLine("The managed MQTT client is connected.");

            // Wait until the queue is fully processed.
            SpinWait.SpinUntil(() => managedMqttClient.PendingApplicationMessagesCount == 0, 10000);

            Console.WriteLine($"Pending messages = {managedMqttClient.PendingApplicationMessagesCount}");
        }
    }
}