using UnityEngine;
using UnityEngine.UI;

namespace SwecorTestSavchenko
{
    public class GameScreenMediator : MonoBehaviour
    {
        public Color ActiveButtonColor;
        public Image[] WeaponButtonImages;

        private int activeGunIndex = 0;

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
            foreach (Image image in WeaponButtonImages)
            {
                image.gameObject.SetActive(true);
            }
        }

        public void ChangeActiveGun(int gunIndex)
        {
            WeaponButtonImages[activeGunIndex].color = Color.white;
            activeGunIndex = gunIndex;
            WeaponButtonImages[activeGunIndex].color = ActiveButtonColor;

            Messenger.Broadcast<int>(GameEvent.CHANGE_ACTIVE_GUN, gunIndex);
        }
    }
}