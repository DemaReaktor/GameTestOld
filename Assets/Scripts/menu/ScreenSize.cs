using UnityEngine;
using UnityEngine.UI;

public class ScreenSize : MonoBehaviour
{
    [SerializeField] private Vector2Int DefaultScreen;
    private void Start()
    {
        ChangeSizeAndPosition(transform as RectTransform);
    }
    private void ChangeSizeAndPosition(RectTransform transform)
    {
        if (transform.TryGetComponent(out RectTransform rectTransform))
        {
            rectTransform.sizeDelta = new Vector2(transform.sizeDelta.x / DefaultScreen.x * Screen.width, transform.sizeDelta.y / DefaultScreen.y * Screen.height);
            rectTransform.anchoredPosition = new Vector2(transform.anchoredPosition.x / DefaultScreen.x * Screen.width, transform.anchoredPosition.y / DefaultScreen.y * Screen.height);
        }

        if (transform.TryGetComponent(out Text text))
            text.fontSize = text.fontSize * Screen.width / DefaultScreen.x;

        foreach (RectTransform child in transform)
            ChangeSizeAndPosition(child);
    }
}
