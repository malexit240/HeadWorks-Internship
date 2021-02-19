using System.Globalization;

namespace HWInternshipProject.Services
{
    public interface ISettingsManager
    {
        Theme Theme { get; set; }
        ProfilesListOrderBy ProfilesListOrderBy { get; set; }
        CultureInfo CurrentCultureInfo { get; set; }
    }


}
