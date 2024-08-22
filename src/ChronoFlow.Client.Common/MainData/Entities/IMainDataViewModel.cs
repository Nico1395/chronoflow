namespace ChronoFlow.Client.Common.MainData.Entities;

public interface IMainDataViewModel
{
    public Guid Id { get; init; }
    public DateTime Created { get; set; }
    public DateTime LastChanged { get; set; }
}
