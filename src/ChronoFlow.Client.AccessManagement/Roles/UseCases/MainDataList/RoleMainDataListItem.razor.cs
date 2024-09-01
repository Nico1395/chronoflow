using ChronoFlow.Client.AccessManagement.Roles.Entities;
using ChronoFlow.Client.Common.MainData.UseCases.MainDataList;
using ChronoFlow.Shared.Common.Objects.Extensions;

namespace ChronoFlow.Client.AccessManagement.Roles.UseCases.MainDataList;

public partial class RoleMainDataListItem : MainDataListItemComponentBase<RoleViewModel>
{
    private List<string> GetBottomDetails()
    {
        var description = Item.Description.Shorten(50);
        if (Item.Description?.Length > 50)
            description += "...";

        return [description];
    }
}
