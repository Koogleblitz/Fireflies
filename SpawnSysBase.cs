using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
using Unity.Mathematics;


public partial class SpawnSysBase : SystemBase
{
    protected override void OnUpdate()
    {


        //select all entities that have a certain component
        EntityQuery atoms= EntityManager.CreateEntityQuery(typeof(AtomFields));

        //get the spawner thing
        QueenFields queenSpawn= SystemAPI.GetSingleton<QueenFields>();
        //SpawnFields spawnComp= SystemAPI.GetSingleton<SpawnFields>();

        //Use syncpoint when spawning entities to avoid race conditions
        EntityCommandBuffer cmdBuffer = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(World.Unmanaged);
        //EntityCommandBuffer cmdBuffer2 = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(World.Unmanaged);



        int popCap= 100;
        if(atoms.CalculateEntityCount() < popCap){

            //Create the entity using the spawner's prefab field
            Entity spawnedEntity= cmdBuffer.Instantiate(queenSpawn.prefab);

            //[+]-- we can use this snippet to adjust the field values that the entity will spawn with
            // cmdBuffer.SetComponent(spawnedEntity, new SpeedTest{
            //     value= 5
            // });
        }

    }
}
