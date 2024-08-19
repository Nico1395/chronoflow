namespace ChronoFlow.Client.Common.MainData.UseCases.MainDataPage;

public interface IMainDataMenuProfile
{
    public void Configure(MainDataMenuConfiguration configuration);
    internal MainDataMenuConfiguration GetMenuConfiguration();
}
