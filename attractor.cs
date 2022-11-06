using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Attractor : MonoBehaviour
{
    //ParticleSystem[] particleSystems;
    //ParticleSystem[] particleSystems = GetComponents(ParticleSystem);
    //public GameObject particleSystemsHolder

    public GameObject particleSystemHolder;
    ParticleSystem[] systList;




    public ParticleSystem yellowSpawner;
    ParticleSystem.Particle[] yellows;

/*    public ParticleSystem blueSpawner;
    ParticleSystem.Particle[] blues;*/

    public float acc = 100f;
    public float cnt = 0f;






    void Start()
    {
        //particleSystems = GetComponents<ParticleSystem>();
        //particleSystemsHolder =
        /*        yellowSpawner = particleSystems[0];
                blueSpawner = particleSystems[1];*/
        //yellowSpawner = GetComponent<ParticleSystem>();
        //blueSpawner = GetComponent<ParticleSystem>();*/


    /*   systList = GetComponentsInChildren<ParticleSystem>();
       blueSpawner = systList[0];*/

        



        //blueSpawner = systList[1];

    }   




    void Update()
    {
        InitializeIfNeeded();

        if(cnt <1.0f)
                cnt += 0.001f;
        else
                cnt = 0.0f;

                
        int yellowIdx = yellowSpawner.GetParticles(yellows);
        var main = yellowSpawner.main;
        for (int i = 0; i < yellowIdx; i++){
            if (Input.GetKeyDown("tab"))
            {
                yellows[i].velocity += Vector3.right * -acc;    
                yellows[i].velocity += Vector3.left * -acc;
                yellows[i].velocity += Vector3.up * -acc;
                yellows[i].velocity += Vector3.down * acc;
            }

            if (cnt%3==1)
                main.startColor = new Color(0f, 1f, 0f, 1f);
            else if (cnt%3==2)
                main.startColor = new Color(0f, 0f, 1f, 1f);    
            else
                main.startColor = new Color(1f, 0f, 1f, 1f);

        } 
        
        yellowSpawner.SetParticles(yellows, yellowIdx);


        
        
    }


    void InitializeIfNeeded()
    {
        if (yellowSpawner == null)
            yellowSpawner = GetComponent<ParticleSystem>();

        if (yellows == null || yellows.Length < yellowSpawner.main.maxParticles)
            yellows = new ParticleSystem.Particle[yellowSpawner.main.maxParticles];
    }
}
