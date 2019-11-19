using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{

    [SerializeField]
    private float _speedEL = 15.0f;
    private Player _player;


    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

    }
    void Update()
    {
        transform.Translate(Vector3.down * _speedEL * Time.deltaTime);

        if (transform.position.y < -8f)
        {
            if (transform.parent != null)
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.tag == "Player")
        {



            _player.Damage();
            Destroy(this.gameObject);
        }


    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{

    //    Destroy(this.gameObject);
    //}

}