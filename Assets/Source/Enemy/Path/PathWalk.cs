using System;
using System.Collections.Generic;
using UnityEngine;

public class PathWalk : MonoBehaviour, IPause
{

    [SerializeField] private List<PathPoint> _points;
    [SerializeField] private PathPoint _currentPoint;
    [SerializeField] private Vector2 _sizeColliderPoint;

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
        if (_currentPoint == null)
        {
            _currentPoint = _points[0];
            _speed = _points[0].Speed;
        }
    }

    private void Update()
    {
        Vector2 direction = _currentPoint.transform.position - transform.position;
        transform.Translate((direction.normalized * _speed) * Time.deltaTime);
        //if (Vector2.Distance(transform.position, _currentPoint.position) > 0.01f)
        //{
        //    Vector2 direction = _currentPoint.position - transform.position;
        //    //_rigidbody.velocity = direction.normalized * _speed;
        //    transform.Translate((direction.normalized * _speed) * Time.deltaTime);
        //}
        //else
        //    TakeNextPoint();

        if (_isPause)
            _rigidbody.velocity = Vector2.zero;

        if (!_miror)
            return;
        if (_currentPoint.transform.position.x > transform.position.x)
            _sprite.flipX = false;
        else
            _sprite.flipX = true;
    }
    public void Init()
    {
        LevelSettings.Settings.Pauses.Add(this);
    }

    public void CreatePoints()
    {
        foreach (PathPoint point in _points)
        {
            point.CreateBoxCollider(_sizeColliderPoint);
        }
    }

    #region IPause
    public void Pause()
    {
        _isPause = true;
    }

    public void Resume()
    {
        _isPause = false;
    }
    #endregion

    private void OnDrawGizmos()
    {
        foreach (PathPoint point in _points)
        {
            if (point == null)
                return;
            if (_currentPoint == transform)
                Gizmos.color = Color.red;
            else
                Gizmos.color = Color.green;
            Gizmos.DrawCube(point.transform.position, point.ColliderSize);

            if(point && _points[0])
                if (point != _points[_points.Count-1])
                    Gizmos.DrawLine(point.transform.position, _points[_points.IndexOf(point) + 1].transform.position);
                else
                    Gizmos.DrawLine(point.transform.position, _points[0].transform.position);
        }
    }

    private void TakeNextPoint()
    {
        for (int i = 0; i < _points.Count; i++)
        {
            if (_points[i] == _currentPoint)
                if (i != _points.Count -1 )
                {
                    _currentPoint = _points[i + 1];
                    _speed = _points[i + 1].Speed;
                    break;
                }
                else
                {
                    _currentPoint = _points[0];
                    _speed = _points[0].Speed;
                    break;
                }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PathPoint point))
        {
            if (point == _currentPoint)
            {
                TakeNextPoint();
            }
        }
    }

}