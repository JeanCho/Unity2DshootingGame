using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    private int _score;
    [SerializeField]
    private GameObject _gameOverImage;
    [SerializeField]
    private GameObject _restartInstruction;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Sprite[] _liveSprites;

    [SerializeField]
    private GameManager _gameManager;
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _scoreText.text = "Score: " + 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int score)
    {
        _scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int CurrentLives)
    {
        _livesImage.sprite = _liveSprites[CurrentLives];
        if (CurrentLives <= 0)
        {
            GameOverSequence();


        }
    }

    private void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverImage.SetActive(true);
        _restartInstruction.SetActive(true);
        StartCoroutine(waittime(0.5f));

        
    }
    private IEnumerator waittime(float time)
    {
        while(true)
        {
            _gameOverImage.SetActive(true);
            yield return new WaitForSeconds(time);
            _gameOverImage.SetActive(false);
            yield return new WaitForSeconds(time);
            _gameOverImage.SetActive(true);
            yield return new WaitForSeconds(time);
            _gameOverImage.SetActive(false);
            
        }
        
    }


}
