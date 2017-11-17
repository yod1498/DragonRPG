using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.CameraUI
{
    public class SomeObserver : MonoBehaviour
    {

        CameraRaycaster cameraRaycaster;

        // Use this for initialization
        void Awake()
        {
            cameraRaycaster = GetComponent<CameraRaycaster>();
            //cameraRaycaster.layerChangeObservers += SomeHandlingFunction;
        }

        // Update is called once per frame
        void SomeHandlingFunction()
        {
            print("handled from elsewhere");
        }
    }
}
