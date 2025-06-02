using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public PlantData padiData;
    public PlantData jagungData;
    public PlantPlot selectedPlot;
    public PlantPlotGroup selectedGroup;
    public void PlantPadi(){
        if(selectedGroup != null)
        selectedGroup.PlantAll(padiData);
    }

    public void PlantJagung(){
        if(selectedGroup !=null)
        selectedGroup.PlantAll(jagungData);
    }

    public void SetSelectedGroup(PlantPlotGroup group){
        if(selectedGroup != null)
        selectedGroup.SetHighlighted(false);
        
        selectedGroup = group;
        FindObjectOfType<GameUI>().SetSelectedGroup(group);

        selectedGroup.SetHighlighted(true);
        Debug.Log("Sawah dipilih: "+ group.name);
    }   

        public void Harvest()
    {
        if (selectedGroup != null)
        {
            selectedGroup.HarvestAll();
        }
    }
    public void ClearSelection()
    {
        if (selectedGroup != null)
        {
            selectedGroup.SetHighlighted(false);
            selectedGroup = null;
        }
    }

        public void HarvestSelected()
    {
        if (selectedGroup != null)
        {
            selectedGroup.HarvestAll();
        }

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
