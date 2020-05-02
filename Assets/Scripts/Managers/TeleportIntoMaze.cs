using System;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Managers
{
    public class TeleportIntoMaze : MonoBehaviour
    {
        private MazeNumber _mazeNumber;

        private void Start()
        {
            _mazeNumber = GameObject.Find("MazeNumber/Maze").GetComponent<MazeNumber>();
            Debug.Log(_mazeNumber.mazeNumber);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                // If all mazes complete the teleporter will restart the game (NO ESCAPE)
                // Else teleport player into the maze to continue the game
                if (_mazeNumber.mazeNumber == 5)
                {
                    FindObjectOfType<AudioManager>().Play("Teleport");
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                else
                {
                    FindObjectOfType<AudioManager>().Play("Teleport");
                    other.gameObject.transform.position = GameObject.Find("InsideMazeTeleporter(Clone)")
                        .GetComponent<Transform>().position;
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
