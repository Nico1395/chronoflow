namespace ChronoFlow.Client.Common.MainData.UseCases.MainDataPage.Configuration;

public interface IMainDataMenuProfile
{
    public void Configure(MainDataMenuConfiguration configuration);
    internal MainDataMenuConfiguration GetMenuConfiguration();
}
