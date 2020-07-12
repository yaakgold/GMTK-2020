using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BunnyController : MonoBehaviour
{
    public NavMeshAgent nav;
    public float walkRadius = 5;
    public Animator anim;
    public GameObject destroyParticle;
    public BunnySpawner bs;

    [Range(1.0f, 10.0f)]
    public float lowerbound = 5;
    [Range(1.0f, 10.0f)]
    public float intervalWidth = 5;
    
    
    private Vector3 randomDirection;
    private float elapsedTime = 0;
    private float randWalkInterval;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        
        if(elapsedTime > randWalkInterval)
        {
            // Reset values
            elapsedTime = 0;
            randWalkInterval = Random.Range(lowerbound, lowerbound + intervalWidth);
            randomDirection = Random.insideUnitSphere * walkRadius;

            // Generate random location on NavMesh 
            randomDirection += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);

            // Go to location
            nav.SetDestination(hit.position);            
        }
        //if(!anim.GetBool("StartJump") && nav.remainingDistance > 0)
        //{
        //    anim.SetBool("StartJump", true);
        //}

        //if(anim.GetBool("StartJump") && nav.remainingDistance < 0.001f)
        //{
        //    anim.SetBool("StartJump", false);
        //}
    }

    public void DestroyBunny()
    {
        FindObjectOfType<AudioManager>().Play("BunnyDeath");
        Instantiate(destroyParticle, transform.position, transform.rotation);

        bs.numBuns--;

        Destroy(gameObject);
    }
}
