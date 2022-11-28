using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

//[Serializable]
public struct AtomFields : IComponentData
{


    public float centroidGrav;
    public float separationWeight;
    public float alignmentWeight;
    public float originGrav;
    public float socialDistance;
    public float boundary;
    public float direction;
    public float playerGrav;
    public float radar;

    public float speed;
    public float speedLimit;
    public float randomness;
    public float3 position;
    public float3 velocity;
    public float3 acc;
    public float posIncrement;


    public float3 playerShip;
    public int sector;


    public float3 initPos;
    public float3 targetPos;

    public Entity prefab;

}