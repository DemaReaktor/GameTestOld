using UnityEngine;
using GameArchitecture;
using GameArchitecture.Save;

namespace KeyCodeController
{
    public class KeyCodeSaver : MonoBehaviour
    {
        private ISaveManager<SettingsConfiguration> saveManager;
        void Start()
        {
            if (Game.TryGetManager(out ISaveManager<SettingsConfiguration> saveManager))
                this.saveManager = saveManager;

            KeyCodeEventSet(transform);
        }

        public void SetKeyCode(KeyCode value, string key)
        {
            if (saveManager != null)
            {
                saveManager.Set(value, key);
                saveManager.Save();

                KeyCodeEventSet(transform,key,value);
            }
        }
        private void KeyCodeEventSet(Transform transform,string key,KeyCode value) {
            if (transform.TryGetComponent(out KeyCodeEvent keyCodeEvent) && keyCodeEvent.Key == key)
                keyCodeEvent.OnKeyCode?.Invoke(value.ToString());

            foreach (Transform child in transform)
                KeyCodeEventSet(child,key,value);
        }
        private void KeyCodeEventSet(Transform transform)
        {
            if (transform.TryGetComponent(out KeyCodeEvent keyCodeEvent) && saveManager.TryGet(out string value,keyCodeEvent.Key))
                keyCodeEvent.OnKeyCode?.Invoke(value);

            foreach (Transform child in transform)
                KeyCodeEventSet(child);
        }
    }
}
