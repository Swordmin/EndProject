using UnityEngine;
public class FlyingTextEngine : MonoBehaviour
{

    [SerializeField] private Color _color;
    [SerializeField] private string _text;
    [SerializeField] private FlyingText _flyingText;
    [SerializeField] private Transform _parentFlyingText;
    
    public void CreateText(Vector2 postition,string text, Color color) 
    {
        FlyingText flyingText = Instantiate(_flyingText, postition, Quaternion.identity, _parentFlyingText);
        flyingText.Initialized(text, color);
    }
    public void CreateText(Vector2 postition, string text)
    {
        FlyingText flyingText = Instantiate(_flyingText, postition, Quaternion.identity, _parentFlyingText);
        flyingText.Initialized(text, _color);
    }   
    public void CreateText(Vector2 postition, string text, Transform parent)
    {
        FlyingText flyingText = Instantiate(_flyingText, postition, Quaternion.identity, parent);
        flyingText.Initialized(text, _color);
    }
    public void CreateText(Vector2 postition)
    {
        FlyingText flyingText = Instantiate(_flyingText, postition, Quaternion.identity, _parentFlyingText);
        flyingText.Initialized(_text, _color);
    }
}

