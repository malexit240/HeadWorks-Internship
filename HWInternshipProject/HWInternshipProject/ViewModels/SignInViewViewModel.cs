using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using HWInternshipProject.Models;


namespace HWInternshipProject.ViewModels
{
    public class SignInViewViewModel : ViewModelBase
    {

        string _login = "";
        string _password = "";

        public string Login
        {
            get { return _login; }
            set
            {
                SetProperty(ref _login, value);
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                SetProperty(ref _password, value);
            }
        }

        public DelegateCommand SignInCommand { get; set; }
        public DelegateCommand SignUpCommand { get; set; }

        public SignInViewViewModel(INavigationService navigationService) :
            base(navigationService)
        {
            SignInCommand = new DelegateCommand(async () =>
           {
               var user = await User.SignInAsync(Login, Password);

               if (user != null)
                   await navigationService.NavigateAsync("MainListView", ("User", user));
               else
                   App.Current.MainPage?.DisplayAlert("Error", "User not found", "Ok").ContinueWith((_) => { Password = ""; });

           },
            () => Login.Length != 0 && Password.Length != 0
            );

            SignInCommand.ObservesProperty(() => Login);
            SignInCommand.ObservesProperty(() => Password);

            SignUpCommand = new DelegateCommand(() => navigationService.NavigateAsync("SignUpView"));


        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            Login = parameters.GetValue<string>("Login") ?? Login;
            Password = parameters.GetValue<string>("Password") ?? Password;
        }
    }
}
