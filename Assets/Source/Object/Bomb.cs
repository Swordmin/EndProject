using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    [SerializeField] private float _radius;
    [SerializeField] private float _force;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            Activate();
    }

    public void Activate() 
    {
        Collider2D[] overCollider = Physics2D.OverlapCircleAll(transform.position, _radius);
        for (int i = 0; i < overCollider.Length; i++)
        {
            Rigidbody2D rigidbody = overCollider[i].attachedRigidbody;
            if (rigidbody)
            {
                rigidbody.AddExplosionForce(_force, transform.position, _radius, 1f);
            }
        }
    }
}
