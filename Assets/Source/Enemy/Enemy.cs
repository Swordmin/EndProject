using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Enemy : Essence
{
    private Animator _animator => GetComponent<Animator>();
    [SerializeField] private GameObject _coin;
    [SerializeField] private float _coinCount;
    [SerializeField] private float _damage;
    [SerializeField] private PathWalk _pathWalk;
    [SerializeField] private FlyingTextEngine _flyingTextEngine;
    public float Damage => _damage;

    private void Awake()
    {
        if (TryGetComponent(out FlyingTextEngine flyingTextEngine))
            _flyingTextEngine = flyingTextEngine;
    }

    public override void TakeDamage(float damage)
    {
        _animator.Play("Damage");
        _flyingTextEngine.CreateText(transform.position, $"-{damage}");
        base.TakeDamage(damage);
    }

    public override void Die()
    {
        base.Die();
        _pathWalk.Pause();
        _animator.Play("Die");
        for(int i = 0; i < _coinCount; i++)
        {

            Rigidbody2D rigidbody = Instantiate(_coin, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            rigidbody.gravityScale = 1;
            float x = Random.Range(-200, 200);
            float y = Random.Range(200, 400);
            rigidbody.AddForce(new Vector2(x, y));

        }
    }

    private void DieInAnimation() 
    {
        Destroy(gameObject);
    }

}
