using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using GameArchitecture;
using GameArchitecture.Save;

[RequireComponent(typeof(RectTransform))]
public class Slider : MonoBehaviour, IPointerDownHandler, IPointerMoveHandler
{
    [SerializeField] protected string Key;
    [SerializeField] protected RectTransform Field;
    [SerializeField] protected uint LevelsCount;

    public uint Value { get; private set; }

    private ISaveManager<SettingsConfiguration> saveManager;
    private float length;
    private bool isMouseDown;
    private RectTransform rectTransform;

    void Start()
    {
        if (Field is null)
            throw new Exception("Value should have ojbect");

        rectTransform = transform as RectTransform;
        length = (rectTransform.sizeDelta.x - Field.sizeDelta.x) / (LevelsCount-1);

        if (Game.TryGetManager(out saveManager) && saveManager.TryGet(out string value, Key) && int.TryParse(value,out int intValue))
            SetValue(intValue);

        isMouseDown = false;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        isMouseDown = true;
        StartCoroutine(CheckMouseUp());
    }
    public virtual void OnPointerMove(PointerEventData eventData)
    {
        if(isMouseDown && Input.GetKey(KeyCode.Mouse0))
            SetValue((int)((eventData.position.x - rectTransform.anchoredPosition.x - Field.sizeDelta.x / 2) / length));
    }
    private IEnumerator CheckMouseUp()
    {
        while (!Input.GetKeyUp(KeyCode.Mouse0))
            yield return null;
            
        isMouseDown = false;
    }

    private void SetValue(int value) {
        if (value < 0)
            value = 0;
        if (value >= LevelsCount)
            value = (int)LevelsCount;
        if (Field != null)
        {
            Field.anchoredPosition = new Vector2(Field.sizeDelta.x * 0.5f + length * value, Field.anchoredPosition.y);
            saveManager.Set(value.ToString(),Key);
            saveManager.Save();
        }
        Value = (uint)value;
    }
}
