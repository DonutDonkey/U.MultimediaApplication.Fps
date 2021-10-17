namespace Script.Mono.Listeners {
    public interface IListener<T> {
        void OnEventRaised(T item);
    }
    public interface IListener {
        void OnEventRaised();
    }
}
