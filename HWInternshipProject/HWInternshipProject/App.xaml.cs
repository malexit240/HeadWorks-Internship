using HWInternshipProject.ViewModels;
using HWInternshipProject.Views;
using Prism;
using Prism.Ioc;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using System.Resources;
using Prism.Navigation;

namespace HWInternshipProject
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {

        }

        protected override async void OnInitialized()
        {

            InitializeComponent();

            //this.Properties.Clear();

            if (!this.Properties.ContainsKey("login"))
                this.Properties.Add("login", "malexit240");
            if (!this.Properties.ContainsKey("password"))
                this.Properties.Add("password", "Password123");

            var login = this.Properties["login"];
            var password = this.Properties["password"];
            System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("ru-RU");
            await NavigationService.NavigateAsync("NavigationPage/SignInView", ("Login", login), ("Password", password));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignInView, SignInViewViewModel>();
            containerRegistry.RegisterForNavigation<MainListView, MainListViewModel>();
            containerRegistry.RegisterForNavigation<SignUpView, SignUpViewViewModel>();
            containerRegistry.RegisterForNavigation<AddEditProfileView, AddEditProfileViewViewModel>();
        }
    }
}
