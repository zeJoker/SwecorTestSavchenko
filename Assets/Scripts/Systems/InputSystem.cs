using ECS;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SwecorTestSavchenko
{
    public class InputSystem : GameSystem
    {
        public override void UpdateSystem(float timeStamp)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject() == false)
                {
                    Vector3 pointerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    pointerPosition.z = 0;

                    Messenger.Broadcast<Vector3>(GameEvent.POINTER_DOWN, pointerPosition);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (EventSystem.current.IsPointerOverGameObject() == false)
                {
                    Vector3 pointerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    pointerPosition.z = 0;

                    Messenger.Broadcast<Vector3>(GameEvent.POINTER_UP, pointerPosition);
                }
            }
        }
    }
}