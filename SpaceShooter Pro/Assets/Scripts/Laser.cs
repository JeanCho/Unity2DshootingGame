﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    [SerializeField]
    private float _speed = 15.0f;

   
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if(transform.position.y >8f)
        {
            if(transform.parent != null)
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }


    //private void OnTriggerEnter2D(Collider2D other)
    //{
        
    //    Destroy(this.gameObject);
    //}
}
