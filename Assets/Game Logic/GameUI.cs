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
    public TMP_Text taskListText; // ✅ letakkan di sini
    public PlantPlotGroup[] allGroups; // ✅ letakkan di sini
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
            UpdateTaskListUI(); // Tambahkan ini

    }

    void Update()
    {
        UpdateUI(); // update terus setiap frame (bisa dioptimalkan nanti)
            UpdateTaskListUI(); // ⬅️ tambahkan ini

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
                    UpdateTaskListUI(); // ⬅️ update tampilan

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
                    !plot.hasWateredToday && !plot.isDead)
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

        UpdateTaskListUI(); // Tambahkan ini
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
    // }public PlantPlotGroup[] allGroups; // drag semua group ke inspector

public void UpdateTaskListUI()
{
    int totalJagung = 0;
    int jagungDisiram = 0;
    int totalTanaman = 0;
    int pestRemoved = 0;

    foreach (var group in allGroups)
    {
        foreach (var plot in group.GetPlots())
        {
            if (plot.plantData != null && !plot.isDead && !plot.IsInPreparation())
            {
                totalTanaman++;
                if (plot.hasRemovedPestToday) pestRemoved++;

                if (plot.plantData.plantType == PlantData.PlantType.Jagung)
                {
                    totalJagung++;
                    if (plot.hasWateredToday) jagungDisiram++;
                }
            }
        }
    }
    // Update UI text
    taskListText.text = "";

    if (totalJagung > 0)
    {
        taskListText.text += $"☐ Siram Jagung: ({jagungDisiram}/{totalJagung})\n";
    }
    if (totalTanaman > 0)
    {
        taskListText.text += $"☐ Semprot Hama: ({pestRemoved}/{totalTanaman})\n";
    }

    // Jika tidak ada task sama sekali, sembunyikan text
    taskListText.gameObject.SetActive(taskListText.text != "");

}

}
