using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class joyStick : MonoBehaviour , IDragHandler , IPointerUpHandler , IPointerDownHandler
{
    public Image joystickBack;
    public Image joystickControl;
    public Vector3 direction;

    public void OnDrag(PointerEventData mouse)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBack.rectTransform, mouse.position, mouse.pressEventCamera, out pos))
        {
            pos.x = (pos.x / joystickBack.rectTransform.sizeDelta.x);
            pos.y = (pos.y / joystickBack.rectTransform.sizeDelta.y);
            direction = new Vector3(pos.x, 0, pos.y);

        }
        if (direction.magnitude > 1f)
        {
            direction = direction.normalized;
        }
        joystickControl.rectTransform.anchoredPosition = new Vector3  (direction.x * (joystickBack.rectTransform.sizeDelta.x / 2),   direction.z * (joystickBack.rectTransform.sizeDelta.y / 2));
    }
    public void OnPointerDown(PointerEventData mouse)
    {
        OnDrag(mouse);
    }
    public void OnPointerUp(PointerEventData mouse)
    {
        direction = Vector3.zero;
        joystickControl.rectTransform.anchoredPosition = Vector3.zero;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
