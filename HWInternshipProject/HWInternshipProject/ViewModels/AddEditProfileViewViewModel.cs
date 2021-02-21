﻿using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using HWInternshipProject.Models;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Acr.UserDialogs;

using HWInternshipProject.Services.Models;
using HWInternshipProject.Resources;

namespace HWInternshipProject.ViewModels
{
    public class AddEditProfileViewViewModel : ViewModelBase
    {
        bool _IsEdit = false;
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

        public string Description
        {
            get { return _profile.Description; }
            set
            {
                _profile.Description = value;
                RaisePropertyChanged("Description");
            }
        }

        public string ImageDestination
        {
            get { return _profile.ImageDestination == "" ? "DefaultProfilePicture.png" : _profile.ImageDestination; }
            set
            {
                _profile.ImageDestination = value;
                RaisePropertyChanged("ImageDestination");
            }
        }

        public DelegateCommand AddSaveCommand { get; set; }
        public DelegateCommand SelectImageCommand { get; set; }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("profile"))
            {
                _IsEdit = true;
                _profile = parameters.GetValue<Profile>("profile");
                RaisePropertyChanged("Name");
                RaisePropertyChanged("NickName");
                RaisePropertyChanged("Description");
                RaisePropertyChanged("ImageDestination");
            }
        }

        public AddEditProfileViewViewModel(INavigationService navigationService, IProfileService profileService) :
            base(navigationService)
        {
            _profile = new Profile() { UserId = User.Current.UserId };
            AddSaveCommand = new DelegateCommand(() =>
            {

                if (NickName == "" || Name == "")
                {
                    UserDialogs.Instance.Alert(TextResources.Error, TextResources.FiledsNandNNmustbefilled, TextResources.Ok);

                    return;
                }



                if (_IsEdit)
                    profileService.UpdateProfile(_profile);
                else
                    profileService.AddProfile(_profile);
                navigationService.GoBackAsync();
            });

            SelectImageCommand = new DelegateCommand(async () =>
            {
                UserDialogs.Instance.ActionSheet(new ActionSheetConfig()
                {
                    Message = TextResources.ChooseAction,
                    Options = new List<ActionSheetOption>()
                    {
                        new ActionSheetOption(TextResources.PickAtGallery, async ()=>{
                            var result = await FilePicker.PickAsync();
                            ImageDestination = result?.FullPath ?? ImageDestination;
                        },""),

                        new ActionSheetOption(TextResources.TakeAPhotoWithCamera, async ()=>{

                        },"")
                    },
                    Cancel = new ActionSheetOption(TextResources.Cancel)
                });
            });
        }
    }
}
