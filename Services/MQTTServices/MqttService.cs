using System.Text;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using MQTTnet;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Client.Receiving;
using MQTTnet.Extensions.ManagedClient;
using Newtonsoft.Json;
using Notification.Wpf;
using SmartOffice.Views.FoodOrdering;

namespace SmartOffice.Services.MQTTServices;

public class MqttService : IMqttService
{
    private readonly IServiceProvider _service;
    private readonly NotificationManager _notification;
    private IManagedMqttClient _mqttClient = null!;
    private bool _clientRunning;

    public MqttService(IServiceProvider service)
    {
        _service = service;
        _notification = new NotificationManager();
    }

    public async Task CreateAndConnectMqttClient()
    {
        var username = AppSettings.Username;
        string json = JsonConvert.SerializeObject(new { message = "Verbunden", sent = DateTimeOffset.UtcNow });

        if (_clientRunning)
        {
            _notification.Show("Smart Office", "Verbindung zum Broker bereits vorhanden.", NotificationType.Information);
            return;
        }
        
        _clientRunning = true;

        _mqttClient = new MqttFactory().CreateManagedMqttClient();
        _mqttClient.ConnectedHandler = new MqttClientConnectedHandlerDelegate(OnConnected);
        _mqttClient.DisconnectedHandler = new MqttClientDisconnectedHandlerDelegate(OnDisconnected);
        _mqttClient.ConnectingFailedHandler = new ConnectingFailedHandlerDelegate(OnConnectingFailed);
        _mqttClient.ApplicationMessageReceivedHandler = new MqttApplicationMessageReceivedHandlerDelegate(a =>
        {
            string payload = Encoding.UTF8.GetString(a.ApplicationMessage.Payload);
            if (a.ApplicationMessage.Topic == "smartoffice/foodorder")
            {
                _notification.Show("Smart Office - Essensbestellung",
                    "Hunger? Es wird Essen bestellt. Möchten Sie mitbestellen?",
                    NotificationType.Information,
                    LeftButtonText: "Ja",
                    LeftButton: async () =>
                    {
                        var orderStart = _service.GetRequiredService<OrderStart>();
                        orderStart.Show();
                        orderStart.stepper.SelectedIndex = 1;
                        await orderStart.LoadDishesFromLastOrder();
                    },
                    expirationTime: TimeSpan.FromSeconds(30));
            }
            else if (payload == "ON")
            {
                _notification.Show("Smart Office", "Es hat geklingelt.", NotificationType.Information);
            }
        });

        MqttClientOptionsBuilder builder = new MqttClientOptionsBuilder()
            .WithClientId(username)
            .WithTcpServer("192.168.42.174", 1883);

        ManagedMqttClientOptions options = new ManagedMqttClientOptionsBuilder()
            .WithAutoReconnectDelay(TimeSpan.FromSeconds(30))
            .WithClientOptions(builder.Build())
            .Build();

        await _mqttClient.StartAsync(options);
        await _mqttClient.SubscribeAsync("%esp8266/switch%/%stat%/POWER");
        await _mqttClient.SubscribeAsync("smartoffice/foodorder");
        await _mqttClient.PublishAsync("esp8266/switch", json);
    }

    public async Task DisconnectMqttClient()
    {
        string json = JsonConvert.SerializeObject(new { message = "Getrennt", sent = DateTimeOffset.UtcNow });

        if (_mqttClient != null)
        {
            await _mqttClient.PublishAsync("esp8266/switch", json);
            await _mqttClient.StopAsync();
            _clientRunning = false;
        }
    }
    
    public IManagedMqttClient GetMqttClient()
    {
        return _mqttClient;
    }
    
    public void OnConnected(MqttClientConnectedEventArgs obj)
    {
        _notification.Show("Smart Office", "Verbindung zum Broker hergestellt.", NotificationType.Success);
    }
    
    public void OnDisconnected(MqttClientDisconnectedEventArgs obj)
    {
        _notification.Show("Smart Office", "Verbindung zum Broker getrennt.", NotificationType.Success);
    }
    
    public void OnConnectingFailed(ManagedProcessFailedEventArgs obj)
    {
        _notification.Show("Smart Office", "Verbindung zum Broker nicht möglich!", NotificationType.Error);
    }
}