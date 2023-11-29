using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using SmartOffice.Services.UserService;
using SmartOffice.Views.Chat;
using SmartOffice.Views.FoodOrdering;
using SmartOffice.Views.MQTT;
using SmartOffice.Views.Settings;

namespace SmartOffice.Views
{
    public partial class HomeScreen : Window
    {
        private readonly IUserService _userService;
        private readonly IServiceProvider _service;
        
        public HomeScreen(IServiceProvider service)
        {
            InitializeComponent();
            _service = service;
            _userService = _service.GetRequiredService<IUserService>();
        }
        
        private void WindowDragMove(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void LoadMqtt(object sender, RoutedEventArgs e)
        {
            RenderPages.Children.Clear();
            RenderPages.Children.Add(new MQTTClient());
        }

        private void LoadChat(object sender, RoutedEventArgs e)
        {
            RenderPages.Children.Clear();
            RenderPages.Children.Add(new ChatRoom());
        }

        private void LoadFoodOrder(object sender, RoutedEventArgs e)
        {
            RenderPages.Children.Clear();
            RenderPages.Children.Add(new FoodOrder());
        }

        private void LoadAdminSettings(object sender, RoutedEventArgs e)
        {
            RenderPages.Children.Clear();
            RenderPages.Children.Add(new AdminSettings());
        }
        
        private void LoadUserSettings(object sender, RoutedEventArgs e)
        {
            RenderPages.Children.Clear();
            RenderPages.Children.Add(new UserSettings());
        }
        
        private void Logout(object sender, RoutedEventArgs e)
        {
            var login = _service.GetRequiredService<Login>();
            login.Show();
            Close();
        }
    }
}