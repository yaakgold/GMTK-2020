using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float speed = 5;
    public Camera cam;
    public GameObject laser;
    public ParticleSystem sparks;
    public PauseMenu pm;

    private void Start()
    {
        cam = Camera.main;
        laser = GameObject.FindGameObjectWithTag("Laser");
        sparks = GameObject.FindGameObjectWithTag("Sparks").GetComponent<ParticleSystem>();
        sparks.Stop();
        laser.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!GameController.Instance.dayOver)
        {
            if(Input.GetAxis("Horizontal") != 0)
            {
                transform.position = new Vector3(transform.position.x + Time.deltaTime * speed * Input.GetAxis("Horizontal"), transform.position.y, transform.position.z);
            }
            if(Input.GetAxis("Vertical") != 0)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Time.deltaTime * speed * Input.GetAxis("Vertical"));
            }
        
            if(!laser.activeSelf && Input.GetAxis("Fire1") > 0)
            {
                laser.SetActive(true);
                sparks.Play();
            }
            else if (laser.activeSelf && Input.GetAxis("Fire1") < 1)
            {
                laser.SetActive(false);
                sparks.Stop();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        transform.rotation = Quaternion.Euler(new Vector3(0f, -angle - 90, 0f));

        if(Input.GetAxisRaw("Cancel") > 0)
        {
            Debug.Log("This is running");
            pm.OpenPanel();
        }
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Bunny"))
        {
            GameController.Instance.CollectBunny(Random.Range(70, 115));

            collision.gameObject.GetComponent<BunnyController>().DestroyBunny();
        }
    }
}
