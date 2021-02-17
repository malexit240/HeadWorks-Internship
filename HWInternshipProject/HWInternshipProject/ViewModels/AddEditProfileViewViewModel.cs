using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using HWInternshipProject.Models;

namespace HWInternshipProject.ViewModels
{
    public class AddEditProfileViewViewModel : ViewModelBase
    {
        string _name = "";
        string _nickName = "";
        string _description = "";


        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public string NickName
        {
            get { return _nickName; }
            set { SetProperty(ref _nickName, value); }
        }

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        public DelegateCommand AddSaveCommand { get; set; }

        public AddEditProfileViewViewModel(INavigationService navigationService) :
            base(navigationService)
        {
            AddSaveCommand = new DelegateCommand(() =>
            {
                Profile.Create(User.Current.UserId, NickName, Name, Description);
                navigationService.GoBackAsync();
            });
        }
    }
}
