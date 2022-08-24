using ECS;
using UnityEngine;

namespace SwecorTestSavchenko
{
    public class GameInitializer : MonoBehaviour
    {
        private Engine engine;

        private void Awake()
        {
            Screen.SetResolution(540, 960, FullScreenMode.Windowed);

            engine = GetComponent<Engine>();
            engine.AddSystem(GetComponent<InputSystem>());
            engine.AddSystem(GetComponent<EnemySpawnSystem>());
            engine.AddSystem(GetComponent<MovementSystem>());
            engine.AddSystem(GetComponent<ShootingSystem>());
            engine.AddSystem(GetComponent<HealthBarSystem>());
        }
    }
}