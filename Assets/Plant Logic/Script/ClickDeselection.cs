using UnityEngine;
using UnityEngine.EventSystems;

public class ClickDeselection : MonoBehaviour
{
    private PlantManager plantManager;

    void Start()
    {
        plantManager = FindObjectOfType<PlantManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // ⛔ Abaikan klik kalau mouse lagi di atas UI
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.GetComponentInParent<PlantPlotGroup>() == null)
                {
                    plantManager.ClearSelection();
                }
            }
            else
            {
                plantManager.ClearSelection();
            }
        }
    }
}
