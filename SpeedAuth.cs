using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SpeedAuth : MonoBehaviour
{
    public float value;
}







public class SpeedTestBaker: Baker<SpeedAuth>
{
    public override void Bake(SpeedAuth authoring)
    {
        AddComponent(new SpeedTest
        {
            value = authoring.value
        }) ; 
    }

}