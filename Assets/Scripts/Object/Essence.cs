using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EssenceType 
{
    BulletDamage,
    NotBulletDamage
}

public class OnHealthChangeEvent : UnityEvent<float, float> { }

public class Essence : MonoBehaviour
{

    [SerializeField] private HealthParameters _parameters = new HealthParameters();
    [SerializeField] private EssenceType _type;
    public UnityEvent OnHit;
    public EssenceType Type => _type;
    public float MaxHealth { get; private set; }
    private float currentHealth;
    public float CurrentHealth 
    {
        get { return currentHealth;}

        private set 
        {
            currentHealth = value;
            OnHealthChange?.Invoke(value, MaxHealth);

            if (CurrentHealth <= 0)
                Die();
        }
    }

    public UnityEvent OnDie;

    public OnHealthChangeEvent OnHealthChange = new OnHealthChangeEvent();

    public virtual void Start()
    {
        UpdateParameters();
    }

    public virtual void TakeDamage(float damage) 
    {
        if(damage < 0)
            throw new ArgumentException("SFS ERROR: The value must be strictly positive.");

        CurrentHealth -= damage;
        OnHit?.Invoke();
    }

    public virtual void Heal(float value)
    {
        if ((CurrentHealth + value) >= MaxHealth)
        {
            CurrentHealth = MaxHealth;
            return;
        }

        CurrentHealth += value;
    }

    public virtual void Kill() 
    {
        Die();
    }

    public virtual void Die() 
    {
        OnDie?.Invoke();
    }

    public bool IsFullHealth() 
    {
        return CurrentHealth == MaxHealth ? true : false;
    }

    public void UpdateParameters() 
    {
        MaxHealth = _parameters.MaxHealth;
        CurrentHealth = _parameters.Health;
    }

}
