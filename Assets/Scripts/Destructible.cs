using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public float destructionTime = 1f;

    public float chance = 1f;
    public GameObject[] spawnItems;
    void Start()
    {
        Destroy(gameObject, destructionTime);
    }

    private void OnDestroy() 
    {
        if(spawnItems.Length > 0 && Random.value < chance)
        {
            int random = Random.Range(0, spawnItems.Length);
            Instantiate(spawnItems[random], transform.position, Quaternion.identity);
    
        }
    }          
    
}
