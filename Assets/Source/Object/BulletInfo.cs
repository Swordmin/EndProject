using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInfo : MonoBehaviour
{

    public int BulletCount { get; private set;}

    public Animator _animatorLeft;
    public Animator _animatorRight;
    public void AddBullet(int count) 
    {
        if(count == 1)
            _animatorLeft.Play("BulletInfoShow");
        else
            _animatorRight.Play("BulletInfoShow");
    }

    public void Shoot(int count) 
    {
        if (count == 1)
            _animatorLeft.Play("BulletInfoHide");
        else
        {
            _animatorLeft.Play("BulletInfoHide");
            _animatorRight.Play("BulletInfoHide");
        }
    }

}
