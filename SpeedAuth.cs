using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

public class SpeedAuth : MonoBehaviour
{
    public float value;
    public float3 testPos;
}







public class SpeedBaker: Baker<SpeedAuth>
{
    public override void Bake(SpeedAuth authoring)
    {
        AddComponent(new SpeedTest
        {
            value = authoring.value
        }) ;

        AddComponent(new AtomFields
        {
           testPos = authoring.testPos
        });
    }

}
