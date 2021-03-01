using System;
using System.Globalization;
using Xamarin.Essentials;
using Xamarin.Forms;
using HWInternshipProject.Models;

namespace HWInternshipProject.Services.Settings
{

    public class SettingsManager : ISettingsManager
    {
        public Theme Theme
        {
            get => (Theme)Preferences.Get(nameof(Theme), (int)Theme.Light);
            set
            {
                Preferences.Set(nameof(Theme), (int)value);
                App.Current.Resources.MergedDictionaries.Clear();
                if (value == Theme.Light)
                    App.Current.Resources.MergedDictionaries.Add(new LightTheme());
                else
                    App.Current.Resources.MergedDictionaries.Add(new DarkTheme());
            }
        }
        public ProfilesListOrderBy ProfilesListOrderBy
        {
            get => (ProfilesListOrderBy)Preferences.Get(nameof(ProfilesListOrderBy), (int)ProfilesListOrderBy.Name);
            set
            {
                Preferences.Set(nameof(ProfilesListOrderBy), (int)value);
            }
        }
        public CultureInfo CurrentCultureInfo
        {
            get => new CultureInfo(Preferences.Get(nameof(CurrentCultureInfo), CultureInfo.CurrentCulture.Name));
            set
            {
                Preferences.Set(nameof(CurrentCultureInfo), (string)value.Name);
                System.Globalization.CultureInfo.CurrentUICulture = value;
            }
        }
    }


}
