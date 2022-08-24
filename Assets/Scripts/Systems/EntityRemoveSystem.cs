using ECS;
using System.Collections.Generic;
using UnityEngine;

namespace SwecorTestSavchenko
{
    public class EntityRemoveSystem : GameSystem
    {
        private List<IEntity> entitiesToRemove = new();

        private void OnEnable()
        {
            Messenger.AddListener<IEntity>(GameEvent.ADD_ENTITY_TO_REMOVE_LIST, OnAddEntityToRemoveList);
            Messenger.AddListener(GameEvent.COLLISIONS_CHECKED, OnCollisionsChecked);
        }

        private void OnDisable()
        {
            Messenger.RemoveListener<IEntity>(GameEvent.ADD_ENTITY_TO_REMOVE_LIST, OnAddEntityToRemoveList);
            Messenger.RemoveListener(GameEvent.COLLISIONS_CHECKED, OnCollisionsChecked);
        }

        private void OnAddEntityToRemoveList(IEntity entityToRemove)
        {
            entitiesToRemove.Add(entityToRemove);
        }

        private void OnCollisionsChecked()
        {
            for (int i = entitiesToRemove.Count - 1; i >= 0; --i)
            {
                IEntity entity = entitiesToRemove[i];

                Messenger.Broadcast<IEntity>(GameEvent.REMOVE_ENTITY, entity);

                Destroy(entity.GetComponent<Transform>().gameObject);
                entitiesToRemove.Remove(entity);
                entity = null;
            }
        }
    }
}