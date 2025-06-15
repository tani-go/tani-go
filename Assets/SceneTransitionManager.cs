using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{


    public void GoToGameplay()
    {
        SceneManager.UnloadSceneAsync("Inventory");
    }
}
