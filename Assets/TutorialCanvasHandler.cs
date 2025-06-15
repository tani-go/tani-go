using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialCanvasHandler : MonoBehaviour
{
    public GameObject tutorialCanvas;

    void Update()
    {
        // Deteksi klik kiri mouse
        if (Input.GetMouseButtonDown(0))
        {
            // Jika tidak mengklik elemen UI mana pun
            if (!IsPointerOverUI())
            {
                // Sembunyikan tutorial canvas
                tutorialCanvas.SetActive(false);
            }
        }
    }

    // Mengecek apakah pointer sedang berada di atas UI
    bool IsPointerOverUI()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        var raycastResults = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);

        foreach (var result in raycastResults)
        {
            // Abaikan jika klik masih di area TutorialCanvas
            if (result.gameObject.transform.IsChildOf(tutorialCanvas.transform))
                return true;
        }

        return false;
    }
}
