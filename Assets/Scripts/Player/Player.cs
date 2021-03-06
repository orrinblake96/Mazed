﻿using Managers;
using UnityEngine;

public class Player : MonoBehaviour
{
        public Transform teleportingPadTarget;
        public GameManager gameManager;
        public ParticleSystem teleporterParticleSystem;

        private MazeNumber _mazeNumber;
        private MazeCell _currentCell;

        private void Start()
        {
            teleportingPadTarget = GameObject.Find("Enviroment/TeleportingPad").GetComponent<Transform>();
            gameManager = GameObject.Find("Enviroment/ButtonTower/Button").GetComponent<GameManager>();
            _mazeNumber = GameObject.Find("MazeNumber/Maze").GetComponent<MazeNumber>();
//            teleporterParticleSystem = GameObject.Find("TeleporterPS").GetComponent<ParticleSystem>();
        }

        // Player should be renamed Reward (Typo to be fixed)
        // Randomly places the location of the reward to leave the maze
        public void SetLocation (MazeCell cell) {
            _currentCell = cell;
            
            // moves the reward up to hover above ground
            transform.localPosition = cell.transform.localPosition + Vector3.up/2;
        }

        private void OnTriggerEnter(Collider other)
        {
            // Teleport player back to starting floor so they can continue the game
            if (other.gameObject.CompareTag("Player"))
            {
//                teleporterParticleSystem.Play();
                FindObjectOfType<AudioManager>().Play("Teleport");
                other.gameObject.transform.position = teleportingPadTarget.transform.position + (Vector3.up/2);
                other.gameObject.transform.rotation = teleportingPadTarget.transform.rotation;
                
                // Update number of mazes completed
                _mazeNumber.mazeNumber++;
                _mazeNumber.UpdateMazeNumber();

                // Resets the maze so it is destroyed, allows for new mazes to be generated by the player
                gameManager.RestartAfterReward();
            }
        }
    }
