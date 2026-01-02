using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Fondamentale per il touch

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [Header("Impostazioni")]
    public Image joystickBackground;
    public Image joystickHandle;
    
    // Questo vettore lo leggeremo dal Player per muoverci!
    public Vector2 InputDirection { set; get; } 

    private Vector2 posInput;

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickBackground.rectTransform, 
            eventData.position, 
            eventData.pressEventCamera, 
            out posInput))
        {
            // Normalizza la posizione del tocco rispetto alla grandezza del background
            posInput.x = posInput.x / (joystickBackground.rectTransform.sizeDelta.x);
            posInput.y = posInput.y / (joystickBackground.rectTransform.sizeDelta.y);

            // Se il valore è preciso, normalizzalo, altrimenti lascia così
            if (posInput.magnitude > 1.0f)
            {
                posInput = posInput.normalized;
            }

            // Muovi la grafica del manettino (Handle)
            InputDirection = posInput;
            joystickHandle.rectTransform.anchoredPosition = new Vector2(
                InputDirection.x * (joystickBackground.rectTransform.sizeDelta.x / 2),
                InputDirection.y * (joystickBackground.rectTransform.sizeDelta.y / 2));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData); // Inizia a trascinare appena tocchi
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Quando lasci il dito, resetta tutto al centro
        InputDirection = Vector2.zero;
        joystickHandle.rectTransform.anchoredPosition = Vector2.zero;
    }
}