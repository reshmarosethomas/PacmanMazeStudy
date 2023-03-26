using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class GhostMove : MonoBehaviour
{
    public Transform[] waypoints;
    int cur = 0;
    public float speed = 0.3f;

    GameManager GM;

    string ghostName;
    //public GameObject pacman;
    Transform pacman;

    string[] distances = new string[121];
    int distIndex = 1;
    Stopwatch watchDist = new();
    float currTime = 0f, prevTime = 0f;
    float period = 250f;

    private void Start()
    {
        // Grab access to GameManager in order to call ResetRound() function
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        pacman = GameObject.Find("pacman").GetComponent<Transform>();

        ghostName = gameObject.name;
        UnityEngine.Debug.Log(ghostName);
        distances[0] = ghostName + ": ";
        for (int i=1; i<121; i++)
        {
            distances[i] = "unfilled";
        }
        watchDist.Start();
    }

    private void Update()
    {
        currTime = watchDist.ElapsedMilliseconds;
        if (currTime-prevTime>period)
        {
            prevTime = currTime;

            Vector2 d1 = pacman.position;
            Vector2 d2 = transform.position;
            float dist = Vector2.Distance(d2, d1);
            distances[distIndex] = dist.ToString();
            distIndex++;
            //UnityEngine.Debug.Log(ghostName + ": " + dist.ToString());
        }
    }

    void FixedUpdate()
    {
        // Waypoint not reached yet? then move closer
        if (transform.position != waypoints[cur].position)
        {
            Vector2 p = Vector2.MoveTowards(transform.position,
                                            waypoints[cur].position,
                                            speed);
            GetComponent<Rigidbody2D>().MovePosition(p);
        }

        // Waypoint reached, select next one
        else cur = (cur + 1) % waypoints.Length;

        // Animation
        Vector2 dir = waypoints[cur].position - transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }

    void OnTriggerEnter2D(Collider2D co)
    {
        if (co.name == "pacman")
        {
            Destroy(co.gameObject);

            //UnityEngine.Debug.Log(distances);

            if (ghostName == "blinky")
                GM.blinkyDists = string.Join("_", distances);
            else if (ghostName == "inky")
                GM.inkyDists = string.Join("_", distances);
            else if (ghostName == "pinky")
                GM.pinkyDists = string.Join("_", distances);
            else if (ghostName == "clyde")
                GM.clydeDists = string.Join("_", distances);

            GM.pacmanDead(); //trigger end
        }

    }
}
