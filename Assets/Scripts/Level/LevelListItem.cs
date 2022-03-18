using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LevelListItem : MonoBehaviour
{

    [SerializeField] private Level _level;

    [SerializeField] private Sprite _spriteOpen, _spriteClose, _spriteDone;
    [SerializeField] private Sprite _starDone;

    [SerializeField] private Image _image;
    [SerializeField] private SceneManagment _sceneManagment;
    [SerializeField] private Image[] _stars;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Start()
    {
        Initialized(GlobalResources.Resources.FindLevelByID(_level.Id));
    }

    public void Initialized(Level level)
    {
        _level = level;
        if (_level.Can) 
        {
            if(GlobalResources.Resources.Stars >= _level.StarsNeed)
            {
                _image.sprite = _spriteOpen;
            }
            else
            {
                _image.sprite = _spriteClose;
            }
        }
        else
        {
            _image.sprite = _spriteClose;
        }
        if (_level.Done)
            _image.sprite = _spriteDone;
        for (int i = 0; i < _level.Stars.Length; i++)
        {
            if(_level.Stars[i])
                _stars[i].sprite = _starDone;
        }
    }

    public void Load() 
    {
        if(CanLoad())
            _sceneManagment.LoadLvl(_level.SceneId);
    }

    private bool CanLoad() 
    {

        if (_level.Can && GlobalResources.Resources.Stars >= _level.StarsNeed)
            return true;
        return false;
    }

}
