using ECS;
using UnityEngine;

namespace SwecorTestSavchenko
{
    public class EnemySpawnSystem : GameSystem
    {
        [SerializeField] private float SpawnEverySeconds;
        private float currentCooldown;
        private bool isActive;

        private void OnEnable()
        {
            Messenger.AddListener(GameEvent.START_GAME, OnStartGame);
        }

        private void OnDisable()
        {
            Messenger.RemoveListener(GameEvent.START_GAME, OnStartGame);
        }

        private void OnStartGame()
        {
            isActive = true;
        }

        public override void UpdateSystem(float timeStamp)
        {
            if (isActive == false)
                return;

            currentCooldown += timeStamp;

            if (currentCooldown >= SpawnEverySeconds)
            {
                SpawnNewEnemy();

                currentCooldown = 0f;
            }
        }

        private void SpawnNewEnemy()
        {
            Vector3 newPosition = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(2f, 4f), 0f);

            Messenger.Broadcast<Vector3, Vector3>(GameEvent.CREATE_ENEMY_SHIP, newPosition, new Vector3(0, 0, -90));
        }
    }
}