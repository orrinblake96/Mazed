using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class TimeManager : MonoBehaviour
    {

        private TextMeshProUGUI timerMinutes;
        private TextMeshProUGUI timerSeconds;
        private TextMeshProUGUI timerSeconds100;

        private float startTime;
        private float stopTime;
        private float timerTime;
        private bool isRunning = false;
        
        // Start is called before the first frame update
        private void Start()
        {
            timerMinutes = GameObject.Find("UI/TimerCanvas/Minutes").GetComponent<TextMeshProUGUI>();
            timerSeconds = GameObject.Find("UI/TimerCanvas/Seconds").GetComponent<TextMeshProUGUI>();
            timerSeconds100 = GameObject.Find("UI/TimerCanvas/SecondsHundred").GetComponent<TextMeshProUGUI>();

            TimerReset();
        }

        // Update is called once per frame
        private void Update()
        {
            timerTime = stopTime + (Time.time - startTime);

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
            int minutesInt = (int)timerTime / 60;
            int secondsInt = (int)timerTime % 60;
            int seconds100Int = (int)(Mathf.Floor((timerTime - (secondsInt + minutesInt * 60)) * 100));
            
            if (isRunning)
            {
                timerMinutes.text = (minutesInt < 10) ? "0" + minutesInt : minutesInt.ToString();
                timerSeconds.text = (secondsInt < 10) ? "0" + secondsInt : secondsInt.ToString();
                timerSeconds100.text = (seconds100Int < 10) ? "0" + seconds100Int : seconds100Int.ToString();
            }
        }


        public void TimerStart() {
            if (!isRunning) {
                print("START");
                isRunning = true;
                startTime = Time.time;       
            }
        }

        public void TimerStop()
        {
            if (isRunning)
            {
                print("STOP");
                isRunning = false;
                stopTime = timerTime;
            }
        }

        public void TimerReset()
        {
            print("RESET");
            stopTime = 0;
            isRunning = false;
            timerMinutes.text = timerSeconds.text = timerSeconds100.text = "00";
        }
    }
}
