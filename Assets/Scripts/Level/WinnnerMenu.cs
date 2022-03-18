using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WinnnerMenu : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _coinCountText;
    [SerializeField] private Animator _animator;
    [SerializeField] private Image[] _starsImage;

    public void Show() 
    {
        LevelSettings.Settings.StopTimer();
        LevelSettings.Settings.Pause(out bool isPause);
        GlobalResources.Resources.AddCoins(LevelSettings.Settings.CoinCount);
        _animator.Play("Show"); 
        _timerText.text = LevelSettings.Settings.TimeText;
        _coinCountText.text = LevelSettings.Settings.CoinCount.ToString();
        for(int i = 0; i < LevelSettings.Settings.Stars.Length; i++)
        {
            if(LevelSettings.Settings.Stars[i])
                _starsImage[i].color = new Color(1,1,1,1);
        }
    }



}
