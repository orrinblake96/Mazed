using System;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public Maze mazePrefab;

        private Maze _mazeInstance;
        private bool _readyToGenerate;

        // Start is called before the first frame update
        private void Start()
        {
//            BeginGame();
            _mazeInstance = Instantiate(mazePrefab) as Maze;
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && _readyToGenerate)
            {
                RestartGame();
            }
        }
    
        //Begin game
        private void BeginGame()
        {
            _mazeInstance = Instantiate(mazePrefab) as Maze;
            _mazeInstance.Generate();
        }
    
        //Restart game
        private void RestartGame()
        {
            StopAllCoroutines();
            Destroy(_mazeInstance.gameObject);
            BeginGame();
        }

        private void OnTriggerEnter(Collider other)
        {
            _readyToGenerate = true;
            GameObject.Find("Button/PressButtonText").GetComponent<Text>().enabled = true;
        }

        private void OnTriggerExit(Collider other)
        {
            _readyToGenerate = false;
            GameObject.Find("Button/PressButtonText").GetComponent<Text>().enabled = false;
        }
    }
}

