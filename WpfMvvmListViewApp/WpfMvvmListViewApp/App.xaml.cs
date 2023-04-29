using CustomerLib;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using WpfMvvmListViewApp.ViewModel;

namespace WpfMvvmListViewApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();
        }

        public new static App Current => (App)Application.Current;

        public IServiceProvider? Services { get; }

        private static IServiceProvider? ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<ICustomerRepository, CustomerRepository>();
            services.AddSingleton<MainViewModel>();

            return services.BuildServiceProvider();
        }

        //public MainViewModel? MainVM => Services?.GetService<MainViewModel>();
    }
}
