using ECS;
using UnityEngine;

namespace SwecorTestSavchenko
{
    public class EntityFactorySystem : GameSystem
    {
        [SerializeField] private GameObject[] playerBasePrefabs;
        [SerializeField] private GameObject[] enemyShipPrefabs;
        [SerializeField] private GameObject[] shellsPrefabs;
        [SerializeField] private Transform battlefieldContainer;

        private void OnEnable()
        {
            Messenger.AddListener<ShellType, Vector3, Vector3>(GameEvent.SHOT_MADE, OnShotMade);
            Messenger.AddListener<Vector3, Vector3>(GameEvent.CREATE_ENEMY_SHIP, OnCreateEnemyShip);
            Messenger.AddListener<Vector3, Vector3>(GameEvent.CREATE_PLAYER_BASE, OnCreatePlayerBase);
        }

        private void OnDisable()
        {
            Messenger.RemoveListener<ShellType, Vector3, Vector3>(GameEvent.SHOT_MADE, OnShotMade);
            Messenger.RemoveListener<Vector3, Vector3>(GameEvent.CREATE_ENEMY_SHIP, OnCreateEnemyShip);
            Messenger.RemoveListener<Vector3, Vector3>(GameEvent.CREATE_PLAYER_BASE, OnCreatePlayerBase);
        }

        private void OnCreateEnemyShip(Vector3 spawnPosition, Vector3 rotation)
        {
            IEntity entity = new Entity();

            GameObject newObject = Instantiate(enemyShipPrefabs[0], spawnPosition, Quaternion.identity, battlefieldContainer);
            newObject.name = enemyShipPrefabs[0].name;

            entity.AddComponent<Movement>(newObject.GetComponent<Movement>());
            entity.AddComponent<Transform>(newObject.transform);
            Collider collider = newObject.GetComponent<Collider>();
            collider.Height = newObject.GetComponent<SpriteRenderer>().bounds.size.y;
            collider.Width = newObject.GetComponent<SpriteRenderer>().bounds.size.x;
            entity.AddComponent<Collider>(collider);
            entity.AddComponent<EntityID>(newObject.GetComponent<EntityID>());
            entity.AddComponent<Health>(newObject.GetComponent<Health>());
            newObject.transform.eulerAngles = rotation;

            Messenger.Broadcast<IEntity>(GameEvent.ENTITY_CREATED, entity);
        }

        private void OnShotMade(ShellType shellType, Vector3 spawnPosition, Vector3 rotation)
        {
            IEntity entity = new Entity();

            GameObject newObject = Instantiate(shellsPrefabs[(int)shellType], spawnPosition, Quaternion.identity, battlefieldContainer);
            newObject.name = shellsPrefabs[(int)shellType].name;

            entity.AddComponent<Movement>(newObject.GetComponent<Movement>());
            entity.AddComponent<Transform>(newObject.transform);
            Collider collider = newObject.GetComponent<Collider>();
            collider.Height = newObject.GetComponent<SpriteRenderer>().bounds.size.y;
            collider.Width = newObject.GetComponent<SpriteRenderer>().bounds.size.x;
            entity.AddComponent<Collider>(GetComponent<Collider>());
            entity.AddComponent<EntityID>(newObject.GetComponent<EntityID>());
            entity.AddComponent<Damage>(newObject.GetComponent<Damage>());
            newObject.transform.eulerAngles = rotation;

            Messenger.Broadcast<IEntity>(GameEvent.ENTITY_CREATED, entity);
        }

        private void OnCreatePlayerBase(Vector3 spawnPosition, Vector3 rotation)
        {
            IEntity entity = new Entity();

            GameObject newObject = Instantiate(playerBasePrefabs[0], spawnPosition, Quaternion.identity, battlefieldContainer);
            newObject.name = playerBasePrefabs[0].name;

            entity.AddComponent<Transform>(newObject.transform);
            entity.AddComponent<EntityID>(newObject.GetComponent<EntityID>());
            entity.AddComponent<Weapon>(newObject.GetComponent<Weapon>());

            Messenger.Broadcast<IEntity>(GameEvent.ENTITY_CREATED, entity);
        }
    }

    public enum EntityType : int
    {
        PlayerBase,
        EnemyShip,
        Shell
    }
}