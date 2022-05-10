using System;

namespace Observer
{
    public interface ISubject<T>
    {
        // Attach an observer to the subject.
        void Subscribe(IObserver<T> observer);

        // Detach an observer from the subject.
        void Unsubscribe(IObserver<T> observer);

        // Notify all observers about an event.
        void GetTemperature();
    }
}