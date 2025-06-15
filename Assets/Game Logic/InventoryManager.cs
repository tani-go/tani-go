using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("UI")]
    public GameObject inventoryPanel;

    [Header("Icon References")]
    public Sprite padiIcon;
    public Sprite jagungIcon;

    [Header("Jumlah Text")]
    public TMP_Text jumlahPadiText;
    public TMP_Text jumlahJagungText;

    private Dictionary<string, int> inventory = new Dictionary<string, int>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Inventory") // Ganti dengan nama scene inventory kamu
        {
            // Re-link UI references setelah pindah scene
            jumlahPadiText = GameObject.Find("JumlahPadi")?.GetComponent<TMP_Text>();
            jumlahJagungText = GameObject.Find("JumlahJagung")?.GetComponent<TMP_Text>();
            inventoryPanel = GameObject.Find("InventoryBox");

            UpdateUI();
        }
    }

    public void AddItem(string itemName, int amount)
    {
        if (inventory.ContainsKey(itemName))
            inventory[itemName] += amount;
        else
            inventory[itemName] = amount;

        Debug.Log($"📦 {itemName} ditambahkan ke inventori (jumlah sekarang: {inventory[itemName]})");

        UpdateUI();
    }

    public void ToggleInventory()
    {
        if (inventoryPanel == null) return;

        bool aktif = inventoryPanel.activeSelf;
        inventoryPanel.SetActive(!aktif);

        if (!aktif) UpdateUI();
    }

    void UpdateUI()
    {
        if (jumlahPadiText == null || jumlahJagungText == null) return;

        int jumlahPadi = inventory.ContainsKey("Padi") ? inventory["Padi"] : 0;
        int jumlahJagung = inventory.ContainsKey("Jagung") ? inventory["Jagung"] : 0;

        jumlahPadiText.text = $"x {jumlahPadi}";
        jumlahJagungText.text = $"x {jumlahJagung}";
    }

    public void BackToGameplay()
    {
        Debug.Log("🔙 Tombol back ditekan");
        SceneManager.LoadScene("Level Design Demo 01"); // GANTI dengan nama scene gameplay kamu
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
