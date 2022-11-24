using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuboidSpawn : MonoBehaviour
{
    public GameObject prefab;
    public float spawnRadius;
    public int population;
    int cnt= 0;
    // Update is called once per frame
    void Update()
    {
        if(cnt<population){
            Instantiate(prefab, this.transform.position+ UnityEngine.Random.insideUnitSphere*spawnRadius, UnityEngine.Random.rotation);
        }
        cnt= (cnt<population)? cnt+1 : population;
    }
}
