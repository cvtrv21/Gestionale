using BiciclottiWpf.Data;
using BiciclottiWpf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BiciclottiWpf.Pages
{
    /// <summary>
    /// Logica di interazione per LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        public LoginForm()
        {
            InitializeComponent();

            try
            {
                // Ottieni il primo utente dal database
                User firstUser = QueueUnitOfWorks.BiciclottiRepository.GetAllUsers().FirstOrDefault();

                if (firstUser != null)
                {
                    // Inizializza gli TextBox con le informazioni del primo utente
                    UsernameTextBox.Text = firstUser.Name;
                    PasswordBox.Password = firstUser.Password; // O puoi lasciarlo vuoto, a seconda delle tue esigenze
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore durante il recupero del primo utente: {ex.Message}");
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Esempio di logica di accesso hardcoded (da sostituire con la tua logica reale)
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (IsValidUser(username, password))
            {
                // L'accesso è riuscito, chiudi la finestra di login
                //this.Close();

                // Puoi aprire la finestra principale o eseguire altre azioni post accesso qui
                MainWindow mainWindow = new MainWindow(username);
                mainWindow.Show();

                //this.Visibility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("Credenziali non valide. Riprova.");
            }
        }

        private bool IsValidUser(string username, string password)
        {
            try
            {
                // Verifica se lo username e la password corrispondono a un utente nel database
                User user = QueueUnitOfWorks.BiciclottiRepository.GetAllUsers()
                    .FirstOrDefault(u => u.Name == username && u.Password == password);

                // Restituisci true solo se un utente è stato trovato
                return user != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore durante la verifica dell'utente: {ex.Message}");
                return false;
            }
        }

        private void OnTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            // Se il testo è uguale al prompt, cancella il testo
            if (textBox.Text == "Utente" || textBox.Text == "Password")
            {
                textBox.Text = string.Empty;
            }
        }

        private void OnTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            // Se il testo è vuoto, ripristina il prompt
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                if (textBox.Name == "UsernameTextBox")
                {
                    textBox.Text = "Utente";
                }
                else if (textBox.Name == "PasswordTextBox")
                {
                    textBox.Text = "Password";
                }
            }
        }

        private void OnPasswordBoxGotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;

            // Se la password è uguale al prompt, cancella la password
            if (PasswordBox.Password == "Password")
            {
                passwordBox.Password = string.Empty;
            }
        }

        private void OnPasswordBoxLostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;

            // Se la password è vuota, ripristina il prompt
            if (string.IsNullOrWhiteSpace(passwordBox.Password))
            {
                passwordBox.Password = "Password";
            }
        }




    }
}
