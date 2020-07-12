using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerComponent : MonoBehaviour
{
    public bool isTurnedOn = false, isOnFire = false, isCooking = false, isCooling = false;
    public string objName = "";
    public GameObject smokePrefab, firePrefab;
    public float timeSinceStartedCooking = 0.0f, timeTillFire = 5.0f;
    public float timeSinceStartedCooling = 0.0f, timeTillStopFire = 6.0f;
    public float powerDraw = 10.0f;
    public float tempDec = .01f;
    public GameObject wireEnd;

    WireEnd wire;

    private void Start()
    {
        wire = GetComponentInChildren<WireEnd>();
    }

    private void Update()
    {
        if(wire.isOnWirePort && !wire.isSelected && !wire.wp.mca.Equals(MainCompAttach.PSU)) //Turn on component
        {
            isTurnedOn = true;
            bool s = GameController.Instance.AddComponent(gameObject);
            powerDraw = GameController.Instance.fanDraw;
            if(s) tempDec += GameController.Instance.fanSpeed / 1000;
        }
        else //Turn off component
        {
            isTurnedOn = false;
            GameController.Instance.RemoveComponent(gameObject);
        }

        if(isCooking)
        {
            if(!isOnFire)
            {
                if(timeSinceStartedCooking < timeTillFire)
                {
                    if(isTurnedOn)
                        timeSinceStartedCooking += Time.deltaTime;
                }
                else //Switch to fire burning, this is bad
                {
                    Destroy(wire.GetComponentInChildren<ParticleSystem>().gameObject);
                    Instantiate(firePrefab, wireEnd.transform.position, firePrefab.transform.rotation, wire.transform);
                    isOnFire = true;
                    timeSinceStartedCooking = 0;
                }
            }

            if(!isTurnedOn)
            {
                if(isOnFire)
                {
                    if(timeSinceStartedCooling < timeTillStopFire)
                    {
                        timeSinceStartedCooling += Time.deltaTime;
                    }
                    else
                    {
                        isOnFire = false;
                        Destroy(wire.GetComponentInChildren<ParticleSystem>().gameObject);
                        Instantiate(smokePrefab, wireEnd.transform.position, smokePrefab.transform.rotation, wire.transform);
                        timeSinceStartedCooling = 0;
                    }
                }
                else
                {
                    if (timeSinceStartedCooling < timeTillStopFire * .5f)
                    {
                        timeSinceStartedCooling += Time.deltaTime;
                    }
                    else
                    {
                        isCooking = false;
                        isCooling = false;
                        Destroy(wire.GetComponentInChildren<ParticleSystem>().gameObject);
                        timeSinceStartedCooling = 0;
                    }
                }
            }
        }

        if(isTurnedOn)
        {
            if(wire.isOnWirePort)
            {
                foreach (MainComponents mc in wire.wp.connectedComponents)
                {
                    float t = tempDec - ((isCooking) ? tempDec * .25f : 0);
                    t = ((isOnFire) ? -.01f : t);
                    mc.DecTemp(t);
                }
            }
        }
    }

    public void ResetObj()
    {
        isCooking = isCooking = isOnFire = false;
        if(wire.GetComponentInChildren<ParticleSystem>() != null)
            Destroy(wire.GetComponentInChildren<ParticleSystem>().gameObject);

        timeSinceStartedCooling = timeSinceStartedCooking = 0;
    }

    public void StartFire()
    {
        isCooking = true;
        Instantiate(smokePrefab, wireEnd.transform.position, smokePrefab.transform.rotation, wire.transform);
    }
}
