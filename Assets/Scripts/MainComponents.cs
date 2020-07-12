using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainComponents : MonoBehaviour
{
    public float powerDraw, speed;
    public float currentTemp = 50.0f, maxTemp = 90.0f;
    public float tempIncTime = 2f, timeSinceLastInc = 0.0f, incAmount = 7f, speedMult = 1.0f;
    public TextMeshProUGUI tempText;
    public MainCompAttach mca;

    public static float maxTempBeforeExplosion = 114.0f, minTemp = 50.0f;

    private void Start()
    {
        tempText = GameObject.FindGameObjectWithTag(mca.ToString() + "Text").GetComponent<TextMeshProUGUI>();
    }

    public void NewGameEntered()
    {
        switch (mca)
        {
            case MainCompAttach.CPU:
                speedMult = (speed < GameController.Instance.cpuSpec) ? .5f : 1.0f;
                break;
            case MainCompAttach.GPU:
                speedMult = (speed < GameController.Instance.gpuSpec) ? .5f : 1.0f;
                break;
            case MainCompAttach.RAM:
                speedMult = (speed < GameController.Instance.ramSpec) ? .5f : 1.0f;
                break;
            case MainCompAttach.HDD:
                speedMult = (speed < GameController.Instance.hddSpec) ? .5f : 1.0f;
                break;
            default:
                break;
        }
    }    

    // Update is called once per frame
    void Update()
    {
        if(GameController.Instance.isTutorial)
        {
            if(GameController.Instance.readyToPlay)
            {
                if (timeSinceLastInc < (tempIncTime * speedMult))
                {
                    timeSinceLastInc += Time.deltaTime;
                }
                else
                {
                    timeSinceLastInc = 0;
                    currentTemp += incAmount;
                    CheckIfOverheating();
                }
            }
        }
        else
        {
            if(timeSinceLastInc < (tempIncTime * speedMult))
            {
                timeSinceLastInc += Time.deltaTime;
            }
            else
            {
                timeSinceLastInc = 0;
                currentTemp += incAmount;
                CheckIfOverheating();
            }
        }

        if(GameController.Instance.useCelc)
            currentTemp = ((currentTemp - 32) * 5 / 9);
        if(!mca.Equals(MainCompAttach.PSU))
        {
            tempText.text = mca.ToString() + ": " + Mathf.Floor(currentTemp) + "°" + GameController.Instance.temperatureType;
        }


    }

    public void DecTemp(float amount)
    {
        currentTemp = (currentTemp - amount > 50) ? currentTemp - amount : 50;
    }

    public void CheckIfOverheating()
    {
        if(currentTemp >= maxTempBeforeExplosion) //Game Over
        {
            GameController.Instance.Explode();
        }
        else if(currentTemp >= maxTemp) //Overheating
        {
            
        }

        Color32 c = new Color32();
        c.r = 255;
        c.b = (byte)(255 - (((currentTemp - 50) / maxTempBeforeExplosion) * 255));
        c.g = (byte)(255 - (((currentTemp - 50) / maxTempBeforeExplosion) * 255));
        c.a = 255;
        tempText.color = c;

    }
}
