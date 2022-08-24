using System;
using System.Collections.Generic;

namespace ECS
{
    public class Entity : IEntity
    {
        protected Dictionary<Type, object> Components = new();

        public void AddComponent<T>(T component)
        {
            Type type = typeof(T);

            if (Components.ContainsKey(type) == false)
            {
                Components.Add(type, component);
            }
        }

        public void RemoveComponent<T>()
        {
            Components.Remove(typeof(T));
        }

        public T GetComponent<T>()
        {
            if (Components.ContainsKey(typeof(T)))
                return (T)Components[typeof(T)];
            else
                return default;
        }
    }
}