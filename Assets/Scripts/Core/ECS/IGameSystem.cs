namespace ECS
{
    public interface IGameSystem
    {
        public void UpdateSystem(float timeStamp);
        public void AddEntity(IEntity entity);
        public void RemoveEntity(IEntity entity);
    }
}