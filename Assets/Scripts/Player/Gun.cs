using System;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gun : MonoBehaviour
{
    public Camera fpsCam;
    public float damage = 10f;
    public float range = 100f;
    public float timeBetweenBullets = 0.15f;
    public ParticleSystem muzzleFlash;
    public GameObject bulletImpactEffect;
    
    private float _timer;
    private TimeManager _timeManager;

    // References
    private void Start()
    {
        _timeManager = GetComponent<TimeManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        _timer += Time.deltaTime;
        
        // Prevent constant firing using timers
        if (Input.GetButton("Fire1") && _timer >= timeBetweenBullets)
        {
            // Effects for the gun
            FindObjectOfType<AudioManager>().Play("GunShot");
            muzzleFlash.Play();
            Shoot();
        }
        
        if (Input.GetKeyDown(KeyCode.N))
        {
            // Effects for the gun
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void Shoot()
    {
        _timer = 0f;
        
        // Uss raycasts to return objects that are hit in the scene
        RaycastHit hit;
        
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            
            //Instantiate Bullet Impact effect
            GameObject impactBulletGameObject = Instantiate(bulletImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            
            // If time reducing object is hit then call function
            if(hit.transform.name == "TimeReducer(Clone)") _timeManager.TimerReduced(hit.transform.gameObject);
            
            // Destroy bullets to clean up scene
            Destroy(impactBulletGameObject, 2f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MazeFloor"))
        {
            // Start timer when play is in the maze
            _timeManager.TimerStart();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("MazeFloor"))
        {
            // Stop timer when they leave the maze
            _timeManager.TimerStop();
        }
    }
}
