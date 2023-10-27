using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject enemyPrefab;
    GameObject planeObject;
    Vector3 spawnPosition;


    private void Awake()
    {
        planeObject = GameObject.FindGameObjectWithTag("Ground");
    }


    private void Start()
    {
        InvokeRepeating("SpawnEnemies", 0.5f, 1f);
    }

    private void SpawnEnemies()
    {
        Vector3 spawnOffset = new(0, 0, 24);

        Vector3 inCircle = Random.insideUnitSphere * 25;
        int xComp = Random.Range(-15, 15);
        inCircle.x = xComp;
        inCircle.y = 0;
        
        
        if (inCircle.x <= playerObject.transform.position.x + 2 && inCircle.x >= playerObject.transform.position.x - 2)
        {
            
        }

        else if (inCircle.z <= playerObject.transform.position.z + 2 && inCircle.z >= playerObject.transform.position.z - 2)
        {

            
        }

        else
        {
            Instantiate(enemyPrefab, spawnOffset + inCircle, Quaternion.identity);
        }
    } 

}
