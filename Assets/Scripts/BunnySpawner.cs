using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnySpawner : MonoBehaviour
{
    public Transform[] spawnLocations;
    public GameObject bunny;
    public float timeTillNextBun, timeBetweenBuns;
    public int numBuns, maxBuns;

    // Start is called before the first frame update
    void Start()
    {
        spawnLocations = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(numBuns < maxBuns)
        {
            timeTillNextBun += Time.deltaTime;

            if(timeTillNextBun >= timeBetweenBuns)
            {
                timeTillNextBun = 0;

                BunnyController bc = Instantiate(bunny, spawnLocations[Random.Range(0, spawnLocations.Length)].position, Quaternion.identity).GetComponent<BunnyController>();
                bc.bs = this;
                
                numBuns++;
            }
        }
    }
}
