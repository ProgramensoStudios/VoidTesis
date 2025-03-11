using System;
using System.Collections;
using UnityEngine;

namespace WorldBehaviours
{
    public class TurboCircle : MonoBehaviour
    {
        [SerializeField] private int turboDuration;
        [SerializeField] private float multiplier;
        private float speed;
        private float beforeSpeed;
        private DisplayInputData displayInputData;
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.layer != 9) return;
            displayInputData = other.GetComponent<DisplayInputData>();
            displayInputData.Turbo(multiplier);
            displayInputData.turboCoroutine = null;

        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer != 9) return;
            displayInputData = other.GetComponent<DisplayInputData>();
            displayInputData.TurboExit();
        }
    }
}
