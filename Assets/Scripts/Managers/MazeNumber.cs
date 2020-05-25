using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using UnityEngine;

public class MazeNumber : MonoBehaviour
{
    public int mazeNumber;
//    public GameObject fireworks;
    public GameObject smoke;
    public GameObject fireworks;
    public GameObject buttonTower;
    public GameObject teleportingPad;
    public GameObject instructionDesk;
    public AudioClip celebrationClip;

    private bool _gameOver = false;    
    private TextMeshProUGUI _mazeNumberText;
    private AudioSource _audioSource;
    private Animator _gameOverAnim;

    // Start is called before the first frame update
    private void Start()
    {
        _gameOverAnim = GameObject.Find("UI/TimerCanvas").GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _mazeNumberText = GetComponent<TextMeshProUGUI>();
        _mazeNumberText.text = "Maze: " + mazeNumber;
    }

    private void Update()
    {
        //if all 10 mazes have been completed then the game is over
        if (mazeNumber == 10)
        {
            _gameOver = true;
        }
        
        // when game is over, start the celebration sequence
        if (_gameOver)
        {
            StartCoroutine(GameOver());
            _gameOver = false;
        }
    }

    //update maze number UI
    public void UpdateMazeNumber()
    {
        _mazeNumberText.text = "Maze: " + mazeNumber;

        //play celebration music and activate the teleportation pad
        if (mazeNumber != 10) return;
        _audioSource.clip = celebrationClip;
        _audioSource.Play();
        teleportingPad.SetActive(true);
    }

    // allows a break before fireworks/smoke begins
    // removes press-able button to generate another maze
    private IEnumerator GameOver()
    {
        buttonTower.SetActive(false);
        instructionDesk.SetActive(false);
        _gameOverAnim.SetTrigger("GameOver");
        yield return new WaitForSeconds(.5f);
        fireworks.SetActive(true);
        smoke.SetActive(true);
    }
}
