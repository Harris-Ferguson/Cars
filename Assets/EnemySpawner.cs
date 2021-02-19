using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        Vector3 spawnPos = new Vector3(transform.position.x, 5.05f, transform.position.z);
        int rand = Random.Range(0, enemyPrefabs.Length);
        Instantiate(enemyPrefabs[rand], spawnPos, Quaternion.Euler(0, 0, 0));
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, new Vector3(4,4,4));
    }
}
