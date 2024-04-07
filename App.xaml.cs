using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartOffice.Context;
using SmartOffice.Services.FoodOrderServices.MenuService;
using SmartOffice.Services.FoodOrderServices.OrderService;
using SmartOffice.Services.FoodOrderServices.RestaurantService;
using SmartOffice.Services.MQTTServices;
using SmartOffice.Services.NotificationService;
using SmartOffice.Services.UserService;
using SmartOffice.Views;
using SmartOffice.Views.FoodOrdering;

namespace SmartOffice;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    private static readonly IHost Host = Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
        .ConfigureServices((context, services) => { ConfigureServices(context.Configuration, services); })
        .Build();
    
    private void OnStartup(object sender, StartupEventArgs e)
    {
        using var mutex = new Mutex(true, "SmartOfficeApplication", out var createdNew);
        if (createdNew)
        {
            Host.Start();

            var login = Host.Services.GetRequiredService<Login>();
            login.Show();
        }
        else
        {
            MessageBox.Show("Die Anwendung ist bereits gestartet.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            Current.Shutdown();
        }
    }
    
    private static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        // DB-Context
        services.AddDbContext<SmartOfficeDbContext>(options =>
            options.UseMySql(configuration.GetConnectionString("SmartOfficeDB"),
                new MySqlServerVersion(new Version(11, 3, 2)),
                builder => builder.MigrationsAssembly(typeof(SmartOfficeDbContext).Assembly.FullName)), ServiceLifetime.Transient);
        
        services.AddDbContext<SoDbContext>(options =>
            options.UseMySql(configuration.GetConnectionString("SmartOfficeDB"),
                new MySqlServerVersion(new Version(11, 3, 2)),
                builder => builder.MigrationsAssembly(typeof(SoDbContext).Assembly.FullName)), ServiceLifetime.Transient);
        
        // Services
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IRestaurantService, RestaurantService>();
        services.AddTransient<IDishService, DishService>();
        services.AddTransient<IOrderService, OrderService>();
        services.AddTransient<IMqttService, MqttService>();
        services.AddTransient<INotificationService, NotificationService>();

        // Logger
        services.AddLogging(configure => configure.AddConsole());

        // Screens
        services.AddTransient<Login>();
        services.AddTransient<Register>();
        services.AddTransient<HomeScreen>();
        services.AddTransient<FoodOrderHome>();
        services.AddTransient<OrderStart>();
        services.AddTransient<AddRestaurants>();
        services.AddTransient<AddDishes>();
        services.AddTransient<OrderHistory>();
    }
}