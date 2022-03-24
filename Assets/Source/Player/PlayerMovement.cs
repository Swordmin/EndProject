using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerMovement : MonoBehaviour, IPause 
{
    private float _horizontal;
    [SerializeField]public float Horizontal => _horizontal;
    [SerializeField] private Joystick _joystick;
    public Joystick Joystick => _joystick;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Rigidbody2D _rigidbody;
    public Rigidbody2D Rigidbody2D => _rigidbody;
    [SerializeField] private ParticleSystem _vfxRun;

    private PlayerShoot _shoot;

    [Header("Player parameters")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _extraJumpCount;
    [SerializeField] private float _extraJump;
    [SerializeField] private Vector2 _doubleVelocity;
    private bool _pause;




    [Header("Ground handling")]
    [SerializeField] private bool _isGround;
    public bool IsGround => _isGround;
    [SerializeField] private LayerMask _groundMask;


    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _shoot = GetComponent<PlayerShoot>();
    }

    private void Start()
    {
        Init();
        if (!_shoot)
        {
            Debug.LogError("SFS ERROR: Shooting component is not found.");
            return;
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
            MoveLeft();
        if(Input.GetKey(KeyCode.D))
            MoveRight();
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            HorizontalClear();
        if(Input.GetKeyDown(KeyCode.Space))
            Jump();

        GroundChecking();
        Move();
    }

    #region Pause
    public void Init()
    {
        LevelSettings.Settings.Pauses.Add(this);
    }

    public void Pause()
    {
        _pause = true;
    }

    public void Resume()
    {
        _pause = false;
    }
    #endregion

    public void Jump() 
    {
        if (_isGround)
        {
            _rigidbody.velocity = Vector2.up * _jumpHeight;
            _isGround = false;
            _extraJump = 0;
        }
        else if (_extraJump < _extraJumpCount)
        {
            _rigidbody.drag = 3;
            _rigidbody.velocity = Vector2.up * _jumpHeight;
            _isGround = false;
            _extraJump++;
        }

    }

    public void MoveLeft() 
    {
        _vfxRun.emissionRate = 50;
        _horizontal = -_speed;
        _sprite.flipX = true;
        _vfxRun.startSpeed = -0.1f;
    }

    public void MoveRight() 
    {
        _vfxRun.emissionRate = 50;
        _horizontal = _speed;
        _sprite.flipX = false;
        _vfxRun.startSpeed = 0.1f;
    }

    public void HorizontalClear() 
    {
        _horizontal = 0;
        _vfxRun.emissionRate = 0;
    }

    public void SetDoubleVelocity(Vector2 _velocity) => _doubleVelocity = _velocity;

    private void Move()
    {
        Vector2 direction = new Vector2(_horizontal, 0);

        _rigidbody.velocity = new Vector2(direction.x * _speed, _rigidbody.velocity.y) + _doubleVelocity;

        if (_pause)
            _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y) + _doubleVelocity;
    }

    private void GroundChecking() 
    {
        _isGround = Physics2D.OverlapCircle(transform.position, 0.05f, _groundMask);

        if (!_isGround)
            _rigidbody.drag = 3;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ground") 
        {
            _extraJump = 0;
            _rigidbody.drag = 10;
        }
        if (collision.tag == "Bomb")
        {
            collision.GetComponent<Bomb>().Activate();
        }
    }

}
