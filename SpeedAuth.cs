using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SpeedAuth : MonoBehaviour
{
    public float value;
}







public class SpeedBaker: Baker<SpeedAuth>
{
    public override void Bake(SpeedAuth authoring)
    {
        AddComponent(new Speed
        {
            value = authoring.value
        }) ; 
    }

}
