namespace ChronoFlow.Client.Common.MainData.UseCases.MainDataPage.Configuration;

public sealed class MainDataMenuConfiguration
{
    internal List<MainDataMenuCategory> MainDataMenuCategories { get; } = [];

    public MainDataMenuConfiguration AddCategory(Action<MainDataMenuCategoryBuilder> builderAction)
    {
        var builder = new MainDataMenuCategoryBuilder();
        builderAction.Invoke(builder);

        MainDataMenuCategories.Add(builder.Build());
        return this;
    }
}
