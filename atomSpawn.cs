using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Rendering;

public class atomSpawn : MonoBehaviour
{
    //-- taken from atomFields  ----//
    public float centroidGrav_ui;
    public float socialDistance_ui;
    public float direction_ui;
    public float playerGrav_ui;
    public float radar_ui;
    public float speed;
    public float posIncrement;
    public float3 playerShip;
    public int sector;
    public float3 position;
    public float3 velocity;
    public float3 acc;
    //----------------------------//
    public float speedLimit;

    //-- Material and Mesh --
    public Material material;
    public Mesh mesh;

    //-- Entity Management --  //
    private EntityManager entMgmt;
    private Entity atom;
    private EntityArchetype atomBox;


    // -- population control -- //
    private int population;
    public int populationControl;
    public int spawnRate;
    public int spawnInterval;
    private int tick;



    void Start()
    {
        population = 0;
        tick= 0;
        entMgmt= World.DefaultGameObjectInjectionWorld.EntityManager;
        position= this.transform.position;

        // -- Create archetype with System.Type of the enties' dynamic fields ---
        atomBox= entMgmt.CreateArchetype(            
            typeof(Translation),
            typeof(Rotation),
            typeof(LocalToWorld),
            typeof(RenderMesh),
            typeof(RenderBounds),
            typeof(atomFields)
        );
    }


    void Update()
    {
        if(tick < spawnInterval){
            tick+= 1;
        }else{
            tick= 0;

            for(int i=0 ; i<= spawnRate; i++){
                if(population <= populationControl){
                    Entity atom= entMgmt.CreateEntity(atomBox);

                    entMgmt.AddComponentData(atom, new Translation{Value = position});



                    entMgmt.AddComponentData(atom, new atomFields{
                        centroidGrav_ui= centroidGrav_ui,
                        socialDistance_ui= socialDistance_ui,
                        direction_ui= direction_ui,
                        playerGrav_ui= playerGrav_ui,
                        radar_ui= radar_ui,
                        speed= speedLimit,
                        position= position,
                        velocity= math.normalize(UnityEngine.Random.insideUnitSphere)*speedLimit,
                        acc= acc,
                        posIncrement= posIncrement, 
                        playerShip= playerShip,
                        sector= sector
                    });


                    entMgmt.AddSharedComponentData(atom, new RenderMesh{
                        // get material, 'enable gpu instancing' in inspector for that material
                        // assign the spawn script to an empty object
                        //drag the mesh and material to the spawn script in the inspector                        


                        // material= 
                        // mesh= 
                    });















                }
            }
        }
    }
}
