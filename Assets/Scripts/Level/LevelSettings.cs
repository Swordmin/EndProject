using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSettings : MonoBehaviour
{

    [SerializeField] private bool[] _stars;

    [SerializeField] private string _levelId;
    [SerializeField] private string _levelIdOpen;
    [SerializeField] private Level _thisLevel;
    public static LevelSettings Settings;
    [SerializeField] private float[] _timeNeedStars;
    [SerializeField] private Essence _playerHealth;

    public float Time { get;  private set; }
    public float BestTime { get;  private set; }
    public float CoinCount { get; private set; }
    public bool[] Stars => _stars;

    private bool _isPause;

    [SerializeField] private TextMeshProUGUI _textTime;
    [SerializeField] private TextMeshProUGUI _textCoinCount;
    public string TimeText => _textTime.text;
    public string CoinCountText => _textCoinCount.text;

    public List<IPause> Pauses = new List<IPause>();

    private void Awake()
    {
        StartCoroutine(Timer());
        if (!Settings)
            Settings = this;
        if(GlobalResources.Resources)
            _thisLevel = GlobalResources.Resources.FindLevelByID(_levelId);
        else
            Debug.LogError("SFS ERROR: GlobalResoures is not found");
    }

    public void TakeCoin(float count) 
    {
        if (count < 0)
        {
            Debug.LogError("SFS ERROR: value must be positive");
            return;
        }
        CoinCount += count;
        _textCoinCount.text = CoinCount.ToString();
    }

    public void StopTimer() 
    {
        StopCoroutine("Timer");
    }
    public void Pause(out bool isPause) 
    {
        if(_isPause)
        {
            PauseObjects(false);
            _isPause = false;
        }
        else
        {
            PauseObjects(true);
            _isPause = true;
        }
        isPause = _isPause;
    }

    private void PauseObjects(bool isPause) 
    {
        foreach (IPause pauseObject in Pauses)
        {
            if (isPause)
                pauseObject.Pause();
            else
                pauseObject.Resume();
        }
    }

    private IEnumerator Timer() 
    {
        int minute = 00;
        int seconts = 00;
        while (true) 
        {
            yield return new WaitForSeconds(1);
            if (!_isPause)
            {
                Time++;
                seconts++;
            }
            _textTime.text = $"{minute}:{seconts}";
            if(seconts == 60)
            {
                minute++;
                seconts = 0;
            }

        }
    }

    public void LevelDone() 
    {
        if (Time > BestTime)
            BestTime = Time;
        if (Time <= _timeNeedStars[0])
            _stars[0] = true;
        if (Time <= _timeNeedStars[1])
            _stars[1] = true;
        if (_playerHealth.IsFullHealth())
            _stars[2] = true;
        if (!_thisLevel.Done) 
        {
            _thisLevel.Done = true;
            if (BestTime > _thisLevel.BestTime)
                GlobalResources.Resources.SetBestTimeLevelByID(_thisLevel.Id, BestTime);
            for (int i = 0; i < _stars.Length; i++) 
            {
                if(!_thisLevel.Stars[i] && _stars[i])
                {
                    _thisLevel.Stars[i] = true;
                }
            }
            if (_levelIdOpen != "Noone")
                GlobalResources.Resources.OpenLevelByID(_levelIdOpen);
        }
        GlobalResources.Resources.Save();
    }
}


