using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Essence
{

    [SerializeField] private PlayerAnimatorController _animator;

    [SerializeField] private float _kickForce;
    [SerializeField] private float _cooldown;
    [SerializeField] private bool _canTakeDamage;

    public override void Start()
    {
        base.Start();
        _animator = GetComponent<PlayerAnimatorController>();
    }

    public override void Die() 
    {
        base.Die();
        Destroy(gameObject);    
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Enemy enemy) && _canTakeDamage) 
        {
            TakeDamage(enemy.Damage);
            Vector2 kickDirection = enemy.transform.position - transform.position;
            GetComponent<Rigidbody2D>().AddForce((-kickDirection.normalized * _kickForce) + Vector2.up * 2000);
            StartCoroutine(CooldownTick());
            _canTakeDamage = false;
        }
    }

    private IEnumerator CooldownTick() 
    {
        _animator.DamageCooldownBegin();
        float tick = _cooldown;
        while(tick > 0)
        {
            tick--;
            yield return new WaitForSeconds(1);
        }
        _canTakeDamage = true;
        _animator.DamageCooldownEnd();
    }

}
