namespace SmartOffice.Services.MQTTServices;

public interface IMqttService
{
    Task CreateAndConnectMqttClient();
    Task DisconnectMqttClient();
}