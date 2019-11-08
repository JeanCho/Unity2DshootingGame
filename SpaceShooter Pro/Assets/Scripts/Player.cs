using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //private variable use _underscore
    public float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        //7 ~ -4    -8~8
    }

    // Update is called once per frame
    void Update()
    {

        CalculateMovement();

    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        



        transform.Translate(direction * speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -8, 8), 0);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -4, 7),transform.position.y, 0);

        //if (transform.position.x >= 7)
        //{
        //    transform.position = new Vector3(7, transform.position.y, 0);
        //}
        //if (transform.position.x <= -4)
        //{
        //    transform.position = new Vector3(-4, transform.position.y, 0);
        //}
        //if (transform.position.y >= 8)
        //{
        //    transform.position = new Vector3(transform.position.x, 8, 0);
        //}
        //if (transform.position.y <= -8)
        //{
        //    transform.position = new Vector3(transform.position.x, -8, 0);
        //}
    }
}
