using System;
using System.Collections.Generic;
using Interfaces;
using UniRx;

namespace Models
{
    public class StorageBase<TItem> : IStorage<TItem>, IDisposable where TItem : IIdentifier, IDisposable
    {
        private readonly Dictionary<string, TItem> _items;
        private readonly Subject<TItem> _itemAdded;
        private readonly Subject<TItem> _itemRemoved;

        public StorageBase()
        {
            _items = new Dictionary<string, TItem>();
            _itemAdded = new Subject<TItem>();
            _itemRemoved = new Subject<TItem>();
        }

        public bool Add(TItem item)
        {
            if (_items.ContainsKey(item.Id)) return false;
            _items.Add(item.Id, item);
            _itemAdded.OnNext(item);
            return true;
        }

        public bool Remove(string id, bool dispose = true)
        {
            if (!_items.TryGetValue(id, out var item)) return false;
            _itemRemoved.OnNext(item);
            if (dispose) item.Dispose();
            _items.Remove(id);
            return true;
        }

        public bool TryGetItem(string id, out TItem result)
        {
            return _items.TryGetValue(id, out result);
        }

        public bool Contains(string id)
        {
            return _items.ContainsKey(id);
        }
        public IObservable<TItem> GetItemAddedAsObservable()
        {
            return _itemAdded.AsObservable();
        }

        public IObservable<TItem> GetItemRemovedAsObservable()
        {
            return _itemRemoved.AsObservable();
        }

        public IEnumerable<TItem> GetItems()
        {
            return _items.Values;
        }

        public virtual void Dispose()
        {
            foreach (var item in _items.Values)
            {
                item?.Dispose();
            }
            _items.Clear();
            _itemAdded.Dispose();
            _itemRemoved.Dispose();
        }
    }
}