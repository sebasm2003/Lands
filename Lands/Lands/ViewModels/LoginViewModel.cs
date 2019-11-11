namespace Lands.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Lands.Views;
    using System;
    using System.ComponentModel;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel : BaseViewModel
    {
        #region Attributes

        private string _email;
        private string _password;
        private bool _isRunning;
        private bool _isEnabled;

        #endregion

        #region Properties

        
        public string Email
        {
            get { return this._email; }
            set { SetValue(ref this._email, value); }
        }
        public string Password
        {
            get { return this._password; }
            set { SetValue(ref this._password, value); }
        }
        public bool IsRunning
        {
            get { return this._isRunning; }
            set { SetValue(ref this._isRunning, value); }
        }
        public bool IsRemembered { get; set; }
        public bool IsEnabled
        {
            get { return this._isEnabled; }
            set { SetValue(ref this._isEnabled, value); }
        }

        #endregion

        #region Constructors

        public LoginViewModel()
        {
            this.IsRemembered = true;
            this.IsEnabled = true;
        }

        #endregion

        #region Commands

        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }



        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter an email",
                    "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter a password",
                    "Accept");
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            if (this.Email != "sebasm2003@hotmail.com" || this.Password != "1234")
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Email or password incorrect",
                    "Accept");
                this.Password = string.Empty;
                return;
            }

           

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Lands = new LandsViewModel(); 
            await Application.Current.MainPage.Navigation.PushAsync(new LandsPage());

            this.IsRunning = false;
            this.IsEnabled = true;

            this.Email = string.Empty;
            this.Password = string.Empty;
        }

        #endregion
    }
}
