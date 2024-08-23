namespace ChronoFlow.Client.Common.MainData.UseCases.MainDataPage.Configuration;

public abstract class MainDataMenuProfileBase : IMainDataMenuProfile
{
    public abstract void Configure(MainDataMenuConfiguration configuration);

    public MainDataMenuConfiguration GetMenuConfiguration()
    {
        var menuConfiguration = new MainDataMenuConfiguration();

        Configure(menuConfiguration);
        return menuConfiguration;
    }
}
