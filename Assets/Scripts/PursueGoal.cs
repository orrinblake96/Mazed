using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace GRIDCITY
{ public class PursueGoal : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private NavigationCityManager _navCityManager;
        
        // Start is called before the first frame update
        void Start()
        {
            GetComponent<Renderer>().material.color = Random.ColorHSV();
            
            _navCityManager = NavigationCityManager.Instance;
            
            _agent = GetComponent<NavMeshAgent>();
            Destroy(gameObject, 10f);
            _agent.destination = _navCityManager.startLocation.position;
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
