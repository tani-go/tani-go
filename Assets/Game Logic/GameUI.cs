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
        if (timeManager != null)
        timeManager.onDayPassed.AddListener(UpdateTaskListUI); // ✅ update task list setiap hari
    }
    
    public void SemprotHama()
    {
        if (selectedGroup == null)
        {
            Debug.Log("❌ Tidak ada sawah yang diseleksi saat menyemprot");
            return;
        }

        Debug.Log("✅ Semprot hama untuk group: " + selectedGroup.name);

        foreach (var plot in selectedGroup.GetPlots())
        {
            if (plot.plantData != null && !plot.isDead)
            {
                plot.RemovePestToday();
                Debug.Log("✅ Hama disemprot di plot: " + plot.name);
            }
            else
            {
                Debug.Log("❌ Plot dilewati: " + plot.name + " | plantData=" + plot.plantData + " | isDead=" + plot.isDead);
            }
        }

        UpdateTaskListUI();
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
            if (allGroups == null || allGroups.Length == 0)
            {
                Debug.LogWarning("❗ GameUI.allGroups belum diisi di Inspector!");
                return;
            }

            string taskText = "📋 Task Hari Ini:\n";

            foreach (var group in allGroups)
            {
                int totalJagung = 0;
                int jagungDisiram = 0;
                int totalTanaman = 0;
                int pestRemoved = 0;

                foreach (var plot in group.GetPlots())
                {
                    if (plot.plantData != null && !plot.isDead)
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

                if (totalJagung > 0 || totalTanaman > 0)
                {
                    taskText += $"🌾 {group.name}\n";
                    if (totalJagung > 0)
                        taskText += $"   ☐ Siram Jagung: {jagungDisiram}/{totalJagung}\n";
                    if (totalTanaman > 0)
                        taskText += $"   ☐ Semprot Hama: {pestRemoved}/{totalTanaman}\n";
                }
            }

            taskListText.text = taskText.Trim();

            // Jika kosong, sembunyikan
            taskListText.gameObject.SetActive(taskListText.text != "Task Hari Ini:\n");
        }


}
