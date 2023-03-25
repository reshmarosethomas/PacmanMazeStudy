using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacdot : MonoBehaviour
{
    GameManager GM;

    private void Start()
    {
        // Grab access to GameManager in order to call ResetRound() function
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D co)
    {
        if (co.name == "pacman")
        {
            Destroy(gameObject);
            GM.addToScore();
            //Score ++
        }
    }
}
