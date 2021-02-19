using System;
using Prism.Mvvm;
using HWInternshipProject.Models;
using System.Globalization;
using Prism.Commands;
using Prism.Common;
using Prism.Navigation;

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

        public ProfileViewModel(INavigationService navigationService, Profile profile) :
            base(navigationService)
        {
            this._profile = profile;
            DeleteCommand = new DelegateCommand(() =>
            {
                this._profile.Delete();
            });

            EditCommand = new DelegateCommand(() =>
            {
                navigationService.NavigateAsync("AddEditProfileView", ("profile", this._profile));
            });
        }
    }
}
