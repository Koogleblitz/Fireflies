using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[Serializable]
public struct atomFields : IComponentData
{
    // Add fields to your component here. Remember that:
    //
    // * A component itself is for storing data and doesn't 'do' anything.
    //
    // * To act on the data, you will need a System.
    //
    // * Data in a component must be blittable, which means a component can
    //   only contain fields which are primitive types or other blittable
    //   structs; they cannot contain references to classes.
    //
    // * You should focus on the data structure that makes the most sense
    //   for runtime use here. Authoring Components will be used for 
    //   authoring the data in the Editor.
    
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
    
}
