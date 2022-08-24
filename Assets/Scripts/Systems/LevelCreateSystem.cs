using ECS;
using UnityEngine;

namespace SwecorTestSavchenko
{
    public class LevelCreateSystem : GameSystem
    {
        public Vector3 playerBasePosition;

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
            Messenger.Broadcast<Vector3, Vector3>(GameEvent.CREATE_PLAYER_BASE, playerBasePosition, new Vector3(0, 0, 0));
        }
    }
}