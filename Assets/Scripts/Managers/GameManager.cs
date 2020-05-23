using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.AI;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public Maze mazePrefab;
        public Player rewardPrefab;
        public InsideMazeTeleporter teleporterPrefab;
        public TimeReducerLoactions timeReducerPrefab;
        public Animator buttonPressedAnimation;
        public GameObject startingTeleporter;
        
        private Maze _mazeInstance;
        private bool _readyToGenerate;
        private Player _rewardInstance;
        private InsideMazeTeleporter _mazeTeleporterInstance;
        private TimeReducerLoactions _mazeTimeReducerInstances, _mazeTimeReducerInstances2, _mazeTimeReducerInstances3, _mazeTimeReducerInstances4;
        private static readonly int ButtonPressed = Animator.StringToHash("ButtonPressed");

        // Start is called before the first frame update
        private void Start()
        {
            buttonPressedAnimation = GetComponent<Animator>();
//            BeginGame();
            _mazeInstance = Instantiate(mazePrefab) as Maze;
        }

        // Update is called once per frame
        private void Update()
        {
            // Prompts player to press "E" to generate mazes
            if (Input.GetKeyDown(KeyCode.E) && _readyToGenerate && FindObjectOfType<AudioManager>().welcomeMessageFinished)
            {
                // Creates teleporter/effects to enter the maze
                startingTeleporter.SetActive(true);
                FindObjectOfType<AudioManager>().Play("ButtonPress");
                buttonPressedAnimation.SetTrigger(ButtonPressed);
                RestartGame();
            }
        }
    
        //Begin game
        private IEnumerator BeginGame()
        {
            // Instantiate entire maze
            yield return new WaitForSeconds(.4f);
            _mazeInstance = Instantiate(mazePrefab) as Maze;
            _mazeInstance.Generate();

            // Instantiate time reducer prefabs
            _mazeTimeReducerInstances = Instantiate(timeReducerPrefab) as TimeReducerLoactions;
            _mazeTimeReducerInstances.SetTimeReducerLocations(_mazeInstance.GetCell(_mazeInstance.RandomCoordinates));
            
            _mazeTimeReducerInstances2 = Instantiate(timeReducerPrefab) as TimeReducerLoactions;
            _mazeTimeReducerInstances2.SetTimeReducerLocations(_mazeInstance.GetCell(_mazeInstance.RandomCoordinates));
            
            _mazeTimeReducerInstances3 = Instantiate(timeReducerPrefab) as TimeReducerLoactions;
            _mazeTimeReducerInstances3.SetTimeReducerLocations(_mazeInstance.GetCell(_mazeInstance.RandomCoordinates));
            
            _mazeTimeReducerInstances4 = Instantiate(timeReducerPrefab) as TimeReducerLoactions;
            _mazeTimeReducerInstances4.SetTimeReducerLocations(_mazeInstance.GetCell(_mazeInstance.RandomCoordinates));

            // Instantiate reward for player to get, allows them to leave the maze and stop the timer
            _rewardInstance = Instantiate(rewardPrefab) as Player;
            _rewardInstance.SetLocation(_mazeInstance.GetCell(_mazeInstance.RandomCoordinates));
            
            // Instantiate teleporter to enter maze
            _mazeTeleporterInstance = Instantiate(teleporterPrefab) as InsideMazeTeleporter;
            _mazeTeleporterInstance.SetTeleporterLocation(_mazeInstance.GetCell(_mazeInstance.RandomCoordinates));
        }
    
        //Restart game
        private void RestartGame()
        {
            // when restarting stop any current co-routines and destroy current maze instances to allow for creation of a new random one 
            StopAllCoroutines();
            Destroy(_mazeInstance.gameObject);
            if (_rewardInstance != null) {
                Destroy(_rewardInstance.gameObject);
            }
            if (_mazeTimeReducerInstances != null) {
                Destroy(_mazeTimeReducerInstances.gameObject);
            }
            if (_mazeTimeReducerInstances2 != null) {
                Destroy(_mazeTimeReducerInstances2.gameObject);
            }
            if (_mazeTimeReducerInstances3 != null) {
                Destroy(_mazeTimeReducerInstances3.gameObject);
            }
            if (_mazeTimeReducerInstances4 != null) {
                Destroy(_mazeTimeReducerInstances4.gameObject);
            }
            if (_mazeTeleporterInstance != null) {
                Destroy(_mazeTeleporterInstance.gameObject);
            }
            StartCoroutine(BeginGame());
        }

        private void OnTriggerEnter(Collider other)
        {
            // Alloow player to generate maze by pressing the button
            _readyToGenerate = true;
            GameObject.Find("ButtonPressCanvas/PressButtonText").GetComponent<Text>().enabled = true;
        }

        private void OnTriggerExit(Collider other)
        {
            // When player moves away from the button they can no longer press it
            _readyToGenerate = false;
            GameObject.Find("ButtonPressCanvas/PressButtonText").GetComponent<Text>().enabled = false;
        }

        public void RestartAfterReward()
        {
            // Similar to previous Restart except its called from the reward the player uses to leave the maze
            StopAllCoroutines();
            Destroy(_mazeInstance.gameObject);
            if (_rewardInstance != null) {
                Destroy(_rewardInstance.gameObject);
            }
            if (_mazeTimeReducerInstances != null) {
                Destroy(_mazeTimeReducerInstances.gameObject);
            }
            if (_mazeTimeReducerInstances2 != null) {
                Destroy(_mazeTimeReducerInstances2.gameObject);
            }
            if (_mazeTimeReducerInstances3 != null) {
                Destroy(_mazeTimeReducerInstances3.gameObject);
            }
            if (_mazeTimeReducerInstances4 != null) {
                Destroy(_mazeTimeReducerInstances4.gameObject);
            }
            if (_mazeTeleporterInstance != null) {
                Destroy(_mazeTeleporterInstance.gameObject);
            }
            _mazeInstance = Instantiate(mazePrefab) as Maze;
        }
    }
}

