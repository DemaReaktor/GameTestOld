using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using GameArchitecture;
using GameArchitecture.Save;

public class Slider : MonoBehaviour , IPointerClickHandler
{
    public string Key;
    public RectTransform Value;
    public uint LevelsCount;

    private ISaveManager<SettingsConfiguration> saveManager;
    float length;

    void Start()
    {
        if (Value is null)
            throw new Exception("Value should have ojbect");

        length = ((transform as RectTransform).sizeDelta.x - Value.sizeDelta.x) / (LevelsCount-1);

        if (Game.TryGetManager(out saveManager) && saveManager.TryGet(out string value, Key) && uint.TryParse(value,out uint intValue))
            SetValue(intValue);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SetValue((uint)((eventData.position.x - (transform as RectTransform).anchoredPosition.x - Value.sizeDelta.x/2 /*+ length/2*/)/ length));
    }

    private void SetValue(uint value) {
        if (value < 0)
            value = 0;
        if (value >= LevelsCount)
            value = LevelsCount-1;
        if (Value != null)
        {
            Value.anchoredPosition = new Vector2(Value.sizeDelta.x * 0.5f + length * value, Value.anchoredPosition.y);
            saveManager.Set(value.ToString(),Key);
            saveManager.Save();
        }
    }
}
