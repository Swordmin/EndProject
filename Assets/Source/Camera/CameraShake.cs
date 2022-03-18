using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{

    [SerializeField] private float _shakeTimer;
    [SerializeField] private float _shakeTimerTotal;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private CinemachineBasicMultiChannelPerlin _perlin;
    [SerializeField] private float _intensity;

    public static CameraShake Instance;

    private void Awake()
    {
        if (!Instance)
            Instance = this;

        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _perlin = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Shake(float intensity, float time) 
    {
        _perlin.m_AmplitudeGain = intensity;
        _shakeTimer = time;
        _shakeTimerTotal = time;
        _intensity = intensity;
    }

    private void Update()
    {
        if(_shakeTimer > 0) 
        {
            _shakeTimer -= Time.deltaTime;
            _perlin.m_AmplitudeGain = Mathf.Lerp(_intensity, 0f, 1 - (_shakeTimer / _shakeTimerTotal));
        }
    }



}
