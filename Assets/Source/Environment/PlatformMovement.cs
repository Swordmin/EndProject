using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : PathWalk
{
    [Space(10),Header("Rotate")]
    [SerializeField] private float _angle;
    [SerializeField] private float _rotateTime;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private Quaternion _targetQuaternion;

    private void Awake()
    {
        StartCoroutine(RotateTick());
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate() 
    {
         transform.rotation = Quaternion.LerpUnclamped(transform.rotation, _targetQuaternion, _rotateSpeed * Time.deltaTime);
    }

    private void UpdateAngle() 
    {
        _targetQuaternion.eulerAngles = Quaternion.Euler(_targetQuaternion.x, _targetQuaternion.y, _targetQuaternion.z).eulerAngles + Quaternion.Euler(_targetQuaternion.x,_targetQuaternion.y, _angle).eulerAngles;
    }

    private IEnumerator RotateTick() 
    {
        while (true)
        {
            yield return new WaitForSeconds(_rotateTime);
            UpdateAngle();
        }

    }

}
