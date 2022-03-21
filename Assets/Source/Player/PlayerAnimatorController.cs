using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimatorController : MonoBehaviour, IPause
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
        Init();
        _shoot.Shooting += Shoot;
        _health.OnHit.AddListener(Hit);
    }

    private void Update()
    {
        _animator.SetFloat("Velocity.x", Mathf.Abs(_player.Horizontal));
        _animator.SetBool("IsGround", _player.IsGround);
    }


    public void Init()
    {
        LevelSettings.Settings.Pauses.Add(this);
    }

    public void Pause()
    {
        _animator.speed = 0;
    }

    public void Resume()
    {
        _animator.speed = 1;
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
