using ChronoFlow.Client.Common.Controls.Menus;
using ChronoFlow.Client.Common.Localization;
using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.Common.MainData.UseCases.MainDataPage;

public partial class MainDataPage : ComponentBase
{
    private MenuItem? _selectedMenuItem;
    private List<MenuCategory> _menuCategories = [];

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
        _menuCategories = MenuProfiles
            .SelectMany(c => c.GetMenuConfiguration().MainDataMenuCategories)
            .Select(m => m.ToMenuCategory())
            .ToList();

        if (DomainObject != null && DomainObject is string domainObjectUriSegment)
        {
            var currentUri = $"main-data/{domainObjectUriSegment}";

            _selectedMenuItem = _menuCategories.SelectMany(c => c.Items).SingleOrDefault(m => m.Uri == currentUri);
            if (_selectedMenuItem == null)
                NavigationManager.NavigateTo("main-data");
        }
    }

    private string GetPageTitle()
    {
        return Localizer[_selectedMenuItem?.Title ?? "MainData"];
    }
}
