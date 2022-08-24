using ECS;
using UnityEngine;

namespace SwecorTestSavchenko
{
    public class OutOfScreenCheckSystem : GameSystem
    {
        [SerializeField] private float verticalBorder;
        [SerializeField] private float horizontalBorder;

        private void OnEnable()
        {
            Messenger.AddListener<IEntity>(GameEvent.ENTITY_CREATED, AddEntity);
            Messenger.AddListener<IEntity>(GameEvent.REMOVE_ENTITY, RemoveEntity);
            Messenger.AddListener(GameEvent.COLLISIONS_CHECKED, OnCollisionsChecked);
        }

        private void OnDisable()
        {
            Messenger.RemoveListener<IEntity>(GameEvent.ENTITY_CREATED, AddEntity);
            Messenger.RemoveListener<IEntity>(GameEvent.REMOVE_ENTITY, RemoveEntity);
            Messenger.RemoveListener(GameEvent.COLLISIONS_CHECKED, OnCollisionsChecked);
        }

        private void OnCollisionsChecked()
        {
            foreach (IEntity entity in Entities)
            {
                Vector3 position = entity.GetComponent<Transform>().position;

                if (position.x > horizontalBorder || position.x < -horizontalBorder || position.y > verticalBorder || position.y < -verticalBorder)
                    Messenger.Broadcast<IEntity>(GameEvent.ADD_ENTITY_TO_REMOVE_LIST, entity);
            }
        }
    }
}