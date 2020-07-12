using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    public TextMeshProUGUI dayCounter, bunnyCounter;
    public TextMeshProUGUI fanCL, cpuCL, ramCL, gpuCL, hddCL, psuCL;
    public TextMeshProUGUI fanUL, cpuUL, ramUL, gpuUL, hddUL, psuUL;
    public TextMeshProUGUI titleDisp, cpuMin, ramMin, gpuMin, hddMin;
    public Button fanUP, cpuUP, ramUP, gpuUP, hddUP, psuUP;

    private void Start()
    {
        fanUP.onClick.AddListener(delegate { bool s = GameController.Instance.UpgradeComponent("FAN"); fanUP.interactable = !s; });
        cpuUP.onClick.AddListener(delegate { bool s = GameController.Instance.UpgradeComponent("CPU"); cpuUP.interactable = !s; });
        ramUP.onClick.AddListener(delegate { bool s = GameController.Instance.UpgradeComponent("RAM"); ramUP.interactable = !s; });
        gpuUP.onClick.AddListener(delegate { bool s = GameController.Instance.UpgradeComponent("GPU"); gpuUP.interactable = !s; });
        hddUP.onClick.AddListener(delegate { bool s = GameController.Instance.UpgradeComponent("HDD"); hddUP.interactable = !s; });
        psuUP.onClick.AddListener(delegate { bool s = GameController.Instance.UpgradeComponent("PSU"); psuUP.interactable = !s; });
    }

    public void StartShop()
    {
        //Enable buttons
        fanUP.interactable = true;
        cpuUP.interactable = true;
        ramUP.interactable = true;
        gpuUP.interactable = true;
        hddUP.interactable = true;
        psuUP.interactable = true;

        dayCounter.text = "Today is day #" + GameController.Instance.dayNumber;
        bunnyCounter.text = "You have " + GameController.Instance.numBunnies + " Bunnies";

        //Current Levels
        fanCL.text = "FAN:\t" + GameController.Instance.fanSpeed + " RPM\n" + GameController.Instance.fanDraw + " Watts";
        cpuCL.text = "CPU:\t" + GameController.Instance.cpuSpeed + " GHz\n" + GameController.Instance.cpuDraw + " Watts";
        ramCL.text = "RAM:\t" + GameController.Instance.ramSpeed + " MHz\n" + GameController.Instance.ramDraw + " Watts";
        gpuCL.text = "GPU:\t" + GameController.Instance.gpuSpeed + " GHz\n" + GameController.Instance.gpuDraw + " Watts";
        hddCL.text = "HDD:\t" + GameController.Instance.hddSpeed + " RPM\n" + GameController.Instance.hddDraw + " Watts";
        psuCL.text = "PSU:\t" + GameController.Instance.psuWatts + " Watts";

        //Upgrade Levels
        fanUL.text = "FAN:\t" + (GameController.Instance.fanSpeed + GameController.Instance.fanIncSpeed) + " RPM\n" + (GameController.Instance.fanDraw + GameController.Instance.fanIncDraw) + " Watts\n" + "Cost: 100";
        cpuUL.text = "CPU:\t" + (GameController.Instance.cpuSpeed + GameController.Instance.cpuIncSpeed) + " GHz\n" + (GameController.Instance.cpuDraw + GameController.Instance.cpuIncDraw) + " Watts\n" + "Cost: 100";
        ramUL.text = "RAM:\t" + (GameController.Instance.ramSpeed + GameController.Instance.ramIncSpeed) + " MHz\n" + (GameController.Instance.ramDraw + GameController.Instance.ramIncDraw) + " Watts\n" + "Cost: 100";
        gpuUL.text = "GPU:\t" + (GameController.Instance.gpuSpeed + GameController.Instance.gpuIncSpeed) + " GHz\n" + (GameController.Instance.gpuDraw + GameController.Instance.gpuIncDraw) + " Watts\n" + "Cost: 100";
        hddUL.text = "HDD:\t" + (GameController.Instance.hddSpeed + GameController.Instance.hddIncSpeed) + " RPM\n" + (GameController.Instance.hddDraw + GameController.Instance.hddIncDraw) + " Watts\n" + "Cost: 100";
        psuUL.text = "PSU:\t" + (GameController.Instance.psuWatts + GameController.Instance.psuIncWatts) + " Watts\n" + "Cost: 1000";

        //Next Game
        titleDisp.text = "Next Game:\n" + GameController.Instance.nextGame.Name;
        cpuMin.text = "CPU: " + GameController.Instance.cpuSpec + " GHz";
        ramMin.text = "RAM: " + GameController.Instance.ramSpec + " MHz";
        gpuMin.text = "GPU: " + GameController.Instance.gpuSpec + " GHz";
        hddMin.text = "HDD: " + GameController.Instance.hddSpec + " RPM";
    }
}
