using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using HWInternshipProject.Services;
using HWInternshipProject.Models;
using HWInternshipProject.Services.Validators;
using HWInternshipProject.Services.Models;

namespace HWInternshipProject.ViewModels
{
    public class SignUpViewViewModel : ViewModelBase
    {
        string _login = "";
        string _password = "";
        string _confirm_password = "";

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
        public string ConfirmPassword
        {
            get { return _confirm_password; }
            set
            {
                SetProperty(ref _confirm_password, value);
            }
        }

        public DelegateCommand SignUpCommand { get; set; }
        public SignUpViewViewModel(INavigationService navigationService, ILoginValidatorService loginValidator, IPasswordValidatorService passwordValidator, IUserService userService) :
            base(navigationService)
        {
            SignUpCommand = new DelegateCommand(() =>
            {
                if (Password != ConfirmPassword)
                {
                    App.Current.MainPage?.DisplayAlert("Error", "Password and Confirm password do not match", "Ok");
                    return;
                }

                switch (passwordValidator.IsPasswordValid(Password))
                {
                    case PasswordValidationStatus.InvalidLength:
                        App.Current.MainPage?.DisplayAlert("Error", "Password length must be greater than 8 and lower than 16", "Ok");
                        return;
                    case PasswordValidationStatus.InvalidContent:
                        App.Current.MainPage?.DisplayAlert("Error", "Password must contains minimum one digit and letter in UPPER case", "Ok");
                        return;
                }

                switch (loginValidator.IsLoginValid(Login))
                {
                    case LoginValidationStatus.InvalidLength:
                        App.Current.MainPage?.DisplayAlert("Error", "Login length must be greater than 4 and lower than 16", "Ok");
                        return;
                    case LoginValidationStatus.StartsWithDigit:
                        App.Current.MainPage?.DisplayAlert("Error", "Login must starts with letter", "Ok");
                        return;
                    case LoginValidationStatus.LoginNotUnique:
                        App.Current.MainPage?.DisplayAlert("Error", "This login already exist", "Ok");
                        return;
                }

                userService.SignUp(Login, Password);
                navigationService.GoBackAsync(("Login", Login));


            },
                () => Login.Length != 0 && Password.Length != 0 && ConfirmPassword.Length != 0);

            SignUpCommand.ObservesProperty(() => Login);
            SignUpCommand.ObservesProperty(() => Password);
            SignUpCommand.ObservesProperty(() => ConfirmPassword);
        }
    }
}
