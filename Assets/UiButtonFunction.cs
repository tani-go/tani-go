using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiButtonFunction : MonoBehaviour
{
    // Start is called before the first frame update
     void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShopButton(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    public void SellButton(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    public void HomeButton(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
}
