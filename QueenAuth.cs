using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

public class QueenAuth : MonoBehaviour
{
    public float value;
    public float speed;
    public float3 initPos;
    public float3 targetPos;

    public GameObject prefab;
}







public class QueenBaker: Baker<QueenAuth>
{
    public override void Bake(QueenAuth authoring)
    {
 

        AddComponent(new QueenFields
        {
            initPos= authoring.initPos,
           targetPos = authoring.targetPos,
           speed = authoring.speed,
           prefab= GetEntity(authoring.prefab)
        });
    }

}
