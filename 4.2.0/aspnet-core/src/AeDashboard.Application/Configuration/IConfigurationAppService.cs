using System.Threading.Tasks;
using AeDashboard.Configuration.Dto;

namespace AeDashboard.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
