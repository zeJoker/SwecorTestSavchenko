using ECS;
using System.Collections.Generic;
using UnityEngine;

namespace SwecorTestSavchenko
{
    public class MovementSystem : GameSystem
    {
        private List<MovementData> movementDatas = new();

        private void OnEnable()
        {
            Messenger.AddListener<IEntity>(GameEvent.ENTITY_CREATED, OnEntityCreated);
            Messenger.AddListener<IEntity>(GameEvent.REMOVE_ENTITY, RemoveEntity);
        }

        private void OnDisable()
        {
            Messenger.RemoveListener<IEntity>(GameEvent.ENTITY_CREATED, OnEntityCreated);
            Messenger.RemoveListener<IEntity>(GameEvent.REMOVE_ENTITY, RemoveEntity);
        }

        private void OnEntityCreated(IEntity entity)
        {
            Movement movement = entity.GetComponent<Movement>();

            if (movement != null)
            {
                AddEntity(entity);
            }
        }

        public override void AddEntity(IEntity entity)
        {
            base.AddEntity(entity);

            movementDatas.Add(new MovementData() { Movement = entity.GetComponent<Movement>(), Transform = entity.GetComponent<Transform>() });
        }

        public override void RemoveEntity(IEntity entity)
        {
            int index = Entities.IndexOf(entity);

            movementDatas.RemoveAt(index);

            base.RemoveEntity(entity);
        }

        public override void UpdateSystem(float timeStamp)
        {
            foreach (MovementData movementData in movementDatas)
            {
                Vector2 newPosition = movementData.Transform.position;
                newPosition.x += Mathf.Cos(movementData.Transform.eulerAngles.z * Mathf.Deg2Rad) * movementData.Movement.Speed * timeStamp;
                newPosition.y += Mathf.Sin(movementData.Transform.eulerAngles.z * Mathf.Deg2Rad) * movementData.Movement.Speed * timeStamp;
                movementData.Transform.position = newPosition;
            }

            Messenger.Broadcast(GameEvent.MOVEMENT_DONE);
        }
    }

    public struct MovementData
    {
        public Transform Transform;
        public Movement Movement;
    }
}