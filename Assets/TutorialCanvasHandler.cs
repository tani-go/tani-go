using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCanvasHandler : MonoBehaviour
{
    public GameObject tutorialCanvas;

    public void CloseTutorial()
    {
        tutorialCanvas.SetActive(false);
    }
}

