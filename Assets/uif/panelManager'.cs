using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [field: SerializeField] private GameObject[] panels;

    public void ActivatePanels()
    {
        ToggleAll(true);
    }

    public void DeactivatePanels()
    {
        ToggleAll(false);
    }

    // Using a helper method reduces code duplication
    private void ToggleAll(bool state)
    {
        foreach (GameObject panel in panels)
        {
            if (panel != null) // Safety check
            {
                panel.SetActive(state);
            }
        }
    }
}