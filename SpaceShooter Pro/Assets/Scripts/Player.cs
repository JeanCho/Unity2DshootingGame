using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //private variable use _underscore
    [SerializeField]
    private AudioClip _laserSound;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _score = 0;
    [SerializeField]
    private float _speed = 5.0f;
    private float _speedModifier = 2.0f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shieldPrefab;
    [SerializeField]
    private GameObject _fireTailLeftPrefab;
    [SerializeField]
    private GameObject _fireTailRightPrefab;

    [SerializeField]
    private float _fireRate = 0.5f;

    private float _canFire = -1.0f;
    private float _powerTime = 5.0f;

    [SerializeField]
    private bool _tripleShotEnable = false;
    private bool _speedBoostEnable = false;
    private bool _speedUpEnable = false;
    private bool _shieldEnable = false;


    private SpawnManager _spawnManager;
    private UIManager _uiManager;

    public GameObject gunPoint;
    private AudioSource _laserAudioSource;


    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _laserAudioSource = GetComponent<AudioSource>();
        _laserAudioSource.clip = _laserSound;
        

        transform.position = new Vector3(0, 0, 0);
        if(_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is null");
        }

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is null");
        }
        if(_laserAudioSource == null)
        {
            Debug.LogError("The Laser Audio Source on Player is null");
        }
        
    }

    
    void Update()
    {

        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            ShootLaser();
        }
        
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        
        if(_speedBoostEnable)
        {
            transform.Translate(direction * _speed *_speedModifier* Time.deltaTime);
        }

        else
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }

        

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -6, 6), 0);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -9, 9),transform.position.y, 0);

       
    }

    void ShootLaser()
    {
            _canFire = Time.time + _fireRate;

        if (_tripleShotEnable == true)
        {
          Instantiate(_tripleShotPrefab, gunPoint.transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, gunPoint.transform.position, Quaternion.identity);

        }
        _laserAudioSource.Play();
    }

    public void Damage()
    {
        if(_shieldEnable)
        {
            _shieldEnable = false;
            _shieldPrefab.SetActive(false);

        }

        else
        {
            _lives--;
            _uiManager.UpdateLives(_lives);
        }
        if(_lives <3)
        {
            _fireTailLeftPrefab.SetActive(true);
        }
        if(_lives<2)
        {
            _fireTailRightPrefab.SetActive(true);
        }
       
        if (_lives <= 0)
        {
            
            _spawnManager.OnPlayerDeath();

            Destroy(this.gameObject);
        }
    }

    public void TrippleShotActive()
    {
        _tripleShotEnable = true;
        StartCoroutine(TripleShotShutDown(_powerTime));
        
    }
    public void SpeedBoostActive()
    {
        _speedBoostEnable = true;
        StartCoroutine(SpeedBoostShutDown(_powerTime));
        
    }

    public void ShieldActive()
    {
        if(_shieldEnable == false )
        {
            _shieldEnable = true;
            _shieldPrefab.SetActive(true);
           
        }
        
    }
    private IEnumerator TripleShotShutDown(float PowerTime)
    {
        while(true)
        {
            yield return new WaitForSeconds(PowerTime);
            _tripleShotEnable = false;
        }

    }

    private IEnumerator SpeedBoostShutDown(float PowerTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(PowerTime);
            _speedBoostEnable = false;
        }

    }

    public void AddScore(int score)
    {
        _score += score;
        _uiManager.UpdateScore(_score);
    }

    public int GetScore()
    {
        return _score;
    }
}
