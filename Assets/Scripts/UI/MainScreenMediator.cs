using UnityEngine;

namespace SwecorTestSavchenko
{
    public class MainScreenMediator : MonoBehaviour
    {
        public GameObject StartGameButton;

        public void StartGame()
        {
            StartGameButton.SetActive(false);

            Messenger.Broadcast(GameEvent.START_GAME);
        }
    }
}