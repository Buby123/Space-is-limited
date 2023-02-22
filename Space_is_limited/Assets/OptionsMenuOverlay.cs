using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionsMenuOverlay : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private GraphicRaycaster _graphicRaycaster;

    /// <summary>
    /// Get the graphic raycaster component
    /// </summary>
    private void Start()
    {
        _graphicRaycaster = GetComponent<GraphicRaycaster>();
    }

    /// <summary>
    /// Disable input to the UI elements beneath the overlay panel
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        _graphicRaycaster.enabled = true;
    }

    /// <summary>
    /// Enable input to the UI elements beneath the overlay panel
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        _graphicRaycaster.enabled = false;
    }
}