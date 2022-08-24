using ECS;
using System.Collections.Generic;
using UnityEngine;

namespace SwecorTestSavchenko
{
    public class HealthBarSystem : GameSystem
    {
        [SerializeField] private GameObject healthBarPrefab;
        [SerializeField] private Transform healthBarsContainer;
        [SerializeField] private Vector3 healthBarsOffset;
        private Dictionary<IEntity, GameObject> healthBars = new();

        private void OnEnable()
        {
            Messenger.AddListener<IEntity>(GameEvent.ENTITY_CREATED, AddEntity);
            Messenger.AddListener<IEntity>(GameEvent.REMOVE_ENTITY, RemoveEntity);
            Messenger.AddListener<IEntity, int>(GameEvent.DAMAGE_DEALED, OnDamageDealed);
        }

        private void OnDisable()
        {
            Messenger.RemoveListener<IEntity>(GameEvent.ENTITY_CREATED, AddEntity);
            Messenger.RemoveListener<IEntity>(GameEvent.REMOVE_ENTITY, RemoveEntity);
            Messenger.RemoveListener<IEntity, int>(GameEvent.DAMAGE_DEALED, OnDamageDealed);
        }

        private void OnDamageDealed(IEntity entity, int damage)
        {
            if (healthBars.ContainsKey(entity) == false)
            {
                healthBars.Add(entity, Instantiate(
                    healthBarPrefab,
                    Camera.main.WorldToScreenPoint(entity.GetComponent<Transform>().position + healthBarsOffset),
                    Quaternion.identity,
                    healthBarsContainer));
            }

            UpdateHealthBarValue(entity);
        }

        private void UpdateHealthBarValue(IEntity entity)
        {
            Health health = entity.GetComponent<Health>();
            healthBars[entity].GetComponent<HealthBar>().FrontImage.fillAmount = (float)health.CurrentHealth / health.MaximumHealth;
        }

        public override void AddEntity(IEntity entity)
        {
            if (entity.GetComponent<Health>() == null)
                return;

            base.AddEntity(entity);
        }

        public override void RemoveEntity(IEntity entity)
        {
            if (healthBars.ContainsKey(entity))
            {
                Destroy(healthBars[entity]);
                healthBars.Remove(entity);
            }

            base.RemoveEntity(entity);
        }

        public override void UpdateSystem(float timeStamp)
        {
            foreach (KeyValuePair<IEntity, GameObject> healthBar in healthBars)
            {
                healthBar.Value.transform.position = Camera.main.WorldToScreenPoint(healthBar.Key.GetComponent<Transform>().position + healthBarsOffset);
            }
        }
    }
}