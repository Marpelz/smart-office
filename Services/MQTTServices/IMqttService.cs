namespace SmartOffice.Services.MQTTServices;

public interface IMqttService
{
    Task CreateAndConnectMqttClientWithUsername(string username);
}