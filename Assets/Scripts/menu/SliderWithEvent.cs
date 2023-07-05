using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SliderWithEvent : Slider
{
    public UnityEvent<float> OnChangeValue;

    public void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        OnChangeValue?.Invoke(Value);
    }

}
