using ECS;
using UnityEngine;

namespace SwecorTestSavchenko
{
    public class ShootingSystem : GameSystem
    {
        private Transform playerBaseTransform;
        private bool isShooting;

        private void OnEnable()
        {
            Messenger.AddListener<IEntity>(GameEvent.ENTITY_CREATED, AddEntity);
            Messenger.AddListener<Vector3>(GameEvent.POINTER_DOWN, OnPointerDown);
            Messenger.AddListener<Vector3>(GameEvent.POINTER_UP, OnPointerUp);
            Messenger.AddListener<int>(GameEvent.CHANGE_ACTIVE_GUN, OnChangeActiveGun);
        }

        private void OnDisable()
        {
            Messenger.RemoveListener<IEntity>(GameEvent.ENTITY_CREATED, AddEntity);
            Messenger.RemoveListener<Vector3>(GameEvent.POINTER_DOWN, OnPointerDown);
            Messenger.RemoveListener<Vector3>(GameEvent.POINTER_UP, OnPointerUp);
            Messenger.RemoveListener<int>(GameEvent.CHANGE_ACTIVE_GUN, OnChangeActiveGun);
        }

        public override void AddEntity(IEntity entity)
        {
            if (entity.GetComponent<EntityID>().EntityType == EntityType.PlayerBase)
                playerBaseTransform = entity.GetComponent<Transform>();

            if (entity.GetComponent<Weapon>() == null)
                return;

            Entities.Add(entity);
        }

        private void OnPointerDown(Vector3 pointerPosition)
        {
            isShooting = true;
        }

        private void OnPointerUp(Vector3 pointerPosition)
        {
            isShooting = false;
        }

        private void OnChangeActiveGun(int gunIndex)
        {
            IEntity player = FindPlayerEntity();

            Weapon weapon = player.GetComponent<Weapon>();
            for (int i = 0; i < weapon.Guns.Count; ++i)
            {
                if (weapon.Guns[i].IsActive == true)
                    weapon.Guns[i].IsActive = false;

                if (i == gunIndex)
                {
                    weapon.Guns[i].IsActive = true;
                    weapon.ActiveGun = weapon.Guns[i];
                }
            }
        }

        private IEntity FindPlayerEntity()
        {
            foreach (IEntity entity in Entities)
            {
                if (entity.GetComponent<EntityID>().EntityType == EntityType.PlayerBase)
                    return entity;
            }

            return default;
        }

        public override void UpdateSystem(float timeStamp)
        {
            foreach(IEntity entity in Entities)
            {
                foreach(Gun gun in entity.GetComponent<Weapon>().Guns)
                {
                    gun.CurrentCooldown += timeStamp;

                    if (gun.CurrentCooldown >= gun.Cooldown && gun.IsActive)
                    {
                        if (isShooting)
                        {
                            gun.CurrentCooldown = 0f;

                            Shoot(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                        }
                    }
                }
            }
        }

        private void Shoot(Vector3 pointerPosition)
        {
            float targetAngle = Vector2.SignedAngle(Vector2.right, pointerPosition - playerBaseTransform.position);

            Messenger.Broadcast<ShellType, Vector3, Vector3>(
                GameEvent.SHOT_MADE,
                FindPlayerEntity().GetComponent<Weapon>().ActiveGun.ShootingShellType,
                playerBaseTransform.position,
                new Vector3(0, 0, targetAngle));
        }

        private void ResetAllCooldowns()
        {
            foreach (IEntity entity in Entities)
            {
                foreach (Gun gun in entity.GetComponent<Weapon>().Guns)
                {
                    gun.CurrentCooldown = gun.Cooldown;
                }
            }
        }
    }
}