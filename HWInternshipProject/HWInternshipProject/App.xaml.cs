using HWInternshipProject.ViewModels;
using HWInternshipProject.Views;
using Prism;
using Prism.Ioc;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using System.Resources;
using Prism.Navigation;
using HWInternshipProject.Services;
using System.Collections.Generic;
using System;
using Xamarin.Essentials;


namespace HWInternshipProject
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
            Device.SetFlags(new string[] { "RadioButton_Experimental" });
        }

        protected override async void OnInitialized()
        {

            InitializeComponent();

            if (!this.Properties.ContainsKey("login"))
                this.Properties.Add("login", "malexit240");
            if (!this.Properties.ContainsKey("password"))
                this.Properties.Add("password", "Password123");

            var login = this.Properties["login"];
            var password = this.Properties["password"];

            System.Globalization.CultureInfo.CurrentCulture = new SettingsManager().CurrentCultureInfo;

            App.Current.Resources.MergedDictionaries.Clear();
            if (new SettingsManager().Theme == Theme.Light)
                App.Current.Resources.MergedDictionaries.Add(new LightTheme());
            else
                App.Current.Resources.MergedDictionaries.Add(new DarkTheme());

            await NavigationService.NavigateAsync("NavigationPage/SignInView", ("Login", login), ("Password", password));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterInstance<ISettingsManager>(Container.Resolve<SettingsManager>());

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignInView, SignInViewViewModel>();
            containerRegistry.RegisterForNavigation<MainListView, MainListViewModel>();
            containerRegistry.RegisterForNavigation<SignUpView, SignUpViewViewModel>();
            containerRegistry.RegisterForNavigation<AddEditProfileView, AddEditProfileViewViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewViewModel>();
        }
    }
}
