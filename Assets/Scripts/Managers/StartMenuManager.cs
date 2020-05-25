using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class StartMenuManager : MonoBehaviour
    {
        private Animator _fadeAnimator;
        private static readonly int Fade = Animator.StringToHash("Fade");

        private void Start()
        {
            _fadeAnimator = GameObject.Find("FadeOutCanvas").GetComponent<Animator>();
        }

        public void PlayFadeAnim()
        {
            _fadeAnimator.SetBool(Fade, true);
        }

        public void StartGame()
        {
            StartCoroutine(NextLevel());
        }

        IEnumerator NextLevel()
        {
            // Allows smoother music to fade with animation
            yield return  new WaitForSeconds(1f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
