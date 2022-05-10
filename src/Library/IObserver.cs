using System;

namespace Observer
{
    public interface IObserver<T>
    {
        // Receive update from subject
        void Update(T value);
    }
}