using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject[] openPanels;
    public GameObject[] closePanels;

    public void OpenPanel()
    {
        foreach (GameObject panel in openPanels)
        {
            panel.SetActive(false);
        }
        foreach (GameObject panel in closePanels)
        {
            panel.SetActive(true);
        }
    }
    public void ClosePanel()
    {
        foreach (GameObject panel in closePanels)
        {
            panel.SetActive(false);
        }
        foreach (GameObject panel in openPanels)
        {
            panel.SetActive(true);
        }
    }
}