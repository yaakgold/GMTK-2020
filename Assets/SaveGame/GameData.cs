using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //Player data
    public int numBunnies;
    public int dayNumber;
    public float[] position;

    //Component values
    public float fanSpeed;
    public float fanDraw;
    public int fanCost;

    public float cpuSpeed;
    public float cpuDraw;
    public float cpuSpec;
    public int cpuCost;

    public float gpuSpeed;
    public float gpuDraw;
    public float gpuSpec;
    public int gpuCost;

    public float ramSpeed;
    public float ramDraw;
    public float ramSpec;
    public int ramCost;

    public float hddSpeed;
    public float hddDraw;
    public float hddSpec;
    public int hddCost;

    public float psuWatts;
    public int psuCost;

    //Game data
    public float timeSinceLastComp;
    public float timeLeftInDay;
    public int saveNum;

    //Wire connections
    public float[] fanL;
    public float[] fanR;
    public float[] fanUL;
    public float[] fanUR;

    //Constructor to actually get all the values setup above
    public GameData()
    {
        //Player
        numBunnies = GameController.Instance.numBunnies;
        dayNumber = GameController.Instance.dayNumber;
        position = new float[3];
        position[0] = GameController.Instance.player.transform.position.x;
        position[1] = GameController.Instance.player.transform.position.y;
        position[2] = GameController.Instance.player.transform.position.z;

        //Components
        fanSpeed = GameController.Instance.fanSpeed;
        fanDraw = GameController.Instance.fanDraw;
        fanCost = GameController.Instance.fanCost;

        cpuSpeed = GameController.Instance.cpuSpeed;
        cpuDraw = GameController.Instance.cpuDraw;
        cpuSpec = GameController.Instance.cpuSpec;
        cpuCost = GameController.Instance.cpuCost;

        gpuSpeed = GameController.Instance.gpuSpeed;
        gpuDraw = GameController.Instance.gpuDraw;
        gpuSpec = GameController.Instance.gpuSpec;
        gpuCost = GameController.Instance.gpuCost;

        ramSpeed = GameController.Instance.ramSpeed;
        ramDraw = GameController.Instance.ramDraw;
        ramSpec = GameController.Instance.ramSpec;
        ramCost = GameController.Instance.ramCost;

        hddSpeed = GameController.Instance.hddSpeed;
        hddDraw = GameController.Instance.hddDraw;
        hddSpec = GameController.Instance.hddSpec;
        hddCost = GameController.Instance.hddCost;

        psuWatts = GameController.Instance.psuWatts;
        psuCost = GameController.Instance.psuCost;

        //Game
        timeSinceLastComp = GameController.Instance.timeSinceLastComp;
        timeLeftInDay = GameController.Instance.timeLeftInDay;
        saveNum = GameController.Instance.saveNum;

        //Wires
        fanL = new float[3];
        fanR = new float[3];
        fanUL = new float[3];
        fanUR = new float[3];

        fanL[0] = GameController.Instance.wires[0].GetComponentInChildren<WireEnd>().transform.position.x; 
        fanL[1] = 2.0f; 
        fanL[2] = GameController.Instance.wires[0].GetComponentInChildren<WireEnd>().transform.position.z;

        fanR[0] = GameController.Instance.wires[1].GetComponentInChildren<WireEnd>().transform.position.x;
        fanR[1] = 2.0f;
        fanR[2] = GameController.Instance.wires[1].GetComponentInChildren<WireEnd>().transform.position.z;

        fanUL[0] = GameController.Instance.wires[2].GetComponentInChildren<WireEnd>().transform.position.x;
        fanUL[1] = 2.0f;
        fanUL[2] = GameController.Instance.wires[2].GetComponentInChildren<WireEnd>().transform.position.z;

        fanUR[0] = GameController.Instance.wires[3].GetComponentInChildren<WireEnd>().transform.position.x;
        fanUR[1] = 2.0f;
        fanUR[2] = GameController.Instance.wires[3].GetComponentInChildren<WireEnd>().transform.position.z;

    }
}
