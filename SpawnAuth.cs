using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

public class SpawnAuth : MonoBehaviour
{

    public GameObject atomPrefab;
}







public class SpawnBaker: Baker<SpawnAuth>
{
    public override void Bake(SpawnAuth authoring)
    {
        AddComponent(new SpawnFields
        {
           atomPrefab= GetEntity(authoring.atomPrefab)
        });
    }

}
