using UnityEngine;
using UnityEngine.Events;

public class KeyChecker : MonoBehaviour
{
    public KeyCode Value;
    [SerializeField] private UnityEvent OnKeyPressUp;

    void Update()
    {
        if (Input.GetKeyUp(Value))
            OnKeyPressUp?.Invoke();
    }
}
