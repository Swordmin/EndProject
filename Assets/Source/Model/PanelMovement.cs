using UnityEngine;
[RequireComponent(typeof(Animator))]
public class PanelMovement : MonoBehaviour
{
    [SerializeField] private PanelType _type;
    [SerializeField] private bool _isOpen;
    [SerializeField] private Animator _animator;

    public PanelType Type => _type;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Open() 
    {
        if(!_isOpen)
        {

            _isOpen = true;
            _animator.Play("Open");

        }
    }
    public void Close()
    {
        if (_isOpen)
        {

            _isOpen = false;
            _animator.Play("Close");

        }
    }
}
