using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    Vector3 _originalScale;

    void Start()
    {
        _originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData) //When the pointer enters the button area, increase the scale to 110% of the original size.
    {
        transform.localScale = _originalScale * 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData)//When the pointer exits the button area, reset the scale to the original size.
    {
        transform.localScale = _originalScale;
    }

    public void OnPointerDown(PointerEventData eventData)//When the pointer is pressed down on the button, decrease the scale to 95% of the original size.
    {
        transform.localScale = _originalScale * 0.95f;
    }

    public void OnPointerUp(PointerEventData eventData)//When the pointer is released from the button, increase the scale back to 110% of the original size.
    {
        transform.localScale = _originalScale * 1.1f;
    }
}