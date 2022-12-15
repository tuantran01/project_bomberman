using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum Item
    {
        ExtraBomb,
        BlastRadius,
        RunFaster,
    }
    
    public Item itemType;

    private void OnPickup(GameObject player)
    {
        switch(itemType)        {
            case Item.ExtraBomb:
            player.GetComponent<BombController>().AddBomb();
            break;

            case Item.BlastRadius:
            player.GetComponent<BombController>().explosionRadius++;
            break;

            case Item.RunFaster:
            player.GetComponent<MovementController>().speed++;
            break;
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            OnPickup(other.gameObject);
        }
    }   
}
