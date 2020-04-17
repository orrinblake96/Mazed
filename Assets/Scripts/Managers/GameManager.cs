using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public Maze mazePrefab;
        public Player rewardPrefab;
        public Animator buttonPressedAnimation;

        private Maze _mazeInstance;
        private bool _readyToGenerate;
        private Player _rewardInstance;
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
            if (Input.GetKeyDown(KeyCode.E) && _readyToGenerate)
            {
                FindObjectOfType<AudioManager>().Play("ButtonPress");
                buttonPressedAnimation.SetTrigger(ButtonPressed);
                RestartGame();
            }
        }
    
        //Begin game
        private IEnumerator BeginGame()
        {
            yield return new WaitForSeconds(.5f);
            _mazeInstance = Instantiate(mazePrefab) as Maze;
            _mazeInstance.Generate();
            yield return new WaitForSeconds(1f);
            _rewardInstance = Instantiate(rewardPrefab) as Player;
            _rewardInstance.SetLocation(_mazeInstance.GetCell(_mazeInstance.RandomCoordinates));
        }
    
        //Restart game
        private void RestartGame()
        {
            StopAllCoroutines();
            Destroy(_mazeInstance.gameObject);
            if (_rewardInstance != null) {
                Destroy(_rewardInstance.gameObject);
            }
            StartCoroutine(BeginGame());
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

