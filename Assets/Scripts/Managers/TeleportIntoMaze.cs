using System;
using UnityEngine;

namespace Managers
{
    public class TeleportIntoMaze : MonoBehaviour
    {
        public InsideMazeTeleporter teleporterCellLocation;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                FindObjectOfType<AudioManager>().Play("Teleport");
                other.gameObject.transform.position = GameObject.Find("InsideMazeTeleporter(Clone)").GetComponent<Transform>().position;
            }
        }
    }
}
