using DatabaseAutoDeployment.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace Database
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();

            ConfigureServices(services);

            using ServiceProvider serviceProvider = services.BuildServiceProvider();
            var form1 = serviceProvider.GetRequiredService<Form1>();
            var context = serviceProvider.GetRequiredService<FormDbContext>();
            context.Database.Migrate();
            Application.Run(form1);
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["production"].ConnectionString;

            services.AddLogging(configure => configure.AddConsole()).AddSingleton<Form1>().RegisterSqlDatabase<FormDbContext>(connectionString);
        }
    }
}
