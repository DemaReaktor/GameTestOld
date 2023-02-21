using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace KeyCodeController
{
    public class KeyCodeSetter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public string Key;
        public UnityEvent<KeyCode, string> OnKeyCodeUp;

        public void OnPointerEnter(PointerEventData eventData) 
        { 
            StartCoroutine("CheckKeyCode");
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            StopAllCoroutines();
        }
        private void OnDisable()
        {
            StopAllCoroutines();
        }
        private void OnDestroy()
        {
            StopAllCoroutines();
        }
        private IEnumerator CheckKeyCode()
        {
            while (true)
            {
                if (Input.anyKeyDown)
                    foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
                        if (Input.GetKeyDown(keyCode))
                            OnKeyCodeUp?.Invoke(keyCode, Key);

                yield return null;
            }
        }
    }

}
