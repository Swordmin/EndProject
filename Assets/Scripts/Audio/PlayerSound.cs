using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{

    [SerializeField] private List<Sound> _sounds;
    [SerializeField] private AudioSource _source;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public void Play(string id)
    {

        foreach(Sound sound in _sounds) 
        {
            if (sound.id == id)
            {
                _source.volume = sound.Volume;
                _source.clip = sound.Clip;
                _source.Play();
            }

        }

    }

}
