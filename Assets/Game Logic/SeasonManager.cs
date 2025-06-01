using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonManager : MonoBehaviour
{
    public enum Season{Hujan, Kemarau}

    public Season currentSeason = Season.Hujan;
    public int daysPerSeason = 10;

    public void UpdateSeason(int currentDay){
        int seasonIndex = (currentDay - 1) / daysPerSeason;
        currentSeason = (Season)(seasonIndex % 2);
    }

    public Season GetCurrentSeason(){
        return currentSeason;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
