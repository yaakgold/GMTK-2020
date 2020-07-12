using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WirePort : MonoBehaviour
{
    public bool isOpen = true;
    public MainCompAttach mca;

    public List<MainComponents> connectedComponents;

    // Start is called before the first frame update
    void Start()
    {
        ConnectComps();
    }

    public void ConnectComps()
    {
        connectedComponents = new List<MainComponents>();
        foreach (GameObject mc in GameObject.FindGameObjectsWithTag("Main Component"))
        {
            if(mc.name.Contains(mca.ToString()))
            {
                connectedComponents.Add(mc.GetComponent<MainComponents>());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum MainCompAttach
{
    CPU,
    GPU,
    RAM,
    HDD,
    PSU
}
