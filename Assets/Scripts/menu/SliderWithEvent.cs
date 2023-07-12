using System.Diagnostics;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;

public class SliderWithEvent : Slider
{
    public UnityEvent<float> OnChangeValue;

    public override void OnPointerMove(PointerEventData eventData)
    {
        base.OnPointerMove(eventData);

        OnChangeValue?.Invoke(Value*0.01f);
    }

}
