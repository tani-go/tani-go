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
        FindObjectOfType<PlantManager>().SetSelectedGroup(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void HarvestAll()
    {
        foreach (var plot in plots)
        {
            plot.Harvest();
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
