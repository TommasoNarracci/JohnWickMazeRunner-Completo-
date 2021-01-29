using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody Rigidbody;
    private Vector3 Direction;

    void SelfDestruct()
    {
        this.gameObject.SetActive(false);
    }

    public void SetDirection(Vector3 direction,Projectile proj)
    {
        Direction = direction;
        Rigidbody = GetComponent<Rigidbody>();
        Rigidbody.velocity = Direction * 100f;
        Destroy(proj.gameObject, 0.4f);
    }

    void Start()
    {
        //Invoke(nameof(SelfDestruct), 1f);
    }

}