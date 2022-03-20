using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private bool _isPause;

    public void Close()
    {
        PanelsSwitcher.Switcher.Close(GetComponent<PanelMovement>().Type);
    }

    public void Open()
    {
        PanelsSwitcher.Switcher.Open(GetComponent<PanelMovement>().Type);
    }

    public void Pause()
    {
        LevelSettings.Settings.Pause(out _isPause);
        if (_isPause) 
        {
            Open();
        }
        else
        {
            Close();
        }
    }

}
