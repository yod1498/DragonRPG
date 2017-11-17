using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.CameraUI
{
    [RequireComponent(typeof(CameraRaycaster))]
    public class CursorAffordance : MonoBehaviour
    {

        [SerializeField] Texture2D walkCursor = null;
        [SerializeField] Texture2D unknowCursor = null;
        [SerializeField] Texture2D targetCursor = null;
        Vector2 cursorHotspot = Vector2.zero;

        private CameraRaycaster cameraRaycaster;

        void Awake()
        {
            cameraRaycaster = GetComponent<CameraRaycaster>();
            cameraRaycaster.layerChangeObservers += OnDelegateCalled; //registering
        }

        void OnDelegateCalled(Layer newLayer)
        {

            //print ("OnDelegateCalled called");

            switch (newLayer)
            {
                case Layer.Walkable:
                    Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
                    break;
                case Layer.Enemy:
                    Cursor.SetCursor(targetCursor, cursorHotspot, CursorMode.Auto);
                    break;
                case Layer.RaycastEndStop:
                    Cursor.SetCursor(unknowCursor, cursorHotspot, CursorMode.Auto);
                    break;
                default:
                    Debug.LogError("Dont' know what curor");
                    return;
            }
        }
    }
}
