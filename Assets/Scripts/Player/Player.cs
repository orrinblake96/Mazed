using System;
using UnityEngine;

public class Player : MonoBehaviour
{
        public Transform teleportingPadTarget;
        private MazeCell _currentCell;

        private void Start()
        {
            teleportingPadTarget = GameObject.Find("Enviroment/TeleportingPad").GetComponent<Transform>();
        }

        public void SetLocation (MazeCell cell) {
            _currentCell = cell;
            transform.localPosition = cell.transform.localPosition + Vector3.up/2;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.transform.position = teleportingPadTarget.transform.position;
            }
        }
    }
