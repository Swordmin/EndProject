using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    [SerializeField] private int _count;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.TryGetComponent(out PlayerMovement player)) 
        {
            LevelSettings.Settings.TakeCoin(_count);
            Destroy(gameObject);
        }
    }

}
