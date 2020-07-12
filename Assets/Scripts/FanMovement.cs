using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanMovement : MonoBehaviour
{
    public float rpm = 1000;
    public GameObject[] fans;

    // Start is called before the first frame update
    void Start()
    {
        fans = GameObject.FindGameObjectsWithTag("FanDecor");
        rpm = rpm / 60; // turn it to rotations per second, easier computation
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject fan in fans)
        {
            fan.transform.RotateAroundLocal(Vector3.forward, Time.deltaTime * rpm);
        }
    }
}
