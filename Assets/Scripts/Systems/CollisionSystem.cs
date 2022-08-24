using ECS;
using System.Collections.Generic;
using UnityEngine;

namespace SwecorTestSavchenko
{
    public class CollisionSystem : GameSystem
    {
        private List<ColliderData> enemyShipColliderDatas = new();
        private List<ColliderData> shellColliderDatas = new();

        private void OnEnable()
        {
            Messenger.AddListener<IEntity>(GameEvent.ENTITY_CREATED, OnEntityCreated);
            Messenger.AddListener<IEntity>(GameEvent.REMOVE_ENTITY, RemoveEntity);
            Messenger.AddListener(GameEvent.MOVEMENT_DONE, OnMovementDone);
        }

        private void OnDisable()
        {
            Messenger.RemoveListener<IEntity>(GameEvent.ENTITY_CREATED, OnEntityCreated);
            Messenger.RemoveListener<IEntity>(GameEvent.REMOVE_ENTITY, RemoveEntity);
            Messenger.RemoveListener(GameEvent.MOVEMENT_DONE, OnMovementDone);
        }

        public override void RemoveEntity(IEntity entity)
        {
            List<ColliderData> list = entity.GetComponent<EntityID>().EntityType == EntityType.EnemyShip ? enemyShipColliderDatas : shellColliderDatas;

            foreach (ColliderData colliderData in list)
            {
                if (colliderData.Entity == entity)
                {
                    list.Remove(colliderData);
                    break;
                }
            }
        }

        private void OnEntityCreated(IEntity entity)
        {
            EntityID id = entity.GetComponent<EntityID>();
            if (id.EntityType == EntityType.EnemyShip)
            {
                enemyShipColliderDatas.Add(new ColliderData() { Entity = entity, Collider = entity.GetComponent<Collider>(), Transform = entity.GetComponent<Transform>() });
            }
            if (id.EntityType == EntityType.Shell)
            {
                shellColliderDatas.Add(new ColliderData() { Entity = entity, Collider = entity.GetComponent<Collider>(), Transform = entity.GetComponent<Transform>() });
            }
        }

        private void OnMovementDone()
        {
            foreach (ColliderData shellColliderData in shellColliderDatas)
            {
                foreach (ColliderData enemyShipColliderData in enemyShipColliderDatas)
                {
                    if (CheckCollision(shellColliderData, enemyShipColliderData) == true)
                    {
                        Messenger.Broadcast<IEntity, IEntity>(GameEvent.SHELL_HITTED_ENEMY_SHIP, shellColliderData.Entity, enemyShipColliderData.Entity);
                    }
                }
            }

            Messenger.Broadcast(GameEvent.COLLISIONS_CHECKED);
        }

        private bool CheckCollision(ColliderData first, ColliderData second)
        {
            if (CheckIntersection(first.Transform.position, second) == true)
                return true;

            return false;
        }

        private bool CheckIntersection(Vector3 point, ColliderData colliderData)
        {
            if (point.x >= colliderData.Transform.position.x - colliderData.Collider.Width * 0.5f &&
                point.x <= colliderData.Transform.position.x + colliderData.Collider.Width * 0.5f &&
                point.y >= colliderData.Transform.position.y - colliderData.Collider.Height * 0.5f &&
                point.y <= colliderData.Transform.position.y + colliderData.Collider.Height * 0.5f)
                return true;

            return false;
        }
    }

    public struct ColliderData
    {
        public IEntity Entity;
        public Collider Collider;
        public Transform Transform;
    }
}