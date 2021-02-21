using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using HWInternshipProject.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Prism.Common;
using HWInternshipProject.Services.Settings;
using HWInternshipProject.Services.Models;

namespace HWInternshipProject.ViewModels
{

    public class MainListViewModel : ViewModelBase
    {
        User _user;
        ObservableCollection<ProfileViewModel> _profiles = new ObservableCollection<ProfileViewModel>();
        protected ISettingsManager SettingsManager { get; private set; }

        public DelegateCommand AddProfileCommand { get; set; }
        public DelegateCommand LogOutCommand { get; set; }
        public DelegateCommand GoToSettingsViewCommand { get; set; }
        public SizedProfileImagePageViewModel ModalContext { get; set; }

        public bool IsNoProfilesAdded
        {
            get => Profiles.Count == 0;
        }

        public ObservableCollection<ProfileViewModel> Profiles
        {
            get { return _profiles; }
            set
            {
                SetProperty(ref _profiles, value);
            }
        }

        private void ReloadProfiles()
        {
            Profiles.Clear();

            switch (SettingsManager.ProfilesListOrderBy)
            {
                case ProfilesListOrderBy.Name:
                    _user.Profiles = _user.Profiles.OrderBy(p => p.Name).ToList();
                    break;
                case ProfilesListOrderBy.NickName:
                    _user.Profiles = _user.Profiles.OrderBy(p => p.NickName).ToList();
                    break;
                case ProfilesListOrderBy.CreationTime:
                    _user.Profiles = _user.Profiles.OrderBy(p => p.CreationTime).ToList();
                    break;

            }

            foreach (var profile in _user.Profiles)
            {
                Profiles.Add(new ProfileViewModel(NavigationService, (IProfileService)App.Current.Container.CurrentScope.Resolve(typeof(IProfileService)), profile));
            }

            RaisePropertyChanged("IsNoProfilesAdded");

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            _user = User.Current;
            ReloadProfiles();
        }


        public MainListViewModel(INavigationService navigationService, ISettingsManager settingsManager, IUserService userService) :
            base(navigationService)
        {
            SettingsManager = settingsManager;

            Profile.Actualize += (sender, args) =>
            {
                _user = User.Current;
                ReloadProfiles();
            };

            AddProfileCommand = new DelegateCommand(() =>
            {
                navigationService.NavigateAsync("AddEditProfileView");
            });

            LogOutCommand = new DelegateCommand(() =>
            {
                userService.LogOut();
                navigationService.GoBackToRootAsync();
            });

            GoToSettingsViewCommand = new DelegateCommand(() =>
            {
                navigationService.NavigateAsync("SettingsView");
            });

            ModalContext = new SizedProfileImagePageViewModel(navigationService);
        }
    }
}
