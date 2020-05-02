using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class TimeManager : MonoBehaviour
    {

        private TextMeshProUGUI _timerMinutes;
        private TextMeshProUGUI _timerSeconds;
        private TextMeshProUGUI _timerSeconds100;

        private float _startTime;
        private float _stopTime;
        private float _timerTime;
        private bool _isRunning = false;
        private float _timeToReduce = 0;

        // Start is called before the first frame update
        private void Start()
        {
            _timerMinutes = GameObject.Find("UI/TimerCanvas/Minutes").GetComponent<TextMeshProUGUI>();
            _timerSeconds = GameObject.Find("UI/TimerCanvas/Seconds").GetComponent<TextMeshProUGUI>();
            _timerSeconds100 = GameObject.Find("UI/TimerCanvas/SecondsHundred").GetComponent<TextMeshProUGUI>();

            TimerReset();
        }

        // Update is called once per frame
        private void Update()
        {
            // updates timer to account for stopping time & reducing time
            _timerTime = _stopTime + (Time.time - _startTime - _timeToReduce);
            

            // Debug keys -- Take out before submission
            if (Input.GetKeyDown(KeyCode.H))
            {
                TimerStart();
            }
            
            if (Input.GetKeyDown(KeyCode.J))
            {
                TimerStop();
            }
            
            if (Input.GetKeyDown(KeyCode.K))
            {
                TimerReset();
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
        }


        // Start timer and set time
        public void TimerStart()
        {
            if (!_isRunning)
            {
                _isRunning = true;
                _startTime = Time.time;       
            }
        }

        // Stop timer and prevent UI from updating
        public void TimerStop()
        {
            if (_isRunning)
            {
                _timeToReduce = 0;
                _isRunning = false;
                _stopTime = _timerTime;
            }
        }

        // Used to reset time
        // Originally to be used in game but unused currently
        public void TimerReset()
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
            _timeToReduce += 3;
            Destroy(timeReducer);
        }
    }
}
