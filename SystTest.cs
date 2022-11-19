using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
using Unity.Mathematics;


public partial class SystTest : SystemBase
{
    //[x]-- Move atom WITHOUT referencing component field
    /*    protected override void OnUpdate()
        {
            foreach (TransformAspect transformAspect in SystemAPI.Query<TransformAspect>())
            {
                transformAspect.Position += new float3(SystemAPI.Time.DeltaTime, 0, 0);
            }
        }*/




    //[x]-- Move atom WITH referencing component field
    /*  protected override void OnUpdate()
      {
          foreach ((TransformAspect transformAspect, RefRO<SpeedTest> speed) in SystemAPI.Query<TransformAspect, RefRO<SpeedTest>>())
          {
              transformAspect.Position += new float3(SystemAPI.Time.DeltaTime*speed.ValueRO.value, 0, 0);
          }
      }*/



    //[]--  Move atom referincing a multi-field component AtomField with two AddComponents in the same Baker
    protected override void OnUpdate()
    {
        foreach ((TransformAspect transformAspect, RefRW<AtomFields> target) in SystemAPI.Query<TransformAspect, RefRW<AtomFields>>())
        {
            float3 dir = math.normalize(target.ValueRW.testPos - transformAspect.Position);

            transformAspect.Position += dir * SystemAPI.Time.DeltaTime;
        }
    }
}
