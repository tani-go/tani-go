using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPlotGroup : MonoBehaviour
{
    private List<PlantPlot> plots = new List<PlantPlot>();

    private Material originalMaterial;
    public Material selectedMaterial; 
    private Renderer[] renderers;

    private void Awake(){

                // Ambil semua anak yang punya PlantPlot
        plots.AddRange(GetComponentsInChildren<PlantPlot>());
        renderers = GetComponentsInChildren<Renderer>();
        if(renderers.Length > 0)
        originalMaterial = renderers[0].material;
    }

    public void PlantAll(PlantData plantData){
        foreach (var plot in plots){
            plot.Plant(plantData);
        }
    }

    public void SetHighlighted(bool highlighted)
    {
        Debug.Log($"SetHighlighted dipanggil: {highlighted}, Jumlah Renderer: {renderers.Length}");
        foreach (var rend in renderers)
        {
            rend.material = highlighted ? selectedMaterial : originalMaterial;
        }
    }


    void OnMouseDown(){
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        return;

        FindObjectOfType<PlantManager>().SetSelectedGroup(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void HarvestAll()
    {
        int totalPadi = 0;
        int totalJagung = 0;
        int totalXP = 0;

        foreach (var plot in plots)
        {
            if (plot.CanBeHarvested())
            {
                string jenis = plot.GetPlantType(); // "Padi" atau "Jagung"
                bool berhasil = plot.Harvest();
                if (berhasil)
                {
                if (jenis == "Padi")
                {
                    totalPadi++;
                    totalXP += 10; // Misalnya 10 XP per Padi
                }
                else if (jenis == "Jagung")
                {
                    totalJagung++;
                    totalXP += 15; // Misalnya 15 XP per Jagung
                }
                }
            }
        }

        if (totalPadi > 0)
    {
        InventoryManager.Instance.AddItem("Padi", totalPadi);
        Debug.Log($"✅ Total panen Padi: {totalPadi}");
    }

    if (totalJagung > 0)
    {
        InventoryManager.Instance.AddItem("Jagung", totalJagung);
        Debug.Log($"✅ Total panen Jagung: {totalJagung}");
    }

    if (totalXP > 0)
    {
        PlayerStats.Instance.AddXP(totalXP);
        Debug.Log($"✨ Total XP diperoleh: {totalXP}");
    }
}


    public void ResetAllDailyStatus()
    {
        foreach (var plot in plots)
        {
            plot.ResetDailyStatus();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public List<PlantPlot> GetPlots()
    {
        return plots;
    }

}
