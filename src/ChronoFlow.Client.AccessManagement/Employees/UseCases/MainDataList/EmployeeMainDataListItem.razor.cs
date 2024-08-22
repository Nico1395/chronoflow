using ChronoFlow.Client.AccessManagement.Employees.Entities;
using ChronoFlow.Client.Common.Localization;
using ChronoFlow.Client.Common.MainData;
using ChronoFlow.Client.Common.MainData.UseCases.MainDataList;
using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.AccessManagement.Employees.UseCases.MainDataList;

public partial class EmployeeMainDataListItem : MainDataListItemTemplate<EmployeeViewModel>
{
    [Inject]
    private ILocalizer Localizer { get; set; } = null!;

    [Inject]
    private ITimespanMessageCalculator TimespanMessageCalculator { get; set; } = null!;

    private string GetRedirectUri()
    {
        return $"main-data/employees/{Item.Id}";
    }

    private string GetTitle()
    {
        return Item.Name.GetFullName();
    }

    private string GetDescription()
    {
        List<string> descriptionDetails = [];
        var createdMessage = TimespanMessageCalculator.GetCreatedMessage(Item.Created);
        var editedMessage = TimespanMessageCalculator.GetEditedMessage(Item.LastChanged);

        if (createdMessage != null)
            descriptionDetails.Add(createdMessage);

        if (editedMessage != null)
            descriptionDetails.Add(editedMessage);

        var primaryEmail = Item.Emails.SingleOrDefault(e => e.IsPrimary) ?? Item.Emails.FirstOrDefault();
        if (primaryEmail != null)
            descriptionDetails.Add(primaryEmail.Email);

        return string.Join(" • ", descriptionDetails);
    }
}
