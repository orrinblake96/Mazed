﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class TimeManager : MonoBehaviour
    {
        public GameObject trailRenderer;

        private TextMeshProUGUI _timerMinutes;
        private TextMeshProUGUI _timerSeconds;
        private TextMeshProUGUI _timerSeconds100;

        private float _startTime;
        private float _stopTime;
        private float _timerTime;
        private bool _isRunning = false;
        private float _timeToReduce = 0f;
        private float _timeToIncrease = 0f;
        private float _timeToIncreaseTrailRenderer = 0f;
        private AudioManager _audioManager;

        // Start is called before the first frame update
        private void Start()
        {
            _audioManager = FindObjectOfType<AudioManager>();
            _timerMinutes = GameObject.Find("UI/TimerCanvas/Minutes").GetComponent<TextMeshProUGUI>();
            _timerSeconds = GameObject.Find("UI/TimerCanvas/Seconds").GetComponent<TextMeshProUGUI>();
            _timerSeconds100 = GameObject.Find("UI/TimerCanvas/SecondsHundred").GetComponent<TextMeshProUGUI>();

            TimerReset();
        }

        // Update is called once per frame
        private void Update()
        {
            // updates timer to account for stopping time & reducing time
            _timerTime = _stopTime + (Time.time - _startTime - _timeToReduce + _timeToIncrease + _timeToIncreaseTrailRenderer);

            if (_timerTime < 0)
            {
                Debug.Log(_timerTime);
            }
            
            // Debug keys -- Take out before submission
             
//            if (Input.GetKeyDown(KeyCode.H))
//            {
//                TimerStart();
//            }
//            
//            if (Input.GetKeyDown(KeyCode.J))
//            {
//                TimerStop();
//            }
//            
//            if (Input.GetKeyDown(KeyCode.K))
//            {
//                TimerReset();
//            }
//            
//            if (Input.GetKeyDown(KeyCode.L))
//            {
//                TimerReduced(null);
//            }
            
            // When player is in the maze they can activate the trail
            if (_isRunning && Input.GetKeyDown(KeyCode.Q) && !trailRenderer.activeSelf)
            {
                trailRenderer.SetActive(true);
                
                //adds time penalty
                TimerIncreasedByTrailRender();
            }
        }

        private void LateUpdate()
        {
            // Handles config for UI (formatting)
            int minutesInt = (int)_timerTime / 60;
            int secondsInt = (int)_timerTime % 60;
            int seconds100Int = (int)(Mathf.Floor((_timerTime - (secondsInt + minutesInt * 60)) * 100));
            
            // Removes problem of miss matching 0's in UI
            if (_isRunning)
            {
                _timerMinutes.text = (minutesInt < 10) ? "0" + minutesInt : minutesInt.ToString();
                _timerSeconds.text = (secondsInt < 10) ? "0" + secondsInt : secondsInt.ToString();
                _timerSeconds100.text = (seconds100Int < 10) ? "0" + seconds100Int : seconds100Int.ToString();
            }
            
            // Changes colours of timer
            if (minutesInt < 3 && minutesInt >= 0)
            {
                _timerMinutes.color = _timerSeconds.color = _timerSeconds100.color = Color.green;
            } 
            else if (minutesInt >= 3 && minutesInt <= 5)
            {
                _timerMinutes.color = _timerSeconds.color = _timerSeconds100.color = Color.yellow;
            }
            else if(minutesInt >= 6)
            {
                _timerMinutes.color = _timerSeconds.color = _timerSeconds100.color = Color.red;
            }
            
        }

        // Start timer and set time
        public void TimerStart()
        {
            if (_isRunning) return;
            _isRunning = true;
            _startTime = Time.time;
        }

        // Stop timer and prevent UI from updating
        public void TimerStop()
        {
            if (!_isRunning) return;
            trailRenderer.SetActive(false);
            trailRenderer.GetComponent<TrailRenderer>().Clear();
            _timeToReduce = 0;
            _timeToIncrease = 0;
            _timeToIncreaseTrailRenderer = 0;
            _isRunning = false;
            _stopTime = _timerTime;
        }

        // Used to reset time
        // Originally to be used in game but unused currently
        private void TimerReset()
        {
            _stopTime = 0;
            _isRunning = false;
            
            // Reset timer UI to default state
            _timerMinutes.text = _timerSeconds.text = _timerSeconds100.text = "00";
        }

        // Called when shot by gun
        public void TimerReduced(GameObject timeReducer)
        {
            // Update time to be reduced & destroy prefab in scene
            if (_timerTime < 10)
            {
                _timeToReduce += Math.Abs(0 - _timerTime);
            }
            else
            {
                _timeToReduce += 10;
            }
            
            Destroy(timeReducer);
            _audioManager.Play("TimeReduced");
        }
        
        public void TimerIncreased()
        {
            // Update time to be increased
            _timeToIncrease += 8;
        }
        
        private void TimerIncreasedByTrailRender()
        {
            // Update time to be increased when player uses a trail renderer
            _timeToIncreaseTrailRenderer += 30;
        }
    }
}
