using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShoot : MonoBehaviour
{

    public UnityAction Shooting;

    [Space(10), Header("Weapon parameters")]
    [SerializeField] private float _coolDownShoot = 1;
    [SerializeField] private float _coolDownReload = 1;
    [SerializeField] private bool _canShoot = true;
    [SerializeField] private bool _canReload = true;
    [SerializeField] private int _readyBullet;


    [Space(10), Header("Camera parameters")]
    [SerializeField] private float _cameraShakeIntentsity;
    [SerializeField] private float _cameraShakeTime;

    [Space(10), Header("Goal  parameters")]
    [SerializeField] private float _damage;
    [SerializeField] private string _goalTag;


    [Space(10), Header("Player  parameters")]

    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Transform _weaponRight;
    [SerializeField] private Transform _weaponLeft;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private BulletInfo _bulletInfo;
    private PlayerSound _sound;


    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _sound = GetComponent<PlayerSound>();
    }

    public void Shoot() 
    {
        if (_canShoot && _readyBullet > 0)
        {
            Shooting?.Invoke();
            StartCoroutine(TickShoot());
            _canShoot = false;
            Bullet bullet = new Bullet();

            if (!_sprite.flipX)
                bullet = Instantiate(_bullet, _weaponRight.position, Quaternion.identity);
            else
                bullet = Instantiate(_bullet, _weaponLeft.position, Quaternion.identity);
            bullet.Side = !_sprite.flipX;
            bullet.Init(_goalTag, _damage * _readyBullet);
            if (_readyBullet == 1)
                bullet.One = true;
            else
                bullet.One = false;

            CameraShake.Instance.Shake((_cameraShakeIntentsity * _readyBullet) / 1.5f, _cameraShakeTime);

            if (_sprite.flipX)
                Kick(new Vector2((600 *_readyBullet),450 * _readyBullet));
            else
                Kick(new Vector2(-(600 * _readyBullet), 450 * _readyBullet));

            _bulletInfo.Shoot(_readyBullet);

            _readyBullet = 0;
        }
    }

    public void Reload() 
    {
        if(_readyBullet < 2 && _canReload) 
        {
            _sound.Play("Reload");
            StartCoroutine(TickReload());
            _canReload = false;
        }
    }

    public void Kick(Vector2 direction)
    {
        _rigidbody.AddForce(direction);
    }

    private IEnumerator TickShoot() 
    {
        yield return new WaitForSeconds(_coolDownShoot);
        _canShoot = true;
    } 
    private IEnumerator TickReload() 
    {
        yield return new WaitForSeconds(_coolDownReload);
        _readyBullet++;
        _bulletInfo.AddBullet(_readyBullet);
        _canReload = true;
    }

}
