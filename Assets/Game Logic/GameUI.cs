using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    public GameTimeManager timeManager;
    public SeasonManager seasonManager; // nanti kita buat
    public TMP_Text hariText;
    public TMP_Text waktuText;
    public TMP_Text musimText;

    public void UpdateUI()
    {
        hariText.text = "Hari ke: " + timeManager.GetCurrentDay();
        waktuText.text = "Waktu: " + timeManager.GetWaktuSaatIni().ToString();
        musimText.text = "Musim: " + seasonManager.GetCurrentSeason().ToString();
    }

    void Update()
    {
        UpdateUI(); // update terus setiap frame (bisa dioptimalkan nanti)
    }

    public void SkipDayBtn()
    {
        timeManager.SkipToNextDay();
    }
}
