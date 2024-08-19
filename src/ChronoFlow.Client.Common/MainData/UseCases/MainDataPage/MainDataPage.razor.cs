using ChronoFlow.Client.Common.Controls.Menus;
using ChronoFlow.Client.Common.Localization;
using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.Common.MainData.UseCases.MainDataPage;

public partial class MainDataPage : ComponentBase
{
    private MenuItem? _selectedMenuItem;
    private List<MenuItem> _menuItems = [];

    [Inject]
    private IEnumerable<IMainDataMenuProfile> MenuProfiles { get; set; } = null!;

    [Inject]
    private ILocalizer Localizer { get; set; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Parameter]
    public object? DomainObject { get; set; }

    protected override void OnInitialized()
    {
        _menuItems = MenuProfiles
            .SelectMany(c => c.GetMenuConfiguration().MainDataMenuItems)
            .Select(m => m.ToMenuItem())
            .ToList();

        if (DomainObject != null && DomainObject is string domainObjectUriSegment)
        {
            _selectedMenuItem = _menuItems.SingleOrDefault(m => m.Key == domainObjectUriSegment);
            if (_selectedMenuItem == null)
                NavigationManager.NavigateTo("main-data");
        }
    }

    private string GetPageTitle()
    {
        return Localizer[_selectedMenuItem?.Title ?? "MainData"];
    }
}
