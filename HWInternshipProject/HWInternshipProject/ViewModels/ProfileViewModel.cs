using System;
using Prism.Mvvm;
using HWInternshipProject.Models;
using System.Globalization;
using Prism.Commands;
using Prism.Common;
using Prism.Navigation;
using HWInternshipProject.Services.Models;
using HWInternshipProject.Resources;
using Acr.UserDialogs;
using HWInternshipProject.Views;
using Xamarin.Forms;

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
            DeleteCommand = new DelegateCommand(() =>
            {
                UserDialogs.Instance.Confirm(new ConfirmConfig()
                {
                    Message = TextResources.WouldYouWantToDelete,
                    OkText = TextResources.Yes,
                    CancelText = TextResources.No,
                    OnAction = (confirm) =>
                    {
                        if (confirm)
                            profileService.RemoveProfile(_profile);
                    }
                });
            });

            EditCommand = new DelegateCommand(() =>
            {
                navigationService.NavigateAsync("AddEditProfileView", ("profile", this._profile));
            });

            ShowModalImageCommand = new DelegateCommand(() =>
            {
                ModalConverter.OpenViewOnPage(PageUtilities.GetCurrentPage(App.Current.MainPage) as ContentPage, new SizedProfileImageView() { BindingContext = this });
            });
        }
    }

    public static class ModalConverter
    {
        public static void OpenViewOnPage(ContentPage page, ContentView view)
        {
            var content = page.Content;

            var absoluteLayout = new AbsoluteLayout();
            AbsoluteLayout.SetLayoutBounds(content, new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(content, AbsoluteLayoutFlags.SizeProportional);
            absoluteLayout.Children.Add(content);

            var stack = new StackLayout();
            AbsoluteLayout.SetLayoutBounds(stack, new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(stack, AbsoluteLayoutFlags.SizeProportional);
            stack.BackgroundColor = Color.FromHex("#A0F0F0F0");

            stack.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new DelegateCommand(() =>
                {
                    CloseView(page);
                })
            });

            view.HorizontalOptions = LayoutOptions.CenterAndExpand;
            view.VerticalOptions = LayoutOptions.CenterAndExpand;
            stack.Children.Add(view);

            absoluteLayout.Children.Add(stack);

            page.Content = absoluteLayout;

        }

        public static void CloseView(ContentPage page)
        {
            page.Content = (page.Content as AbsoluteLayout).Children[0];
        }
    }
}
