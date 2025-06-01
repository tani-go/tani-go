using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPlot : MonoBehaviour
{
    public PlantData plantData;
    public int stage = 0;
    private float timer = 0f;
    private int daysWaited = 0;
    private GameObject currentPlantObj;

    public bool isDead = false;

    public void Plant(PlantData newPlant)
    {
        if(plantData != null && !isDead && stage <plantData.growthPrefabs.Length -1)
        {
        Debug.Log("Plot masih ada isinya");
        return; 
        }

        plantData = newPlant;
        stage = 0;
        timer = 0f;
        daysWaited = 0;
        isDead = false;
        UpdateVisual();
    }

    void Start()
    {
        GameTimeManager timeManager = FindObjectOfType<GameTimeManager>();
        if (timeManager != null)
        {
            timeManager.onDayPassed.AddListener(GrowPerDay);
        }
    }

    void UpdateVisual()
    {
        if (currentPlantObj != null)
            Destroy(currentPlantObj);

        if (plantData == null)
            return; // Tambahkan ini agar tidak error saat belum ada tanaman

        if (isDead && plantData.wiltedPrefab != null)
        {
            currentPlantObj = Instantiate(plantData.wiltedPrefab, transform.position + Vector3.up * 1f, Quaternion.identity, transform);
        }
        else if (stage < plantData.growthPrefabs.Length)
        {
            currentPlantObj = Instantiate(plantData.growthPrefabs[stage], transform.position + Vector3.up * 1f, Quaternion.identity, transform);
        }
    }


    public void GrowPerDay()
    {
        if (plantData == null || isDead || stage >= plantData.growthPrefabs.Length) return;

        // Hanya cek musim jika di stage 1
        if (stage == 1)
        {
            var currentSeason = FindObjectOfType<SeasonManager>().GetCurrentSeason();
            bool salahMusim =
                (plantData.plantType == PlantData.PlantType.Padi && currentSeason == SeasonManager.Season.Kemarau) ||
                (plantData.plantType == PlantData.PlantType.Jagung && currentSeason == SeasonManager.Season.Hujan);

            if (salahMusim)
            {
                isDead = true;
                Debug.Log("Tanaman gagal panen di stage 1 karena musim tidak sesuai");
                UpdateVisual();
                return;
            }
        }

        daysWaited++;
        if (daysWaited >= plantData.growthTimes[stage])
        {
            stage++;
            daysWaited = 0;
            UpdateVisual();
        }
    }

    public void Harvest(){
        if(plantData ==  null || isDead || stage < plantData.growthPrefabs.Length-1)
        {
            Debug.Log("Belum bisa dipanen/Mati");
            return;
        }

        Debug.Log("Tanaman dipanen");
            plantData = null;
            stage = 0;
            timer = 0f;
            daysWaited = 0;
            isDead = false;
            UpdateVisual(); // Bersihkan visual
    }

}
