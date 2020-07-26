using FollowersInsta.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;


namespace FollowersInsta
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string username;
        public string password;
        public string accountToFollow;
        public bool? toFollow;
        public string logsString = string.Empty;

        public MainWindow()
        {
            this.DataContext = this;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            username = usernameTextField.Text;
            password = passwordTestField.Text;
            accountToFollow = accToFollowTextField.Text;
            toFollow = toFollowCheckBox.IsChecked;
           
            ChromeDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.instagram.com");
            AddLogs("Opened instagram");

            LoginPage loginPage = new LoginPage(driver);
            loginPage.PerformLogin(username, password, driver);
            AddLogs("Successfully logged in");
            driver.Navigate().GoToUrl("https://www.instagram.com/" + accountToFollow);

            AccountPage accountPage = new AccountPage(driver);

            if (toFollow.Value)
            {
                accountPage.FollowAccount();
                AddLogs($"Followed account {username}");
            }

            accountPage.ClickFollowersButton();

            FollowersPage followersPage = new FollowersPage(driver);
            AddLogs(followersPage.ScrollFollowersWindow(driver));
            AddLogs(followersPage.FollowPeople(driver));

        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            username = usernameTextField.Text;
            password = passwordTestField.Text;
            accountToFollow = accToFollowTextField.Text;
            toFollow = toFollowCheckBox.IsChecked;

            ChromeDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.instagram.com");
            AddLogs("Opened instagram");

            LoginPage loginPage = new LoginPage(driver);
            loginPage.PerformLogin(username, password, driver);
            driver.Navigate().GoToUrl("https://www.instagram.com/" + username);

            AccountPage accountPage = new AccountPage(driver);
            accountPage.ClickFollowingButton();

            FollowersPage followersPage = new FollowersPage(driver);
            AddLogs(followersPage.ScrollFollowersWindow(driver));
            AddLogs(followersPage.UnfollowPeople(driver));
        }

        #region Logs
        private void LogsBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder csvContent = new StringBuilder();
            AppendLog(logsString, csvContent);
            string csvPath = "C:\\instaFollowersLog.csv";
            File.AppendAllText(csvPath, csvContent.ToString());
            LogsWindow logsWindow = new LogsWindow();
            logsWindow.Owner = this;
            this.Hide();
            logsWindow.ShowDialog();
        }

        public void AppendLog(string log, StringBuilder csvContent)
        {
            csvContent.AppendLine(log);
        }

        public string AddLogs(string logsToAdd)
        {
            logsString += $"{DateTime.Now} {logsToAdd}\n";
            return logsString;
        }
        #endregion

        #region Unused elements
        private void username_TextChanged(object sender, TextChangedEventArgs e) { }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) { }
        #endregion
    }
}
