using System.Windows;
using System.Windows.Media;
using System.Diagnostics;
using System.Threading;
using messenger_app.db;

namespace messenger_app
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {

        public const string PLACEHOLDER_EMAIL = "example@yourdomain.com"; // Constant string representing the default value for the email user control component.
        public const string PLACEHOLDER_PWD = "<enter your password>"; // Constant string representing the default value for the password user control component.
        public const string PLACEHOLDER_FULL_NAME = "<first_name> <last_name>"; // Constant string representing the default value for the full name user control component.
        private SQLConnectionWrapper _wrapper; //  A SQLConnectionWrapper object that represents the active connection to the Microsoft SQL Server instance.

        // Default constructor that takes a single parameter to initialize @see _wrapper.
        public Register(ref SQLConnectionWrapper wrapper)
        {
            InitializeComponent();
            textEmailError.Content = "";
            textPasswordError.Content = "";
            textFullNameError.Content = "";
            _wrapper = wrapper;
           
        }

        private bool IsEmailAvailable(string email)
        {

            string emailQuery = string.Format("SELECT id FROM [users].[Table] WHERE email='{0}'", email);
            var data = _wrapper.query(emailQuery);
            return !data.ContainsKey("id");

        }
        private void handleLostFocusEmail(object sender, RoutedEventArgs e)
        {

            if (textEmail.Text.Length >= 6)
            {

                if(IsEmailAvailable(this.textEmail.Text))
                { 

                    this.textEmailError.Content = "This email is available";
                    this.textEmailError.Foreground = Brushes.Green;

                }
            }

            this.textEmail.Text = this.textEmail.Text == "" ? PLACEHOLDER_EMAIL : this.textEmail.Text;

        }

        private void handleLostFocusPwd(object sender, RoutedEventArgs e)
        {

            this.textPassword.Password = this.textPassword.Password == "" ? PLACEHOLDER_PWD : this.textPassword.Password;

        }

        private void handleGotFocusPwd(object sender, RoutedEventArgs e)
        {

            this.textPassword.Password = "";

        }

        private void handleGotFocusEmail(object sender, RoutedEventArgs e)
        {

            this.textEmail.Text = "";

        }

        private void handleRegisterClick(object sender, RoutedEventArgs e)
        {

            int errorCount = 0;

            if(textEmail.Text == "" || !IsValidEmail(textEmail.Text) || textEmail.Text == PLACEHOLDER_EMAIL)
            {

                errorCount += 1;
                textEmailError.Content = "Invalid email provided";
                textEmailError.Foreground = Brushes.Red;

            }

            if(textPassword.Password != textPasswordConfirm.Password)
            {

                errorCount += 1;
                textPasswordError.Content = "Passwords must match";
                textPasswordError.Foreground = Brushes.Red;

            }

            if(textPassword.Password.Length < 5 || textPassword.Password == PLACEHOLDER_PWD || textPasswordConfirm.Password.Length < 5 || textPasswordConfirm.Password == PLACEHOLDER_PWD)
            {

                errorCount += 1;
                textPasswordError.Content = "Invalid password entered";
                textPasswordError.Foreground = Brushes.Red;

            }

            (string F, string L) fullName = IsFullNameValid(this.textFullName.Text);
            
            if(fullName.F == null && fullName.L == null)
            {

                errorCount += 1;
                this.textFullNameError.Content = "Name is not valid";
                this.textFullNameError.Foreground = Brushes.Red;

            }

            if (errorCount == 0)
            {


                if (textEmailError.Foreground != Brushes.Green || !IsEmailAvailable(textEmail.Text))
                {

                    textEmailError.Content = "Email address in use";
                    textEmailError.Foreground = Brushes.Red;

                }
                else
                {

                    Debug.WriteLine("Registration information is valid.");
                    string email = textEmail.Text;
                    string password = textPassword.Password;
                    int gender = femaleRB.IsChecked == true ? 0 : 1;
                    Thread insertThread = new Thread(() => insertThreadHandler(fullName.F, fullName.L, email, password, gender));
                    insertThread.Start();
                    NavigateMainWindow();

                }
            }
        }

        // NavigateMainWindow() navigates the user to the main window.
        private void NavigateMainWindow()
        {

            MainWindow home = new MainWindow();
            home.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            home.Show();
            this.Close();
        }

        private void insertThreadHandler(string firstName, string lastName, string email, string password, int gender)
        {

            Mutex mutex = new Mutex();
            mutex.WaitOne();
            string insertQuery = string.Format("INSERT INTO [users].[Table] (first_name, last_name, email, password, gender) VALUES ('{0}', '{1}', '{2}', '{3}', {4})", firstName, lastName, email, password, gender);
            _wrapper.insert(insertQuery);
            mutex.ReleaseMutex();
        }

        private void handleGotFocusPwdConfirm(object sender, RoutedEventArgs e)
        {

            this.textPasswordConfirm.Password = "";

        }

        private void handleLostFocusPwdConfirm(object sender, RoutedEventArgs e)
        {

            this.textPasswordConfirm.Password = this.textPasswordConfirm.Password == "" ? PLACEHOLDER_PWD : this.textPasswordConfirm.Password;

        }

        private void handleMaleChecked(object sender, RoutedEventArgs e)
        {

            if (femaleRB != null && maleRB != null)
            {

                femaleRB.IsChecked = false;
                maleRB.IsChecked = true;

            }
        }

        private void handleFemaleChecked(object sender, RoutedEventArgs e)
        {

            if (femaleRB != null && maleRB != null)
            {

                femaleRB.IsChecked = true;
                maleRB.IsChecked = false;

            }
        }

        // IsValidEmail(email) returns true if email is valid. And otherwise, false.
        private bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        private void handlePasswordChanged(object sender, RoutedEventArgs e)
        {

            if(textPasswordConfirm == null || textPassword == null || textPasswordError == null) { return; }

            if(textPassword.Password.Length >= 5 && textPasswordConfirm.Password.Length >= 5 && textPassword.Password == textPasswordConfirm.Password)
            {

                textPasswordError.Content = "Password is valid";
                textPasswordError.Foreground = Brushes.Green;

            }
        }

        private void handlePasswordConfirmChanged(object sender, RoutedEventArgs e)
        {

            if (textPasswordConfirm == null || textPassword == null || textPasswordError == null) { return; }

            if (textPassword.Password.Length >= 5 && textPasswordConfirm.Password.Length >= 5 && textPassword.Password == textPasswordConfirm.Password)
            {

                textPasswordError.Content = "Password is valid";
                textPasswordError.Foreground = Brushes.Green;

            }
        }

        // IsFullNameValid(fullName) returns a tuple (F, L) if fullName is valid. And otherwise, (null, null).
        // fullName is valid if it consists of two-space delimited names (or strings). For example,
        // John Doe. On the other hand, a contiguous sequence such as JohnDoe or John-Doe is not valid.
        //
        // Additionally, the tuple (F, L) can be defined as a tuple containing strings F and L, where F
        // represents the first name and L represents the last name.
        private (string F, string L) IsFullNameValid(string fullName)
        {

            string[] components = this.textFullName.Text.Split(" ");

            if (components.Length != 2)
            {

                return (null, null);

            }

            if(components.Length == 2 && components[1] == "")
            {

                return (null, null);

            }

            return (components[0], components[1]);

        }

        private void handleLostFocusFullName(object sender, RoutedEventArgs e)
        {

            if(this.textFullName.Text  != "" && this.textFullName.Text != PLACEHOLDER_FULL_NAME)
            {

                (string F, string L) result = IsFullNameValid(this.textFullName.Text);

                if(result.F != null && result.L != null)
                {

                    this.textFullNameError.Content = "Name is valid";
                    this.textFullNameError.Foreground = Brushes.Green;

                }
                else
                {

                    this.textFullNameError.Content = "Name is not valid";
                    this.textFullNameError.Foreground = Brushes.Red;

                }

            }

            this.textFullName.Text = this.textFullName.Text == "" ? PLACEHOLDER_FULL_NAME : this.textFullName.Text;

        }

        private void handleGotFocusFullName(object sender, RoutedEventArgs e)
        {

            this.textFullName.Text = this.textFullName.Text != "" && this.textFullName.Text != PLACEHOLDER_FULL_NAME ? this.textFullName.Text : "";

        }

        private void handleFullNameChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

            if (this.textFullName.Text != "" && this.textFullName.Text != PLACEHOLDER_FULL_NAME)
            {

                (string F, string L) result = IsFullNameValid(this.textFullName.Text);

                if (result.F != null && result.L != null)
                {

                    this.textFullNameError.Content = "Name is valid";
                    this.textFullNameError.Foreground = Brushes.Green;

                }
                else
                {

                    this.textFullNameError.Content = "Name is not valid";
                    this.textFullNameError.Foreground = Brushes.Red;

                }

            }
        }

        private void handleBackClick(object sender, RoutedEventArgs e)
        {

            NavigateMainWindow();

        }
    }
}