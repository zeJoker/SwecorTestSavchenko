namespace ECS
{
    public interface IEntity
    {
        public void AddComponent<T>(T component);
        public void RemoveComponent<T>();
        public T GetComponent<T>();
    }
}