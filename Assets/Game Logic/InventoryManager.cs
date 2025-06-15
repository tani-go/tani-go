using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    private Dictionary<string, int> items = new Dictionary<string, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem(string itemName)
    {
        if (!items.ContainsKey(itemName))
            items[itemName] = 0;

        items[itemName]++;
        Debug.Log("Item Ditambahkan: " + itemName + " (" + items[itemName] + ")");
    }

    public bool HasItem(string itemName)
    {
        return items.ContainsKey(itemName) && items[itemName] > 0;
    }

    public void RemoveItem(string itemName)
    {
        if (HasItem(itemName))
        {
            items[itemName]--;
            Debug.Log("Item Dikurangi: " + itemName + " (" + items[itemName] + ")");
        }
    }

    public int GetItemCount(string itemName)
    {
        return items.ContainsKey(itemName) ? items[itemName] : 0;
    }
}
