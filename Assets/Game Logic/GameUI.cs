using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    public GameTimeManager timeManager;
    public SeasonManager seasonManager; 
    public TMP_Text hariText;
    public TMP_Text waktuText;
    public TMP_Text musimText;
    private PlantPlotGroup selectedGroup;
    public Button bersihkanButton;
    public Button siramButton;
    public Button semprotHamaButton;
  //  public TMP_Text notifikasiText;

    public void UpdateUI()
    {
        hariText.text = "Hari ke: " + timeManager.GetCurrentDay();
        waktuText.text = "Waktu: " + timeManager.GetWaktuSaatIni().ToString();
        musimText.text = "Musim: " + seasonManager.GetCurrentSeason().ToString();
    }
    public void SetSelectedGroup(PlantPlotGroup group)
    {
        selectedGroup = group;
        UpdateButtonVisibility();
    }

    void Update()
    {
        UpdateUI(); // update terus setiap frame (bisa dioptimalkan nanti)
    }

    public void SkipDayBtn()
    {
        timeManager.SkipToNextDay();
    }
    public void BersihkanTanaman()
    {
        if (selectedGroup == null) return;

        foreach (var plot in selectedGroup.GetPlots())
        {
            if (plot.isDead)
            {
                plot.ClearDeadPlant();
            }
        }

        UpdateButtonVisibility();
    }

    public void SiramTanaman()
    {
        if (selectedGroup == null) return;

        foreach (var plot in selectedGroup.GetPlots())
        {
            if (plot.plantData != null &&
                plot.plantData.plantType == PlantData.PlantType.Jagung &&
                plot.soilState == PlantPlot.SoilState.Bajak)
            {
                plot.Water();
                plot.WaterPlantToday(); // ⬅️ Tambahkan ini
            }
        }

        UpdateButtonVisibility();
    }

    void UpdateButtonVisibility()
    {
        bool adaTanamanMati = false;
        bool adaJagungBelumDisiram = false;
        bool adaPerluSemprot = false;

        if (selectedGroup != null)
        {
            foreach (var plot in selectedGroup.GetPlots())
            {
                if (plot.isDead)
                    adaTanamanMati = true;

                if (plot.plantData != null &&
                    plot.plantData.plantType == PlantData.PlantType.Jagung &&
                    plot.soilState == PlantPlot.SoilState.Bajak)
                {
                    adaJagungBelumDisiram = true;
                }

                if (plot.plantData != null)
                {
                    adaPerluSemprot = true;
                }
            }
        }

        bersihkanButton.gameObject.SetActive(adaTanamanMati);
        siramButton.gameObject.SetActive(adaJagungBelumDisiram);
        semprotHamaButton?.gameObject.SetActive(adaPerluSemprot); // pastikan sudah di-assign di inspector
    }

    void Start()
    {
        InvokeRepeating(nameof(UpdateUI), 0f, 1f); // update setiap 1 detik
    }
    public void SemprotHama()
    {
        if (selectedGroup == null) return;

        foreach (var plot in selectedGroup.GetPlots())
        {
            if (plot.plantData != null)
            {
                plot.RemovePestToday(); // tandai sudah dihapus hamanya hari ini
            }
        }

        UpdateButtonVisibility();
    }
    // void ShowNotif(string message)
    // {
    //     notifikasiText.text = message;
    //     CancelInvoke(nameof(HideNotif));
    //     Invoke(nameof(HideNotif), 2f); // sembunyikan dalam 2 detik
    // }

    // void HideNotif()
    // {
    //     notifikasiText.text = "";
    // }

}
