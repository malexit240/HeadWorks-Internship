using System;
using Prism.Mvvm;
using HWInternshipProject.Models;
using System.Globalization;
using Prism.Commands;
using Prism.Common;
using Prism.Navigation;
using HWInternshipProject.Services.Models;


namespace HWInternshipProject.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        Profile _profile;

        public string Name
        {
            get { return _profile.Name; }
            set
            {
                _profile.Name = value;
                RaisePropertyChanged("Name");
            }
        }

        public string NickName
        {
            get { return _profile.NickName; }
            set
            {
                _profile.NickName = value;
                RaisePropertyChanged("NickName");
            }
        }

        public string Date
        {
            get
            {
                return _profile.CreationTime.Date.ToString(CultureInfo.CurrentCulture.DateTimeFormat.LongDatePattern);
            }
        }

        public string ImageDestination
        {
            get
            {
                return _profile.ImageDestination == "" ? "DefaultProfilePicture.png" : _profile.ImageDestination;
            }
        }

        public DelegateCommand DeleteCommand { get; set; }
        public DelegateCommand EditCommand { get; set; }

        public DelegateCommand ShowModalImageCommand { get; set; }

        public ProfileViewModel(INavigationService navigationService, IProfileService profileService, Profile profile) :
            base(navigationService)
        {
            this._profile = profile;
            DeleteCommand = new DelegateCommand(async () =>
            {
                if (await App.Current.MainPage.DisplayAlert("Delete?", "Would you want to delete?", "Yes", "No"))
                    profileService.RemoveProfile(_profile);
            });

            EditCommand = new DelegateCommand(() =>
            {
                navigationService.NavigateAsync("AddEditProfileView", ("profile", this._profile));
            });

            ShowModalImageCommand = new DelegateCommand(() =>
            {

                var parameters = new NavigationParameters();
                parameters.Add("ImageDestination", ImageDestination);

                //navigationService.NavigateAsync("SizedProfileImagePage", parameters, true, true);
            });
        }
    }
}
