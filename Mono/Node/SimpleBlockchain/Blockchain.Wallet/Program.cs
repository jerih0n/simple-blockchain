using Microsoft.Extensions.Configuration;
using System;
using System.Windows.Forms;

namespace Blockchain.Wallet
{
    internal static class Program
    {
        public static IConfiguration Configuration;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            var builder = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}