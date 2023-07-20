using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField] private Transform player;
    Vector3 v;
    float y;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(player.position);
        v = player.position;
        y = v.y;
        v = transform.position;
        v.y = y;
        transform.position = v;
    }
}
