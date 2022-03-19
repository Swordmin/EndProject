using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvent : MonoBehaviour
{

    [SerializeField] private UnityEvent _triggerEnter;
    [SerializeField] private UnityEvent _triggerExit;


    [SerializeField] private List<string> _tags;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_tags.Count == 0)
        {
            Debug.LogError("SFS ERROR: Tags not found.");
            return;
        }
        foreach(string tag in _tags)
        {
            if (collision.gameObject.tag == tag)
                _triggerEnter?.Invoke();
            break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_tags.Count == 0)
        {
            Debug.LogError("SFS ERROR: Tags not found.");
            return;
        }
        foreach (string tag in _tags)
        {
            if (collision.gameObject.tag == tag)
                _triggerExit?.Invoke();
            break;
        }
    }
}
