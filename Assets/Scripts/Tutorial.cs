using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public Tuts[] tuts;
    public int tutIndex = -1;
    public GameObject text;
    public TextMeshProUGUI tutText;

    private void Start()
    {
        DisplayNextTutorial();
    }

    public void DisplayNextTutorial()
    {
        if(tutIndex < tuts.Length - 1)
        {
            tutIndex++;
            tutText.text = tuts[tutIndex].Text;

            if (tutIndex >= tuts.Length - 1)
            {
                GameController.Instance.readyToPlay = true;
            }
        }
    }
    public void DisplayPrevTutorial()
    {
        if (tutIndex > 0)
        {
            tutIndex--;
        }
            tutText.text = tuts[tutIndex].Text;
    }
}
