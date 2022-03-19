using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthbar : MonoBehaviour
{

    [SerializeField] private Essence _essence;
    [SerializeField] private Image _healthbar;

    private void Awake()
    {
        _essence.OnHealthChange.AddListener((currentHealth, maxhealth) =>
        {
            _healthbar.fillAmount = currentHealth / maxhealth;
        });
        _essence.OnDie.AddListener(() => { Destroy(gameObject); });
    }

}
