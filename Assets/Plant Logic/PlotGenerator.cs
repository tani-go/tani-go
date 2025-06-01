using UnityEngine;

public class PlotGenerator : MonoBehaviour
{
    public GameObject plotPrefab; // drag prefab PlotTemplate ke sini
    public int width = 5;
    public int height = 5;
    public float spacing = 3f;
    public Vector3 startOffset = Vector3.zero;

    void Start()
    {
        GeneratePlots();
    }

    void GeneratePlots()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 pos = new Vector3(x * spacing, 0, z * spacing) + startOffset;
                Instantiate(plotPrefab, pos, Quaternion.identity, transform);
            }
        }
    }
}
