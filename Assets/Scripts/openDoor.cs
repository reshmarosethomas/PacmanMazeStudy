using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoor : MonoBehaviour
{
    public GameObject pacman;
    Color tempColor;
    float distance;

    private void Update()
    {
        tempColor = GetComponent<SpriteRenderer>().color;

        distance = Vector2.Distance(pacman.transform.position, gameObject.transform.position);
        Debug.Log(distance);

        if (distance < 3)
        {
            tempColor.a = 0f;
            GetComponent<SpriteRenderer>().color = tempColor;

        }

        else if (distance >= 3)
        {
            tempColor.a = 1f;
            GetComponent<SpriteRenderer>().color = tempColor;
        }
    }

    // void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //tempColor = GetComponent<SpriteRenderer>().color;
    //    //if (tempColor.a > 0)
    //    //{
    //    //    tempColor.a -= 0.1f;
    //    //}

    //    //GetComponent<SpriteRenderer>().color = tempColor;
    //}
}
