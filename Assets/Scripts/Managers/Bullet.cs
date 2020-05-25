using Managers;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10.0f;
    
    private Vector3 _aimVec;
    private Transform _aimTarget;
    private TimeManager _timeManager;

    private void Awake()
    {
        _timeManager = GameObject.Find("Heavy").GetComponent<TimeManager>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        Destroy(gameObject, 1f);
        _aimTarget = GameObject.FindWithTag("Player").transform;
        _aimVec = _aimTarget.position - transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += Time.deltaTime * speed * _aimVec;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _timeManager.TimerIncreased();
            FindObjectOfType<AudioManager>().Play("EnemyHit");
        }
    }
}
