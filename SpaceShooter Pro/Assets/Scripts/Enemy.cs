using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    private Player _player ;
    private Animator _anim;
    private AudioSource _explosionAudioSource;
    [SerializeField]
    private AudioClip _explosionAudioClip;
    [SerializeField]
    private AudioClip _laserSound;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;

    private float _canFire = -1.0f;
    public GameObject gunPoint;

    private AudioSource _laserAudioSource;
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _anim = GetComponent<Animator>();
        _explosionAudioSource = GetComponent<AudioSource>();
        _explosionAudioSource.clip = _explosionAudioClip;
        _laserAudioSource = GetComponent<AudioSource>();
        _laserAudioSource.clip = _laserSound;
        if (_explosionAudioSource == null)
        {
            Debug.LogError("Explosion Audio Source on Enemy is null");
        }
        if (_player == null)
        {
            Debug.LogError("Can not find player");
        }

        if(_anim == null)
        {
            Debug.LogError("Can not find Enemy Animator");
        }
    }

    void Update()
    {
        
       

        
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(Time.time > _canFire&& transform.position.y <6f)
        {
            ShootLaser();
        }

        if (transform.position.y < -8f)
        {
            int randomX = Random.Range(-9, 9);
            transform.position = new Vector3(randomX, 8, 0);
        }
    }


    private void ShootLaser()
    {
        _canFire = Time.time + _fireRate;

        

        
        Instantiate(_laserPrefab, gunPoint.transform.position, Quaternion.identity);

        
        _laserAudioSource.Play();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {

            _player.AddScore(20);


            _anim.SetTrigger("OnEnemyDeath");
            _explosionAudioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(other.gameObject);
            Destroy(this.gameObject,2.2f);
            _speed = 1.0f;


        }
        if (other.tag == "Player")
        {
            
           
            
            _player.Damage();
            _anim.SetTrigger("OnEnemyDeath");
            _explosionAudioSource.Play();

            Destroy(GetComponent<Collider2D>());

            Destroy(this.gameObject, 2.2f);
            _speed = 0.8f;


        }

    }
}
