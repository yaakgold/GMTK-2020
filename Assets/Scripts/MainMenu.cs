using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject inGamePanel, savePanel;
    public TextMeshProUGUI newGameText;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        RestartObj();
    }

    public void RestartObj()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
        StartCoroutine(LateStart(.01f));
    }

    IEnumerator LateStart(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        inGamePanel.SetActive(false);
    }

    public void UpdateNewGameText()
    {
        newGameText.text = "New Game: " + slider.value;
    }

    public void OpenTutorial()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenSavePanel()
    {
        savePanel.SetActive(true);
    }

    public void StartGame(int num)
    {
        inGamePanel.SetActive(true);
        Time.timeScale = 1;

        if(num < 4)
        {
            GameController.Instance.saveNum = num;
            GameData data = SaveSystem.Load(num);

            if (data != null)
            {
                LoadData(data);
            }

            savePanel.SetActive(false);
        }
        else
        {
            GameController.Instance.saveNum = (int)slider.value;
        }

        GameController.Instance.CollectBunny(0);

        gameObject.SetActive(false);
    }

    public void SaveGame()
    {
        SaveSystem.Save(GameController.Instance.saveNum);
    }

    public void ExitGame()
    {
        SaveSystem.Save(GameController.Instance.saveNum);

        Application.Quit();
    }

    public void LoadData(GameData data)
    {
        //Player
        GameController.Instance.numBunnies = data.numBunnies;
        GameController.Instance.dayNumber = data.dayNumber;
        GameController.Instance.player.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);

        GameController.Instance.fanSpeed = data.fanSpeed;
        GameController.Instance.fanDraw = data.fanDraw;
        GameController.Instance.fanCost = data.fanCost;

        GameController.Instance.cpuSpeed = data.cpuSpeed;
        GameController.Instance.cpuDraw = data.cpuDraw;
        GameController.Instance.cpuSpec = data.cpuSpec;
        GameController.Instance.cpuCost = data.cpuCost;

        GameController.Instance.gpuSpeed = data.gpuSpeed;
        GameController.Instance.gpuDraw = data.gpuDraw;
        GameController.Instance.gpuSpec = data.gpuSpec;
        GameController.Instance.gpuCost = data.gpuCost;

        GameController.Instance.ramSpeed = data.ramSpeed;
        GameController.Instance.ramDraw = data.ramDraw;
        GameController.Instance.ramSpec = data.ramSpec;
        GameController.Instance.ramCost = data.ramCost;

        GameController.Instance.hddSpeed = data.hddSpeed;
        GameController.Instance.hddDraw = data.hddDraw;
        GameController.Instance.hddSpec = data.hddSpec;
        GameController.Instance.hddCost = data.hddCost;

        GameController.Instance.psuWatts = data.psuWatts;
        GameController.Instance.psuCost = data.psuCost;


        GameController.Instance.timeSinceLastComp = data.timeSinceLastComp;
        GameController.Instance.timeLeftInDay = data.timeLeftInDay;
        GameController.Instance.saveNum = data.saveNum;

        GameController.Instance.wires[0].GetComponentInChildren<WireEnd>().transform.position = new Vector3(data.fanL[0], data.fanL[1], data.fanL[2]);
        GameController.Instance.wires[1].GetComponentInChildren<WireEnd>().transform.position = new Vector3(data.fanR[0], data.fanR[1], data.fanR[2]);
        GameController.Instance.wires[2].GetComponentInChildren<WireEnd>().transform.position = new Vector3(data.fanUL[0], data.fanUL[1], data.fanUL[2]);
        GameController.Instance.wires[3].GetComponentInChildren<WireEnd>().transform.position = new Vector3(data.fanUR[0], data.fanUR[1], data.fanUR[2]);

        foreach (GameObject wire in GameController.Instance.wires)
        {
            wire.GetComponentInChildren<WireEnd>().PlugInWire();
        }
    }
}
