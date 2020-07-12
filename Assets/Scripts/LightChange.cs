using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.Rendering;

public class LightChange : MonoBehaviour
{
    public float duration = 5;
    public List<Light> lights = new List<Light>();

    private float t;
    private Color c1 = Color.red;
    private Color c2 = Color.cyan;
    private float elapsed;

    private List<Color> rands1 = new List<Color>();
    private List<Color> rands2 = new List<Color>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("LightDecor"))
        {
            lights.Add(g.GetComponent<Light>());
            rands1.Add(Random.ColorHSV(0,1,0,1,1,1,1,1));
            rands2.Add(Random.ColorHSV(0,1,0,1,1,1,1,1));
        }
    }

    // Update is called once per frame
    void Update()
    {
       for(int i = 0; i < lights.Count; i++)
        {
            t = Mathf.PingPong(Time.time, duration) / duration;
            lights[i].color = Color.Lerp(rands1[i], rands2[i], t);
        }
    }
}
