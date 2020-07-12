using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    #region Singleton
    private static GameController _instance;

    public static GameController Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    public bool wireEndIsSelected = false, useCelc = false, dayOver = false, dayEnded = false, isTutorial = false, readyToPlay = false;
    public string temperatureType = "F";
    public Camera cam;
    public GameObject player, shopPanel, powerDispPanel, endGame, explosionPref;
    public float maxDist = 5;
    public float timeSinceLastComp = 0.0f, timeTillNextCompStart = 10.0f, timeChangeMult = 1.0f;
    public float minTime, maxTime, speedMult = 1.0f;
    public float currentPowerDraw = 0.0f, maxPowerDraw = 100.0f;
    public Shop shop;
    public int saveNum;

    public float tutTimeScale = 1.0f;

    public Games[] games;
    public Games nextGame;
    
    [Space(40)]
    public int dayNumber = 0, numBunnies;
    public float amountOfTimePerDay, timeLeftInDay;
    public TextMeshProUGUI timeDisp, bunCounterDisp, bunnyGameDisp, daysEndDisp, bunsEndDisp;

    [Space(20)]
    public float fanSpeed = 10;
    public float fanDraw = 5;
    public float fanIncSpeed = 15;
    public float fanIncDraw = 10;
    public int fanCost;

    [Space(10)]
    public float cpuSpeed;
    public float cpuDraw;
    public float cpuIncSpeed = .4f;
    public float cpuIncDraw = 30;
    public float cpuSpecInc;
    public float cpuSpec;
    public int cpuCost;

    [Space(10)]
    public float gpuSpeed;
    public float gpuDraw;
    public float gpuIncSpeed = .2f;
    public float gpuIncDraw = 25;
    public float gpuSpecInc;
    public float gpuSpec;
    public int gpuCost;

    [Space(10)]
    public float ramSpeed;
    public float ramDraw;
    public float ramIncSpeed = 114;
    public float ramIncDraw = 5;
    public float ramSpecInc;
    public float ramSpec;
    public int ramCost;

    [Space(10)]
    public float hddSpeed;
    public float hddDraw;
    public float hddIncSpeed = 63;
    public float hddIncDraw = 10;
    public float hddSpecInc;
    public float hddSpec;
    public int hddCost;

    [Space(10)]
    public float psuWatts;
    public float psuIncWatts = 50;
    public int psuCost;

    public List<GameObject> activatedComponents = new List<GameObject>();
    public List<GameObject> mainComponents = new List<GameObject>();
    public GameObject[] wires;

    public WireEnd we;

    public TextMeshProUGUI cpuMinT, ramMinT, gpuMinT, hddMinT;

    // Start is called before the first frame update
    void Start()
    {
        wires = GameObject.FindGameObjectsWithTag("MainFan");
        CollectBunny(0);
        timeLeftInDay = amountOfTimePerDay;
        cam = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");

        mainComponents.Add(PowerSupply.Instance.gameObject);
        psuWatts = maxPowerDraw = PowerSupply.Instance.maxPowerDraw;

        foreach (GameObject mc in GameObject.FindGameObjectsWithTag("Main Component"))
        {
            mainComponents.Add(mc);

            MainComponents mcs = mc.GetComponent<MainComponents>();

            switch (mcs.mca)
            {
                case MainCompAttach.CPU:
                    cpuSpeed = mcs.speed;
                    cpuDraw = mcs.powerDraw;
                    break;
                case MainCompAttach.GPU:
                    gpuSpeed = mcs.speed;
                    gpuDraw = mcs.powerDraw;
                    break;
                case MainCompAttach.RAM:
                    ramSpeed = mcs.speed;
                    ramDraw = mcs.powerDraw;
                    break;
                case MainCompAttach.HDD:
                    hddSpeed = mcs.speed;
                    hddDraw = mcs.powerDraw;
                    break;
                default:
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(numBunnies < 0)
        {
            Explode();
            return;
        }

        if (isTutorial)
            Time.timeScale = tutTimeScale;
        //Check if there is still time left in the day
        if(!dayOver || (isTutorial && readyToPlay))
        {
            if(isTutorial)
            {
                if(readyToPlay)
                {
                    if (timeLeftInDay > 0)
                    {
                        timeLeftInDay -= Time.deltaTime;
                        timeDisp.text = "Time Left: " + Mathf.Floor(timeLeftInDay) + "s";
                    }
                    else
                    {
                        EndDay();
                    }
                }
            }
            else
            {
                if (timeLeftInDay > 0)
                {
                    timeLeftInDay -= Time.deltaTime;
                    timeDisp.text = "Time Left: " + Mathf.Floor(timeLeftInDay) + "s";
                }
                else
                {
                    EndDay();
                }
            }
        }

        if(!dayOver)
        {
            if(timeSinceLastComp < (timeTillNextCompStart * timeChangeMult))
            {
                timeSinceLastComp += Time.deltaTime * speedMult;
            }
            else
            {
                timeSinceLastComp = 0;

                if(activatedComponents.Count > 0)
                {
                    SelectRandomComponent();
                }

                timeTillNextCompStart = Random.Range(minTime, maxTime);
            }

            if(currentPowerDraw > maxPowerDraw)
            {
                timeChangeMult = .5f;
            }
            else
            {
                timeChangeMult = 1.0f;
            }

            #region Moving Wires
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButtonUp(0) && Physics.Raycast(ray, out hit))
            {
                if(Vector3.Distance(player.transform.position, hit.transform.position) <= maxDist)
                {
                    if (hit.transform.CompareTag("Wire End"))
                    {
                        if(we == null)
                            we = hit.transform.GetComponent<WireEnd>();

                        we.isSelected = !we.isSelected;

                        wireEndIsSelected = !wireEndIsSelected;

                        if (we.wp != null)
                        {
                            we.wp.isOpen = !we.wp.isOpen;
                        }

                        if (!we.cc.isTurnedOn)
                            FindObjectOfType<AudioManager>().Play("PlugIn");

                        if (!we.isSelected)
                            we = null;
                    }
                }
            }
            #endregion
        }
    }

    public bool AddComponent(GameObject comp)
    {
        bool success = true;

        if(!activatedComponents.Contains(comp))
        {
            activatedComponents.Add(comp);
            currentPowerDraw = CalcCurrentPowerDraw();
            success = true;
        }
        else
        {
            success = false;
        }

        return success;
    }

    public bool RemoveComponent(GameObject comp)
    {
        activatedComponents.Remove(comp);
        currentPowerDraw = CalcCurrentPowerDraw();
        return true;
    }

    public float CalcCurrentPowerDraw()
    {
        float power = 0.0f;

        foreach (GameObject cc in activatedComponents)
        {
            power += cc.GetComponent<ComputerComponent>().powerDraw;
        }

        foreach (GameObject mc in mainComponents)
        {
            power += mc.GetComponent<MainComponents>().powerDraw;
        }

        return power;
    }

    public bool SelectRandomComponent()
    {
        bool success = false;

        int numCompsTested = 0;
        ComputerComponent component = null;

        while(component == null && numCompsTested < activatedComponents.Count)
        {
            component = activatedComponents[Random.Range(0, activatedComponents.Count)].GetComponent<ComputerComponent>();
            if (component.isCooking)
            {
                component = null;
                numCompsTested++;
            }
        }

        if(component != null)
        {
            component.StartFire();
            success = true;
        }

        return success;
    }

    public void CollectBunny(int amount)
    {
        numBunnies += amount;
        bunnyGameDisp.text = "Bunnies: " + numBunnies;
    }

    public bool UpgradeComponent(string componentName)
    {
        bool success = false;
        switch (componentName.ToLower())
        {
            case "fan":
                if(numBunnies >= fanCost)
                {
                    fanSpeed += fanIncSpeed;
                    fanDraw += fanIncDraw;
                    numBunnies -= fanCost;
                    success = true;
                }
                break;
            case "ram":
                if (numBunnies >= ramCost)
                {
                    ramSpeed += ramIncSpeed;
                    ramDraw += ramIncDraw;
                    numBunnies -= ramCost;
                    success = true;
                }
                break;
            case "cpu":
                if (numBunnies >= cpuCost)
                {
                    cpuSpeed += cpuIncSpeed;
                    cpuDraw += cpuIncDraw;
                    numBunnies -= cpuCost;
                    success = true;
                }
                break;
            case "gpu":
                if (numBunnies >= gpuCost)
                {
                    gpuSpeed += gpuIncSpeed;
                    gpuDraw += gpuIncDraw;
                    numBunnies -= gpuCost;
                    success = true;
                }
                break;
            case "hdd":
                if (numBunnies >= hddCost)
                {
                    hddSpeed += hddIncSpeed;
                    hddDraw += hddIncDraw;
                    numBunnies -= hddCost;
                    success = true;
                }
                break;
            case "psu":
                if (numBunnies >= psuCost)
                {
                    psuWatts += psuIncWatts;
                    numBunnies -= psuCost;
                    success = true;
                }
                break;
            default:
                Debug.LogError("Everything is broken!!!!!");
                break;
        }
        bunCounterDisp.text = "You have " + numBunnies + " Bunnies";

        return success;
    }

    public void Explode()
    {
        Time.timeScale = 1.0f;
        Instantiate(explosionPref);
        FindObjectOfType<AudioManager>().Play("Explosion");
        StartCoroutine(StopRuning(2f));

        daysEndDisp.text = "You Lasted\n" + dayNumber + " Days!";
        bunsEndDisp.text = "You Collected\n" + numBunnies + " Bunnies!";
    }

    IEnumerator StopRuning(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        Time.timeScale = 0;
        tutTimeScale = 0;

        powerDispPanel.SetActive(false);
        endGame.SetActive(true);
    }

    public void EndDay()
    {
        dayOver = true;

        nextGame = games[Random.Range(0, games.Length)];

        cpuSpec += cpuSpecInc;
        ramSpec += ramSpecInc;
        gpuSpec += gpuSpecInc;
        hddSpec += hddSpecInc;

        cpuMinT.text = "CPU: " + cpuSpec + "GHz";
        ramMinT.text = "CPU: " + ramSpec + "MHz";
        gpuMinT.text = "CPU: " + gpuSpec + "GHz";
        hddMinT.text = "CPU: " + hddSpec + "RPM";

        dayNumber++;
        powerDispPanel.SetActive(false);
        shopPanel.SetActive(true);
        shop.StartShop();
        Time.timeScale = 0.0f;
    }

    public void EndDayEarly()
    {
        if(wireEndIsSelected)
        {
            wireEndIsSelected = false;
            we.isSelected = false;
        }

        numBunnies -= 50;

        EndDay();
    }

    public void StartDay()
    {
        Time.timeScale = 1.0f;
        CollectBunny(0);
        timeLeftInDay = amountOfTimePerDay;

        powerDispPanel.SetActive(true);
        shopPanel.SetActive(false);

        if(we != null)
        {
            WirePort pswp = PowerSupply.Instance.gameObject.GetComponent<WirePort>();
            we.PlugInWire(pswp);
            we = null;
        }

        foreach (GameObject go in activatedComponents)
        {
            go.GetComponent<ComputerComponent>().ResetObj();
        }

        foreach (GameObject component in mainComponents)
        {
            MainComponents compMA = component.GetComponent<MainComponents>();

            compMA.currentTemp = MainComponents.minTemp;

            switch (compMA.mca)
            {
                case MainCompAttach.CPU:
                    compMA.speed = cpuSpeed;
                    break;
                case MainCompAttach.GPU:
                    compMA.speed = gpuSpeed;
                    break;
                case MainCompAttach.RAM:
                    compMA.speed = ramSpeed;
                    break;
                case MainCompAttach.HDD:
                    compMA.speed = hddSpeed;
                    break;
                case MainCompAttach.PSU:
                    PowerSupply.Instance.maxPowerDraw = maxPowerDraw = psuWatts;
                    break;
                default:
                    break;
            }
        }

        dayOver = false;
    }
}
