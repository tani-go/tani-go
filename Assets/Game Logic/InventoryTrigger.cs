using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryTrigger : MonoBehaviour
{
    void OnMouseDown()
    {
        SceneManager.LoadScene("Inventory", LoadSceneMode.Additive);
    }
}
