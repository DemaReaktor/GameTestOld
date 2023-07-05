using System;
using UnityEngine;
using UnityEngine.EventSystems;
using GameArchitecture;
using GameArchitecture.Save;

public class Slider : MonoBehaviour , IPointerClickHandler
{
    [SerializeField] protected string Key;
    [SerializeField] protected RectTransform Field;
    [SerializeField] protected uint LevelsCount;

    public uint Value { get; private set; }

    private ISaveManager<SettingsConfiguration> saveManager;
    private float length;

    void Start()
    {
        if (Field is null)
            throw new Exception("Value should have ojbect");

        length = ((transform as RectTransform).sizeDelta.x - Field.sizeDelta.x) / (LevelsCount-1);

        if (Game.TryGetManager(out saveManager) && saveManager.TryGet(out string value, Key) && uint.TryParse(value,out uint intValue))
            SetValue(intValue);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SetValue((uint)((eventData.position.x - (transform as RectTransform).anchoredPosition.x - Field.sizeDelta.x/2)/ length));
    }

    private void SetValue(uint value) {
        if (value < 0)
            value = 0;
        if (value >= LevelsCount)
            value = LevelsCount-1;
        if (Field != null)
        {
            Field.anchoredPosition = new Vector2(Field.sizeDelta.x * 0.5f + length * value, Field.anchoredPosition.y);
            saveManager.Set(value.ToString(),Key);
            saveManager.Save();
        }
        Value = value;
    }
}
