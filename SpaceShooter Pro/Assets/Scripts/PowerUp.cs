using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    //ID for power ups
    //0 = Triple Shot
    //1 = Speed boost
    //2 = Shield
    private int _powerUpID = 0;



   
    [SerializeField]
    private AudioClip _powerUpAudioClip;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -8f)
        {
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag =="Player")
        {

            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                

                switch(_powerUpID)
                {
                    case 0:
                        player.TrippleShotActive();
                        break;
                    case 1:
                        
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldActive();
                        

                        break;
                    default:
                        Debug.Log("Somethings Wrong");
                        break;
                }

                AudioSource.PlayClipAtPoint(_powerUpAudioClip, transform.position);
                Destroy(this.gameObject);
            }
            

        }
    }
}
