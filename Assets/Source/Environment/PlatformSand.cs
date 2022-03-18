using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformSand : MonoBehaviour
{

    private BoxCollider2D _collider;
    private Animator _animator;
    [SerializeField] private bool _active;
    [SerializeField] private float _timerRespawn;
    [SerializeField] private float _timerActivate;
    [SerializeField] private UnityEvent _respawnEvent;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
    }

    private void Activate() 
    {
        if (!_active)
            StartCoroutine(ActivateTimer());
    }

    private void Respawn() 
    {
        StartCoroutine(RespawnTimer());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerMovement player)) 
        {
            Activate();
        }
    }

    private IEnumerator ActivateTimer() 
    {
        yield return new WaitForSeconds(_timerActivate);
        _active = true;
        _animator.Play("Activate");
        _collider.enabled = false;
    }

    private IEnumerator RespawnTimer() 
    {
        yield return new WaitForSeconds(_timerRespawn);
        _active = false;
        _respawnEvent.Invoke();

    }

}
