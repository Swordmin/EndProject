using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathWalk : MonoBehaviour, IPause
{

    [SerializeField] private List<PathPoint> _points;
    [SerializeField] private Transform _currentPoint;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private float _speed;
    [SerializeField] private bool _miror;
    [SerializeField] private bool _isPause;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Init();
        _currentPoint = _points[0].Point;
        _speed = _points[0].Speed;
    }

    private void Update() 
    {
        if (Vector2.Distance(transform.position, _currentPoint.position) > 0.01f)
        {
            Vector2 direction = _currentPoint.position - transform.position;
            _rigidbody.velocity = direction.normalized * _speed;
        }
        else
            GetNextPoint();

        if (_isPause)
            _rigidbody.velocity = Vector2.zero;

        if (!_miror)
            return;
        if (_currentPoint.transform.position.x > transform.position.x)
            _sprite.flipX = false;
        else
            _sprite.flipX = true;
    }

    private void OnDrawGizmos()
    {
        foreach(PathPoint point in _points) 
        {
            if (_currentPoint == transform)
                Gizmos.color = Color.red;
            else
                Gizmos.color = Color.green;
            Gizmos.DrawSphere(point.Point.position, 0.04f);
        }
    }

    private void GetNextPoint()
    {
        for (int i = 0; i < _points.Count; i++)
        {
            if (_points[i].Point == _currentPoint)
                if (i != _points.Count-1)
                {
                    _currentPoint = _points[i + 1].Point;
                    _speed = _points[i + 1].Speed;
                    break;
                }
                else
                {
                    _currentPoint = _points[0].Point;
                    _speed = _points[0].Speed;
                    break;
                }
        }
    }

    public void Init()
    {
        LevelSettings.Settings.Pauses.Add(this);
    }

    public void Pause()
    {
        _isPause = true;
    }

    public void Resume()
    {
        _isPause = false;
    }
}

[System.Serializable]
public class PathPoint
{
    public Transform Point;
    public float Speed;
}
