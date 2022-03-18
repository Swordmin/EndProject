using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimatorController : MonoBehaviour
{

    private Animator _animator;
    private PlayerMovement _player;
    private PlayerShoot _shoot;
    private PlayerHealth _health;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _player = GetComponent<PlayerMovement>();
        _shoot = GetComponent<PlayerShoot>();
        _health = GetComponent<PlayerHealth>();
    }

    private void Start()
    {
        _shoot.Shooting += Shoot;
        _health.OnHit.AddListener(Hit);
    }

    private void Update()
    {
        _animator.SetFloat("Velocity.x", Mathf.Abs(_player.Horizontal));
        _animator.SetBool("IsGround", _player.IsGround);
    }

    private void Shoot() 
    {
        _animator.SetTrigger("Shoot");
    }

    private void Hit() 
    {
        _animator.Play("Damage");
    }

    public void DamageCooldownBegin() 
    {
        _animator.SetBool("DamageCooldown", true);
    }    
    public void DamageCooldownEnd() 
    {
        _animator.SetBool("DamageCooldown", false);
    }



}
