namespace Script {
    public interface IRuntimeSet<T> {
        void Initialize();
        void AddToList(T item);
        void RemoveFromList(T item);
    }
}