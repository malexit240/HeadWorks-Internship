using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using HWInternshipProject.Models;
using System.Collections.ObjectModel;

namespace HWInternshipProject.ViewModels
{
    public class ProfileViewModel : BindableBase
    {
        Profile _profile;

        public string Name
        {
            get
            {
                return _profile.Name;
            }
            set
            {
                _profile.Name = value;
                RaisePropertyChanged("Name");
            }
        }

        public ProfileViewModel(Profile profile)
        {
            this._profile = profile;
        }
    }

    public class MainListViewModel : ViewModelBase
    {
        User _user;
        ObservableCollection<ProfileViewModel> _profiles = new ObservableCollection<ProfileViewModel>();

        public DelegateCommand AddProfileCommand { get; set; }

        public ObservableCollection<ProfileViewModel> Profiles
        {
            get { return _profiles; }
            set
            {
                SetProperty(ref _profiles, value);
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);

            _user = User.Current;
            Profiles.Clear();

            foreach (var profile in _user.Profiles)
            {
                Profiles.Add(new ProfileViewModel(profile));
            }

            RaisePropertyChanged("Profiles");
        }

        public MainListViewModel(INavigationService navigationService) :
            base(navigationService)
        {
            AddProfileCommand = new DelegateCommand(() =>
            {
                navigationService.NavigateAsync("AddEditProfileView");
            });



        }
    }
}
