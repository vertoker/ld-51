using System;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IStorage<TItem> where TItem : IIdentifier, IDisposable
    {
        bool Add(TItem item);
        bool Remove(string id, bool dispose = true);
        bool TryGetItem(string id, out TItem result);
        bool Contains(string id);
        IObservable<TItem> GetItemAddedAsObservable();
        IObservable<TItem> GetItemRemovedAsObservable();
        IEnumerable<TItem> GetItems();
    }
}