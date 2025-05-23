using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPlot : MonoBehaviour
{
    public PlantData plantData;
    public int stage = 0;
    private float timer = 0f;
    private GameObject currentPlantObj;

    public void Plant (PlantData newPlant){
        plantData = newPlant;
        stage = 0;
        timer = 0f;
        UpdateVisual();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (plantData == null || stage >= plantData.growthPrefabs.Length) return;

        timer += Time.deltaTime;
        if(timer>=plantData.growthTimes[stage]){
            timer = 0f;
            stage++;
            UpdateVisual();
        }
    }

    void UpdateVisual(){
        if(currentPlantObj != null)
            Destroy(currentPlantObj);

        if(stage < plantData.growthPrefabs.Length)
            currentPlantObj = Instantiate(plantData.growthPrefabs[stage], transform.position + Vector3.up * 1f, Quaternion.identity, transform);
        
    }

    /*void OnMouseDown(){
        FindObjectOfType<PlantManager>().SetSelectedPlot(this);
    }
    */
}
