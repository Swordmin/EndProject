using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformParent : MonoBehaviour
{

    private Rigidbody2D _playerRigidbody;

    private void Update()
    {
        if(_playerRigidbody != null)
        {
            //_playerRigidbody.velocity = GetComponent<Rigidbody2D>().velocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerMovement player)) 
        {
            player.transform.parent = transform;
            _playerRigidbody = player.Rigidbody2D;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerMovement player))
        {
            player.transform.parent = null;
            _playerRigidbody = null;
        }
    }
}
