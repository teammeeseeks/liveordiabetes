﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public float Speed;
    public LayerMask CollisionMask;

    public GameObject Owner { get; private set; }
    public Vector2 Direction { get; private set; }
    public Vector2 InitialVelocity { get; private set; }

    public void Initialize(GameObject owner, Vector2 direction, Vector2 initialVelocity)
    {
        transform.right = direction;

        Owner = owner;
        Direction = direction;
        InitialVelocity = initialVelocity;
        OnInitailized();
    }

    protected virtual void OnInitailized()
    {
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if((CollisionMask.value & (1 << other.gameObject.layer)) == 0)
        {
            OnNotCollideWith(other);
        }

        var isOwner = other.gameObject == Owner;
        if(isOwner)
        {
            OnNotCollideOwner();
            return;
        }

        var takeDamage = (ITakeDamage)other.GetComponent(typeof(ITakeDamage));
        if (takeDamage != null)
        {
            OnCollideTakeDamage(other, takeDamage);
            return;
        }

        OnCollideOther(other);
    }

    protected virtual void OnCollideOther(Collider2D other)
    {
       
    }

    protected virtual void OnCollideTakeDamage(Collider2D other, ITakeDamage takeDamage)
    {
        
    }

    protected virtual void OnNotCollideOwner()
    {
        
    }

    protected virtual void OnNotCollideWith(Collider2D other)
    {
       
    }
}

