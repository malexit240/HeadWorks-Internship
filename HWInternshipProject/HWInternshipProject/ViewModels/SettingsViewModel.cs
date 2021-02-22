using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using Prism.Navigation;
using HWInternshipProject.Services.Settings;

namespace HWInternshipProject.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        ISettingsManager _settingsManager;

        public bool OrderBy_NickName
        {
            get => _settingsManager.ProfilesListOrderBy == ProfilesListOrderBy.NickName;
            set
            {
                if (value)
                    _settingsManager.ProfilesListOrderBy = ProfilesListOrderBy.NickName;
            }
        }

        public bool OrderBy_Name
        {
            get => _settingsManager.ProfilesListOrderBy == ProfilesListOrderBy.Name;
            set
            {
                if (value)
                    _settingsManager.ProfilesListOrderBy = ProfilesListOrderBy.Name;
            }
        }

        public bool OrderBy_CreationTime
        {
            get => _settingsManager.ProfilesListOrderBy == ProfilesListOrderBy.CreationTime;
            set
            {
                if (value)
                    _settingsManager.ProfilesListOrderBy = ProfilesListOrderBy.CreationTime;
            }
        }

        public bool IsLightTheme
        {
            get => _settingsManager.Theme == Theme.Light;
            set { _settingsManager.Theme = value ? Theme.Light : Theme.Dark; }
        }

        public bool IsEn_US
        {
            get => _settingsManager.CurrentCultureInfo.Name == "en-US";
            set
            {
                if (value)
                {
                    _settingsManager.CurrentCultureInfo = new CultureInfo("en-US");
                }
            }
        }

        public bool IsRu_RU
        {
            get => _settingsManager.CurrentCultureInfo.Name == "ru-RU";
            set
            {
                if (value) _settingsManager.CurrentCultureInfo = new CultureInfo("ru-RU");
            }
        }

        public SettingsViewModel(INavigationService navigationService, ISettingsManager settingsManager) :
            base(navigationService)
        {
            _settingsManager = settingsManager;
        }
    }
}
