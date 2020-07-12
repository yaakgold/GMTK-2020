using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONReader : MonoBehaviour
{
    public TextAsset jsonFile;
    public bool isForGames = true;

    // Start is called before the first frame update
    void Start()
    {
        if(isForGames)
        {
            GameList games = JsonUtility.FromJson<GameList>(jsonFile.text);

            GameController.Instance.games = games.games;
            GameController.Instance.nextGame = games.games[0];
        }
        else
        {
            TutsList tuts = JsonUtility.FromJson<TutsList>(jsonFile.text);

            GetComponent<Tutorial>().tuts = tuts.tutorialTexts;
        }
    }

}
