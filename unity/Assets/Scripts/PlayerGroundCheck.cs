using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    Player player;

    const string FLOOR = "Floor";

    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    void OnTriggerStay(Collider collision)
    {        
        if (collision.gameObject.CompareTag(FLOOR))
        {
            player.tagOfFooting = FLOOR;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag(FLOOR))
        {
            player.tagOfFooting = null;
        }
    }
}
