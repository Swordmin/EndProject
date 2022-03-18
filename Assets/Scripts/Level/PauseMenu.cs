using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private Animator _animator;
    [SerializeField] private bool _isPause;
    public void Pause()
    {
        LevelSettings.Settings.Pause(out _isPause);
        if (_isPause) 
        {
            _animator.Play("Show");
        }
        else
        {
            _animator.Play("Hide");
        }
    }

}
