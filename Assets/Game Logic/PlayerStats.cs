using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    public int xp;
    public Text xpText;

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

    public void AddXP(int amount)
    {
        xp += amount;
        UpdateXPUI();
    }

    void UpdateXPUI()
    {
        if (xpText != null)
        {
            xpText.text = xp.ToString("D6");
        }
    }
}
