using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

//[Serializable]
public struct AtomFields : IComponentData
{


    public float centroidGrav_ui;
    public float socialDistance_ui;
    public float direction_ui;
    public float playerGrav_ui;
    public float radar_ui;

    public float speed;
    public float3 position;
    public float3 velocity;
    public float3 acc;
    public float posIncrement;


    public float3 playerShip;
    public int sector;


    public float3 testPos;

}