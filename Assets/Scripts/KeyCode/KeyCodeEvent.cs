using UnityEngine;
using UnityEngine.Events;

namespace KeyCodeController
{
    public class KeyCodeEvent : MonoBehaviour
    {
        public string Key;
        public UnityEvent<string> OnKeyCode;
    }
}
