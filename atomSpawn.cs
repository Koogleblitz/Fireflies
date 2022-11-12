using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Rendering;

public class atomSpawn : MonoBehaviour
{
    //-- taken from atomFields  ----//
    public float centroidGrav_ui;
    public float spaceBetween_ui;
    public float direction_ui;
    public float playerGrav_ui;
    public float radar;
    public float vel_ui;
    public float3 position;
    public float3 velocity;
    public float3 acc;
    public float posIncrement;
    public float3 playerShip;
    public int sector;
    //----------------------------//


    public Material skin;
    public Mesh mesh;
    public float speedLimit;
    private EntityManager entMgmt;
    private Entity atom;
    private float zeit;
    private int population;
    private EntityArchtype atomBox;






    void Start()
    {
        population = 0;
        entMgmt= World.DefultGameObjectInjectionWorld.EntityManager;
        position= this.transform.position;

        // System.Type ---
        atomBox= entMgmt.CreateArchetype(            
            typeof(Translation),
            typeof(Rotation),
            typeof(LocalToWorld),
            typeof(RenderMesh),
            typeof(RenderBounds),
            typeof(atomFields));
    }


    void Update()
    {
        
    }
}
