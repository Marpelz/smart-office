using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using MQTTnet.Extensions.ManagedClient;
using SmartOffice.Services.MQTTServices;
using SmartOffice.Services.UserServices;
using SmartOffice.Views.Chat;
using SmartOffice.Views.FoodOrdering;
using SmartOffice.Views.Settings;

namespace SmartOffice.Views
{
    public partial class HomeScreen : Window
    {
        private readonly IServiceProvider _service;
        private readonly IUserService _userService;
        private readonly IMqttService _mqttService;
        private readonly MqttService _mqttServiceInstance;
        private IManagedMqttClient _mqttClient;
        
        
        public HomeScreen(IServiceProvider service)
        {
            InitializeComponent();
            _service = service;
            _userService = _service.GetRequiredService<IUserService>();
            _mqttService = _service.GetRequiredService<IMqttService>();
            _mqttServiceInstance = (MqttService)_mqttService;
            _mqttClient = _mqttServiceInstance.GetMqttClient();
        }
        
        private void WindowDragMove(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        
        private void OpenFoodOrderBTN_Click(object sender, RoutedEventArgs e)
        {
            RenderPages.Children.Clear();
            RenderPages.Children.Add(new FoodOrderHome(_service));
        }

        private void OpenChatBTN_Click(object sender, RoutedEventArgs e)
        {
            RenderPages.Children.Clear();
            RenderPages.Children.Add(new ChatRoom());
        }

        private void OpenAdminSettingsBTN_Click(object sender, RoutedEventArgs e)
        {
            RenderPages.Children.Clear();
            RenderPages.Children.Add(new AdminSettings());
        }
        
        private void OpenUserSettingsBTN_Click(object sender, RoutedEventArgs e)
        {
            RenderPages.Children.Clear();
            RenderPages.Children.Add(new UserSettings());
        }

        private void MinimizeBTN_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        
        private async void Logout(object sender, RoutedEventArgs e)
        {
            var login = _service.GetRequiredService<Login>();
            await _mqttServiceInstance.DisconnectMqttClient();
            login.Show();
            Close();
        }
    }
}