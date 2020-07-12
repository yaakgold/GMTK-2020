using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireEnd : MonoBehaviour
{
    public bool isSelected = false, isOnWirePort = false;
    public Camera cam;
    public WirePort wp;
    public ComputerComponent cc;

    private Vector3 pos;

    public void PlugInWire(WirePort port)
    {
        pos = port.transform.position;
        pos.y += .1f;
        transform.position = pos;
        isOnWirePort = true;

        port.isOpen = false;

        PowerSupply wpps = port.GetComponent<PowerSupply>();

        if (wpps != null)
        {
            wpps.hasBeenPluggedIn = true;
        }

        isSelected = false;
    }

    public void PlugInWire()
    {
        RaycastHit h;
        if (Physics.Raycast(transform.position, -Vector3.up, out h))
        {
            if (h.transform.CompareTag("Wire Port"))
            {
                wp = h.transform.GetComponent<WirePort>();
                pos = h.transform.position;
                pos.y += .1f;
                transform.position = pos;
                isOnWirePort = true;

                wp.isOpen = false;

                PowerSupply wpps = wp.GetComponent<PowerSupply>();

                if (wpps != null)
                {
                    wpps.hasBeenPluggedIn = true;
                }
            }
        }
    }

    private void Start()
    {
        cam = Camera.main;
        cc = GetComponentInParent<ComputerComponent>();

        if(isSelected)
        {
            RaycastHit h;
            if(Physics.Raycast(transform.position, -Vector3.up, out h))
            {
                if (h.transform.CompareTag("Wire Port"))
                {
                    wp = h.transform.GetComponent<WirePort>();
                    pos = h.transform.position;
                    pos.y += .1f;
                    transform.position = pos;
                    isOnWirePort = true;

                    wp.isOpen = false;

                    PowerSupply wpps = wp.GetComponent<PowerSupply>();

                    if (wpps != null)
                    {
                        wpps.hasBeenPluggedIn = true;
                    }
                }
            }
            isSelected = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if(isSelected)
        {
            if(Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("WireEnd")) && hit.transform.GetComponent<WirePort>().isOpen)
            {
                if(!(cc.isCooking || cc.isCooling) )
                {
                    if (hit.transform.CompareTag("Wire Port"))
                    {
                        wp = hit.transform.GetComponent<WirePort>();
                        pos = hit.transform.position;
                        pos.y += .1f;
                        transform.position = pos;
                        isOnWirePort = true;

                        if (cc.isTurnedOn)
                            FindObjectOfType<AudioManager>().Play("UnPlug");
                    }
                }
                else
                {
                    if(cc.isTurnedOn)
                        FindObjectOfType<AudioManager>().Play("UnPlug");

                }
            }
            else
            {
                pos = Input.mousePosition;
                pos.z = 8.0f;
                pos = cam.ScreenToWorldPoint(pos);

                transform.position = pos;
                isOnWirePort = false;

                if(wp != null)
                {
                    wp = null;
                }
            }
        }
    }
}
