using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

public class AtomAuth : MonoBehaviour
{

    public float speed;
    public float speedLimit;
    public float randomness;
    public float3 targetPos;
    public float3 velocity;

    public float centroidGrav;
    public float socialDistance;
    public float direction;
    public float playerGrav;
    public float radar;

}







public class AtomBaker: Baker<AtomAuth>
{
    public override void Bake(AtomAuth author)
    {

        AddComponent(new AtomFields
        {
            speed = author.speed,
            speedLimit= author.speedLimit,
            randomness= author.randomness,
            targetPos = author.targetPos,
            velocity= author.velocity,

            centroidGrav= author.centroidGrav,
            socialDistance= author.socialDistance,
            direction= author.direction,
            playerGrav= author.playerGrav,
            radar= author.radar

        }) ;
    }

}
