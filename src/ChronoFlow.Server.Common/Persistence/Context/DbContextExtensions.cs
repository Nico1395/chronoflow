using Microsoft.EntityFrameworkCore;

namespace ChronoFlow.Server.Common.Persistence.Context;

public static class DbContextExtensions
{
    public static void SyncCollections<T>(this DbContext context, ICollection<T> existingItems, ICollection<T> updatedItems, Func<T, object> keySelector)
    {
        var updatedItemKeys = updatedItems.Select(keySelector).ToHashSet();

        // Add or update items
        foreach (var updatedItem in updatedItems)
        {
            var existingItem = existingItems.FirstOrDefault(e => keySelector(e).Equals(keySelector(updatedItem)));
            if (existingItem == null)
                existingItems.Add(updatedItem);
            else
                context.Entry(existingItem).State = EntityState.Unchanged;
        }

        // Remove items that are no longer in the updated collection
        foreach (var existingItem in existingItems.ToList())
        {
            if (!updatedItemKeys.Contains(keySelector(existingItem)))
                existingItems.Remove(existingItem);
        }
    }
}
