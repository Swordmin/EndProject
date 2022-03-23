using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformParent : MonoBehaviour
{

    private PlayerMovement _player;
    private PathWalk _pathWalk;

    private void OnEnable() => _pathWalk = GetComponent<PathWalk>();

    private void Update()
    {
        if (_player)
            _player.SetDoubleVelocity(new Vector2(_pathWalk.MoveDirectionVelocity.x,0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerMovement player)) 
        {
            //player.transform.parent = transform;
            //player.SetDoubleVelocity(GetComponent<Rigidbody2D>().velocity);
            _player = player;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerMovement player))
        {
            //player.transform.parent = null;
            player.SetDoubleVelocity(Vector2.zero);
            _player = null;
        }
    }
}
