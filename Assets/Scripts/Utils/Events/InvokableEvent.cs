namespace Utils.Events
{
    public class InvokableEvent<T> : Event<T>
    {
        public void Invoke(T param) => listeners?.Invoke(param);
    }
    
    public class InvokableEvent : Event
    {
        public void Invoke() => listeners?.Invoke();
    }
}