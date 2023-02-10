using UnityEngine;
using UnityEngine.UI;

public class ScreenSize : MonoBehaviour
{
    [SerializeField] private Vector2Int DefaultScreen;
    private void Start()
    {
        ChangeSizeAndPosition(transform);
    }
    private void ChangeSizeAndPosition(Transform transform)
    {
        if (transform.TryGetComponent(out RectTransform rectTransform))
        {
            rectTransform.sizeDelta = new Vector2(transform.localScale.x / DefaultScreen.x * Screen.width, transform.localScale.y / DefaultScreen.y * Screen.height);
            rectTransform.localPosition = new Vector2(transform.localPosition.x / DefaultScreen.x * Screen.width, transform.localPosition.y / DefaultScreen.y * Screen.height);
        }

        if (transform.TryGetComponent(out Text text))
            text.fontSize = text.fontSize * Screen.width / DefaultScreen.x;

        foreach (Transform child in transform)
            ChangeSizeAndPosition(child);
    }
}
