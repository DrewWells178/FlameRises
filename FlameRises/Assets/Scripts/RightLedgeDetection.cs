using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightLedgeDetection : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask whatIsLedge;
    [SerializeField] private PlayerMovement player;

    private bool canDetect = true;

    private void Update()
    {
        if(canDetect)
            player.ledgeDetectedRight = Physics2D.OverlapCircle(transform.position, radius, whatIsLedge);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == LayerMask.NameToLayer("World")) || (collision.gameObject.layer == LayerMask.NameToLayer("Surfaces")))
            canDetect = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == LayerMask.NameToLayer("World")) || (collision.gameObject.layer == LayerMask.NameToLayer("Surfaces")))
            canDetect = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
