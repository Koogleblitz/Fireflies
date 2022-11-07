using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(ParticleSystem))]
public class Attractor : MonoBehaviour
{
    //ParticleSystem[] particleSystems;
    //ParticleSystem[] particleSystems = GetComponents(ParticleSystem);
    //public GameObject particleSystemsHolder

/*    public GameObject particleSystemHolder;
    ParticleSystem[] systList;
*/



    public ParticleSystem atomSpawner;
    ParticleSystem.Particle[] atoms;

    //public ParticleSystem blueSpawner;
    /*    int[] blues;
        int[] reds;
        int[] greens;*/
    public List<int> reds = new List<int>();
    public List<int> greens = new List<int>();
    public List<int> blues = new List<int>();

    public float acc = 100f;
    public int cnt = 0;
    public float g = 0.1f;
    





    void Start()
    {


    }   




    void Update()
    {
        InitializeIfNeeded();

        if(cnt <1000)
                cnt += 1;
        else
                cnt = 1;

                
        int atomIdx = atomSpawner.GetParticles(atoms);
        var main = atomSpawner.main;
        for (int i = 0; i < atomIdx; i++) {
            if (Input.GetKeyDown("tab"))
            {
                if(atoms[i].GetCurrentColor(atomSpawner)==Color.red){
                    atoms[i].velocity += Vector3.right * -acc;

                }

            }
            atoms[i].remainingLifetime += 1;   
            var green = new Color(0f, 1f, 0f, 1f);
            var red = new Color(1f, 0f, 0f, 1f);
            var blue = new Color(0f, 0f, 1f, 1f);
            var clr = new Color(0f, 0f, 0f, 1f);

            if (cnt % 3 == 1) {
                main.startColor = new Color(0f, 1f, 0f, 1f);
                //greens.Add(i);
                //atoms[i].color = Color.blue;
            }
            else if(cnt % 3 == 2) { 
                main.startColor = new Color(0f, 0f, 1f, 1f);
                blues.Add(i);
                //atoms[i].color = Color.blue;
            }
            else { 
                main.startColor = new Color(1f, 0f, 0f, 1f);
                reds.Add(i);
                //atoms[i].color = Color.green;
            }

            if(atoms[i].GetCurrentColor(atomSpawner)==Color.red){
                reds.Add(i);
            }
            if(atoms[i].GetCurrentColor(atomSpawner)==Color.green){
                greens.Add(i);
            }
            if(atoms[i].GetCurrentColor(atomSpawner)==Color.blue){
                blues.Add(i);
            }


            if(atoms[i].GetCurrentColor(atomSpawner)==Color.red)
            {
                foreach(int j in greens){
                    Vector3 d = atoms[i].position - atoms[j].position;
                    atoms[i].position += d*0.00001f;
                    atoms[j].position -= d*0.00001f;

                }
            }
            


        } 
        atomSpawner.SetParticles(atoms, atomIdx);

     /*   for (int i = 0; i < reds.Length; i++)
        {
            reds[i].velocity += Vector3.down * acc;
        }*/
        

            /*        def rule(atoms1, atoms2, g):
                for i in range(len(atoms1)):
                    fx = 0
                    fy = 0
                    for j in range(len(atoms2)):
                        a = atoms1[i]
                        b = atoms2[j]
                        dx = a["x"] - b["x"]
                        dy = a["y"] - b["y"]
                        d = (dx * dx + dy * dy) * *0.5
                        if (d > 0 and d< 80):
                            F = g / d
                            fx += F * dx
                            fy += F * dy
                    a["vx"] = (a["vx"] + fx) * 0.5
                    a["vy"] = (a["vy"] + fy) * 0.5
                    a["x"] += a["vx"]
                    a["y"] += a["vy"]
                    if (a["x"] <= 0 or a["x"] >= window_size):
                        a["vx"] *= -1
                    if (a["y"] <= 0 or a["y"] >= window_size):
                        a["vy"] *= -1*/
    }










    void InitializeIfNeeded()
    {
        if (atomSpawner == null)
            atomSpawner = GetComponent<ParticleSystem>();

        if (atoms == null || atoms.Length < atomSpawner.main.maxParticles)
            atoms = new ParticleSystem.Particle[atomSpawner.main.maxParticles];
    }
}