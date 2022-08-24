using System.Collections.Generic;
using UnityEngine;

namespace ECS
{
    public class Engine : MonoBehaviour
    {
        private List<IGameSystem> systems = new();

        public void AddSystem(IGameSystem system)
        {
            systems.Add(system);
        }

        public void RemoveSystem(IGameSystem system)
        {
            systems.Remove(system);
        }

        void Update()
        {
            for (int i = 0; i < systems.Count; ++i)
            {
                systems[i].UpdateSystem(Time.deltaTime);
            }
        }
    }
}
