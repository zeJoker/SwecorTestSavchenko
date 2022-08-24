using ECS;

namespace SwecorTestSavchenko
{
    public class DamageDealSystem : GameSystem
    {
        private void OnEnable()
        {
            Messenger.AddListener<IEntity, IEntity>(GameEvent.SHELL_HITTED_ENEMY_SHIP, OnShellHittedEnemyShip);
        }

        private void OnDisable()
        {
            Messenger.RemoveListener<IEntity, IEntity>(GameEvent.SHELL_HITTED_ENEMY_SHIP, OnShellHittedEnemyShip);
        }

        private void OnShellHittedEnemyShip(IEntity shell, IEntity enemyShip)
        {
            DealDamage(enemyShip, shell.GetComponent<Damage>().DamageAmmount);

            Messenger.Broadcast<IEntity>(GameEvent.ADD_ENTITY_TO_REMOVE_LIST, shell);
        }

        private void DealDamage(IEntity entity, int damage)
        {
            Health health = entity.GetComponent<Health>();
            health.CurrentHealth -= damage;

            Messenger.Broadcast<IEntity, int>(GameEvent.DAMAGE_DEALED, entity, damage);

            if (health.CurrentHealth <= 0)
            {
                Messenger.Broadcast<IEntity>(GameEvent.ADD_ENTITY_TO_REMOVE_LIST, entity);
            }
        }
    }
}