using System;
using UnityEngine;

namespace Managers
{
    public class TeleportIntoMaze : MonoBehaviour
    {
        private MazeNumber _mazeNumber;

        private void Start()
        {
            _mazeNumber = GameObject.Find("MazeNumber/Maze").GetComponent<MazeNumber>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && _mazeNumber.mazeNumber != 5)
            {
                FindObjectOfType<AudioManager>().Play("Teleport");
                other.gameObject.transform.position = GameObject.Find("InsideMazeTeleporter(Clone)").GetComponent<Transform>().position;
                gameObject.SetActive(false);
            }
            else
            {
                FindObjectOfType<AudioManager>().Play("Teleport");
                Debug.Log("here");
                gameObject.SetActive(false);
            }
        }
    }
}
