using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerSupply : MonoBehaviour
{
    #region Singleton
    private static PowerSupply _instance;

    public static PowerSupply Instance { get { return _instance; } }


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

    public bool hasBeenPluggedIn = false, unPlugged = false;
    public float maxPowerDraw = 100.0f;
    public WirePort wp;
    public TextMeshProUGUI currentPowerDrawText, maxPowerDrawText;

    // Start is called before the first frame update
    void Start()
    {
        GameController.Instance.maxPowerDraw = maxPowerDraw;
        wp = GetComponent<WirePort>();

        currentPowerDrawText = GameObject.FindGameObjectWithTag("CPowerText").GetComponent<TextMeshProUGUI>();
        maxPowerDrawText = GameObject.FindGameObjectWithTag("PSUText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hasBeenPluggedIn) //Game has started
        {
            unPlugged = wp.isOpen;

            if(unPlugged) //Power supply has been turned off, and we need to end the day
            {
                if(!GameController.Instance.dayOver)
                    GameController.Instance.EndDayEarly();
            }
        }

        currentPowerDrawText.text = "Power Draw: " + GameController.Instance.currentPowerDraw + " Watts";
        maxPowerDrawText.text = "PSU Max Draw: " + GameController.Instance.maxPowerDraw + " Watts";
    }
}
