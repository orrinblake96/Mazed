using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public Maze mazePrefab;

        private Maze _mazeInstance;
        
        // Start is called before the first frame update
        private void Start()
        {
            BeginGame();
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RestartGame();
            }
        }
    
        //Begin game
        private void BeginGame()
        {
            _mazeInstance = Instantiate(mazePrefab);
            StartCoroutine(_mazeInstance.Generate());
        }
    
        //Restart game
        private void RestartGame()
        {
            StopAllCoroutines();
            Destroy(_mazeInstance.gameObject);
            BeginGame();
        }
    }
}

