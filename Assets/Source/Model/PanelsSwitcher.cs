using System.Collections.Generic;
using UnityEngine;

public enum PanelType 
{
    Pause,
    Win,
    Defeat,
    StartScreen
}

public class PanelsSwitcher : MonoBehaviour
{

    public static PanelsSwitcher Switcher;

    [SerializeField] private List<PanelMovement> _panels;

    private void Awake()
    {
        if (!Switcher)
            Switcher = this;
    }

    public void Open(PanelType type) 
    {
        foreach (PanelMovement panel in _panels)
        {
            if (panel.Type != type)
                panel.Close();
            else
                panel.Open();
        }
        
    }

    public void Close(PanelType type)
    {
        foreach (PanelMovement panel in _panels)
        {
            if (panel.Type == type)
                panel.Close();
        }
        
    }

}
