using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private float _destoyTime;
    public bool One{ private get; set; }
    public bool Side { private get; set; }

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _sprite;
    private Animator _animator;
    private string _goalTag;
    private float _damage;

    public void Init(string tag, float damage) 
    {
        _goalTag = tag;
        _damage = damage;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        StartCoroutine(DestroyTick());
    }

    private void Start()
    {
        if (One)
            _animator.Play("OneFly");
        else
            _animator.Play("TwoFly");
    }


    private void Update()
    {
        if (Side)
        {
            _rigidbody.velocity = Vector2.right * _speed;
            _sprite.flipX = false;
        }
        else
        {
            _sprite.flipX = true;
            _rigidbody.velocity = Vector2.left * _speed;
        }
        if (One)
            _animator.Play("OneFly");
        else
            _animator.Play("TwoFly");
    }

    private IEnumerator DestroyTick() 
    {
        yield return new WaitForSeconds(_destoyTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Essence essence))
        { 
            if (essence.Type == EssenceType.BulletDamage) 
            {
                collision.GetComponent<Essence>().TakeDamage(_damage);
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
            Destroy(this.gameObject);
    }

}
