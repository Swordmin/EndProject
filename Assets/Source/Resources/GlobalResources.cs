using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnCoinChangeEvent : UnityEvent<float> { }
public class OnStarChangeEvent : UnityEvent<float> { }

public class GlobalResources : MonoBehaviour
{

    public static GlobalResources Resources;

    public float Coins => _coins;
    public float Stars => _stars;

    public OnCoinChangeEvent OnCoinsChange = new OnCoinChangeEvent();
    public OnStarChangeEvent OnStarsChange = new OnStarChangeEvent();

    [SerializeField] private float _coins;
    [SerializeField] private float _stars;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (!Resources)
            Resources = this;
    }

    public void AddCoins(float count) 
    {
        if(count < 0)
        {
            Debug.LogError("SFS ERROR: Value must be positive");
            throw new ArgumentException($"Value must be positive: {nameof(count)}");
        }
        _coins += count;
        OnCoinsChange?.Invoke(_coins);
    }
    public void AddStars(float count)
    {
        if (count < 0)
        {
            Debug.LogError("SFS ERROR: Value must be positive");
            throw new ArgumentException($"Value must be positive: {nameof(count)}");
        }
        _stars += count;
        OnStarsChange?.Invoke(_stars);
    }

}

