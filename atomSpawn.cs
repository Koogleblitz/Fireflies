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

        //Create entity manager, each world has one entity manager
        entMgmt= World.DefaultGameObjectInjectionWorld.EntityManager;

        //the postition of the spawner object that this script will attach to, atoms will spawn at this position
        position= this.transform.position;

        // -- Create archetype with System.Type of the enties' dynamic fields, archetypes = "entity template" ---
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

                    //This is he 'entity assembly line' in which each atom will be created and 'processed' before spawning
                    Entity atom= entMgmt.CreateEntity(atomBox);

                    //Decide the spawn location (same location as teh spawner itself):
                    entMgmt.AddComponentData(atom, new Translation{
                        Value = position
                        });


 
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



                    // get material, 'enable gpu instancing' in inspector for that material
                    // give the entity material and a mesh to make it visible
                    entMgmt.AddSharedComponentData(atom, new RenderMesh{
                        material= material,
                        mesh= mesh,
                        //castShadows = UnityEngine.Rendering.ShadowCastingMode.On
                        });
                    





                population= population+1;

                }
               
            }
        }
    }




}
