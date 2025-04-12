using UnityEngine;
using UnityEngine.EventSystems; // Perlu untuk menangani klik
using UnityEngine.SceneManagement;
public class PanelClick : MonoBehaviour, IPointerClickHandler
{
    public GameObject panelStart;
    public GameObject panelMainMenu;
    public GameObject panelOption;
    public AudioSource clickSound; // Tambahkan variabel untuk suara klik

    public void OnPointerClick(PointerEventData eventData)
    {
                if (clickSound != null)
        {
            clickSound.Play(); // Memainkan efek suara klik
        }
        panelStart.SetActive(false); // Sembunyikan Panel Start
        panelMainMenu.SetActive(true); // Tampilkan Panel Main Menu
        panelOption.SetActive(false); // Tampilkan Panel Option
    }

    public void OpenMainMenu(){
        panelStart.SetActive(false); // Sembunyikan Panel Start
        panelMainMenu.SetActive(true); // Tampilkan Panel Main Menu
        panelOption.SetActive(false); // Tampilkan Panel Option
    }

    public void OpenOption(){
        panelStart.SetActive(false); // Sembunyikan Panel Start
        panelMainMenu.SetActive(false); // Tampilkan Panel Main Menu
        panelOption.SetActive(true); // Tampilkan Panel Option
    }

    public void OpenGameplay(){
        SceneManager.LoadScene("Gameplay");
    }
}
