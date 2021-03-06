﻿using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace Managers
{
    public class TeleportIntoMaze : MonoBehaviour
    {
        private MazeNumber _mazeNumber;
        private Animator _gameOverAnim;
        private GameObject _timerUi;
        private GameObject _mazeNumberUi;
        private GameObject _crossHairUi;
        private FirstPersonController _fpsController;
        private Gun _gunScript;

        private void Start()
        {
            _mazeNumber = GameObject.Find("MazeNumber/Maze").GetComponent<MazeNumber>();
            _gameOverAnim = GameObject.Find("UI/GameOverCanvas/GameOver").GetComponent<Animator>();
            _timerUi = GameObject.Find("TimerCanvas");
            _mazeNumberUi = GameObject.Find("MazeNumber");
            _crossHairUi = GameObject.Find("CrossHairCanvas");
            _fpsController = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
            _gunScript = GameObject.Find("Heavy").GetComponent<Gun>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                // If all mazes complete the teleporter will restart the game (NO ESCAPE)
                // Else teleport player into the maze to continue the game
                if (_mazeNumber.mazeNumber == 10)
                {
                    FindObjectOfType<AudioManager>().Play("Teleport");
                    
                    // Disables all player actions to play outro
                    _timerUi.SetActive(false);
                    _mazeNumberUi.SetActive(false);
                    _crossHairUi.SetActive(false);
                    _fpsController.enabled = false;
                    _gunScript.enabled = false;
                    
                    _gameOverAnim.SetTrigger("GameOver");
                    StartCoroutine(NoEscapeVoiceOver());
                }
                else
                {
                    FindObjectOfType<AudioManager>().Play("Teleport");
                    other.gameObject.transform.position = GameObject.Find("InsideMazeTeleporter(Clone)")
                        .GetComponent<Transform>().position;
                    gameObject.SetActive(false);
                    if (_mazeNumber.mazeNumber == 0)
                    {
                        FindObjectOfType<AudioManager>().Play("MazeInstructions");
                    }
                    if (_mazeNumber.mazeNumber == 5)
                    {
                        FindObjectOfType<AudioManager>().Play("MazeTaunt");
                    }
                }
            }
        }

        private IEnumerator NoEscapeVoiceOver()
        {
            // Plays final voiceover before restarting the game
            yield return new WaitForSeconds(1f);
            FindObjectOfType<AudioManager>().Play("NoEscape");
            yield return new WaitForSeconds(12f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
