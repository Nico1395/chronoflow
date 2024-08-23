namespace ChronoFlow.Client.Common.MainData;

public interface ITimespanMessageCalculator
{
    public string? GetCreatedMessage(DateTime timestampCreated);
    public string? GetEditedMessage (DateTime timestampEdited);
}
