using BiciclottiWpf.Data;
using BiciclottiWpf.Pages;
using FrameQueues.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BiciclottiWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Crea e mostra la finestra di login
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();

            // Verifica se l'utente ha effettuato l'accesso prima di aprire la finestra principale
            if (loginForm.DialogResult.HasValue && loginForm.DialogResult.Value)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
            else
            {
                // L'utente ha chiuso la finestra di login senza effettuare l'accesso, chiudi l'applicazione
                this.Shutdown();
            }
        }
        public App() { }
    }
}
