using System;

namespace Utils.Events
{
    public class Event<T>
    {
        protected Action<T> listeners;

        public void AddListener(Action<T> listener) => listeners += listener;
        public void RemoveListener(Action<T> listener) => listeners -= listener;
        
    }

    public class Event
    {
        protected Action listeners;
    
        public void AddListener(Action listener) => listeners += listener;
        public void RemoveListener(Action listener) => listeners -= listener;
    }
}