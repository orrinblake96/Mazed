using Managers;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Camera fpsCam;
    
    public float damage = 10f;
    public float range = 100f;
    public float timeBetweenBullets = 0.15f;

    public ParticleSystem muzzleFlash;
    
    private float _timer;

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        
        if (Input.GetButton("Fire1") && _timer >= timeBetweenBullets)
        {
            FindObjectOfType<AudioManager>().Play("GunShot");
            muzzleFlash.Play();
            Shoot();
        }
    }

    void Shoot()
    {
        _timer = 0f;
        
        RaycastHit hit;
        
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
        }
    }
}
