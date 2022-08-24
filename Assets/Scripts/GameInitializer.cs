using ECS;
using UnityEngine;

namespace SwecorTestSavchenko
{
    public class GameInitializer : MonoBehaviour
    {
        private Engine engine;

        private void Awake()
        {
            engine = GetComponent<Engine>();
            engine.AddSystem(GetComponent<InputSystem>());
            engine.AddSystem(GetComponent<EnemySpawnSystem>());
            engine.AddSystem(GetComponent<MovementSystem>());
            engine.AddSystem(GetComponent<ShootingSystem>());
            engine.AddSystem(GetComponent<HealthBarSystem>());
        }
    }
}