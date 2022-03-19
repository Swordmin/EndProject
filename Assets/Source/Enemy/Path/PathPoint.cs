using System;
using UnityEngine;
[Serializable]
public class PathPoint : MonoBehaviour
{

    [SerializeField] private float _speed;

    public float Speed => _speed;

    public Vector2 ColliderSize => GetComponent<BoxCollider2D>() ? GetComponent<BoxCollider2D>().size : new Vector2(0, 0);


    public void CreateBoxCollider(Vector2 size)
    {
        if (GetComponent<BoxCollider2D>())
            if (GetComponent<BoxCollider2D>().size == size)
                return;
            else
                GetComponent<BoxCollider2D>().size = size;
        else
        {
            gameObject.AddComponent<BoxCollider2D>();
            GetComponent<BoxCollider2D>().size = size;
        }
        gameObject.layer = LayerMask.NameToLayer("PathPoint");
    }

}
