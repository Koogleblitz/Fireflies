using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

public class AtomAuth : MonoBehaviour
{
    public float value;
    public float speed;
    public float3 targetPos;

}







public class AtomBaker: Baker<AtomAuth>
{
    public override void Bake(AtomAuth author)
    {

        AddComponent(new AtomFields
        {
            targetPos = author.targetPos,
            speed = author.speed,

        }) ;
    }

}
