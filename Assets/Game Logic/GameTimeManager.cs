using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameTimeManager : MonoBehaviour
{
    public float secondsPerDay = 120f; //600 = 10 menit
    private float timeCounter = 0f;
    public int currentDay = 1;
    public enum WaktuHarian{Pagi, Siang, Sore, Malam}
    public UnityEvent onDayPassed;
    public SeasonManager seasonManager;

    private bool isTimeRunning = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isTimeRunning) return;

        timeCounter += Time.deltaTime;
        if(timeCounter >= secondsPerDay)
        {
            NextDay();
        }
    }

    public void NextDay(){
        timeCounter = 0f;
        currentDay++;
        Debug.Log("Hari ke - "+currentDay);

        if(seasonManager != null)
        seasonManager.UpdateSeason(currentDay);
        Debug.Log("Hari ke - "+currentDay + " | Musim: " + seasonManager.GetCurrentSeason());

        onDayPassed?.Invoke();//memanggil event(pertumbuhan, musim)

    }

    public void SkipToNextDay(){
        Debug.Log("Lanjut ke hari berikutnya");
        NextDay();
    }

    public int GetCurrentDay(){
        return currentDay;
    }

    public float GetTimeProgress(){
        return timeCounter/ secondsPerDay; //0.0 - 1.0
    }

    public WaktuHarian GetWaktuSaatIni(){
        float t = timeCounter;

        if(t < secondsPerDay * 0.25f) return WaktuHarian.Pagi;
        else if(t < secondsPerDay * 0.5f) return WaktuHarian.Siang;
        else if(t < secondsPerDay * 0.75f) return WaktuHarian.Sore;
        else return WaktuHarian.Malam;
    }
}
