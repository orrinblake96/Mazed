using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using UnityEngine;

public class MazeNumber : MonoBehaviour
{
    public int mazeNumber;
    public GameObject fireworks;
    public GameObject buttonTower;
    public GameObject teleportingPad;
    public AudioClip celebrationClip;

    private bool _gameOver = false;    
    private TextMeshProUGUI _mazeNumberText;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _mazeNumberText = GetComponent<TextMeshProUGUI>();
        _mazeNumberText.text = "Maze: " + mazeNumber;
    }

    private void Update()
    {
        if (mazeNumber == 5)
        {
            _gameOver = true;
        }
        if (_gameOver)
        {
            StartCoroutine(GameOver());
            _gameOver = false;
        }
    }

    public void UpdateMazeNumber()
    {
        _mazeNumberText.text = "Maze: " + mazeNumber;

        if (mazeNumber != 5) return;
        _audioSource.clip = celebrationClip;
        _audioSource.Play();
        teleportingPad.SetActive(true);
    }

    private IEnumerator GameOver()
    {
        buttonTower.SetActive(false);
        yield return new WaitForSeconds(.5f);
        fireworks.SetActive(true);
//        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
