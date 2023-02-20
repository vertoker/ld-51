using System;
using UniRx;

namespace Interfaces
{
    public interface IModel : IDisposable
    {
        IObservable<Unit> GetDisposedAsObservable();
    }
}