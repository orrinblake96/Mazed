﻿using System;
using Managers;
using UnityEngine;

public class Player : MonoBehaviour
{
        public Transform teleportingPadTarget;
        public GameManager gameManager;
        
        public ParticleSystem teleporterParticleSystem;
        
        private MazeCell _currentCell;

        private void Start()
        {
            teleportingPadTarget = GameObject.Find("Enviroment/TeleportingPad").GetComponent<Transform>();
            gameManager = GameObject.Find("Enviroment/ButtonTower/Button").GetComponent<GameManager>();
//            teleporterParticleSystem = GameObject.Find("TeleporterPS").GetComponent<ParticleSystem>();
        }

        public void SetLocation (MazeCell cell) {
            _currentCell = cell;
            transform.localPosition = cell.transform.localPosition + Vector3.up/2;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
//                teleporterParticleSystem.Play();
                FindObjectOfType<AudioManager>().Play("Teleport");
                other.gameObject.transform.position = teleportingPadTarget.transform.position + (Vector3.up/2);
                other.gameObject.transform.rotation = teleportingPadTarget.transform.rotation;
                gameManager.RestartAfterReward();
            }
        }
    }
