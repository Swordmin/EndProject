using UnityEngine;
using System.Collections;
using TMPro;
public class FlyingText : MonoBehaviour, IPause
{
    [SerializeField] TextMesh _textMesh;
    [SerializeField] private Color _color;
    [SerializeField] private string _text;
    [SerializeField] private float _cooldownDelete;
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        StartCoroutine(TimerDelete());
    }

    public void Initialized(string text, Color color) 
    {       
        _textMesh.text = text;
        _textMesh.color = color;
    }


    private void Delete() 
    {
        LevelSettings.Settings.Pauses.Remove(this);
        Destroy(this.gameObject);
    }

    private IEnumerator TimerDelete() 
    {
        yield return new WaitForSeconds(_cooldownDelete);
        Delete();
    }

    public void Init()
    {
        LevelSettings.Settings.Pauses.Add(this);
    }


    public void Pause()
    {
        _animator.speed = 0;
    }

    public void Resume()
    {
        _animator.speed = 1;
    }
}
