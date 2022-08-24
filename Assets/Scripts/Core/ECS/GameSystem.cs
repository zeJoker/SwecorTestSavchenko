using System.Collections.Generic;
using UnityEngine;

namespace ECS
{
    public class GameSystem : MonoBehaviour, IGameSystem
    {
        protected List<IEntity> Entities = new();

        virtual public void UpdateSystem(float timeStamp)
        {

        }

        virtual public void AddEntity(IEntity entity)
        {
            if (!Entities.Contains(entity))
                Entities.Add(entity);
        }

        virtual public void RemoveEntity(IEntity entity)
        {
            if (Entities.Contains(entity))
                Entities.Remove(entity);
        }
    }
}