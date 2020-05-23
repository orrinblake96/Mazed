﻿using System;
using UnityEngine;

namespace Managers
{
    public class EnemyAttack : MonoBehaviour
    {
        public GameObject bulletPrefab;
        public float gunInterval = 2.0f;
        public LayerMask raycastMask;

        private bool _playerInRange;
        private Transform _playerPosition;
        private float _gunTimer = 0.0f;
        
        private void Update()
        {
            _gunTimer -= Time.deltaTime;
            
            if (_playerInRange)
            {
                Quaternion targetRotation = Quaternion.LookRotation(_playerPosition.position - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 1);
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, 20f, raycastMask))
                {
                    if (hit.collider.CompareTag("Player") && _gunTimer<0.0f)
                    {
                        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                        
                        _gunTimer = gunInterval;
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _playerInRange = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _playerInRange = false;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _playerPosition = other.transform;
            }
        }
    }
}