using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HWInternshipProject.ViewModels
{
    public class SizedProfileImagePageViewModel : ViewModelBase
    {

        string _imageDestination = "DefaultProfilePicture.png";

        public string ImageDestination
        {
            get => _imageDestination;

            set => SetProperty(ref _imageDestination, value);
        }

        public DelegateCommand TapCommand { get; set; }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("ImageDestination"))
                ImageDestination = (string)parameters["ImageDestination"];

        }

        public SizedProfileImagePageViewModel(INavigationService navigationService) :
            base(navigationService)
        {
            TapCommand = new DelegateCommand(() => navigationService.GoBackAsync());
        }
    }
}
