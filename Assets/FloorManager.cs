using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    // Start is called before the first frame update

    public List<GameObject> Floors = new List<GameObject>();
    public List<float> Timers = new List<float>();
    public List<float> GravityForce;
    public bool start = false;
    public float Acc;
    public Vector2 WaitTime;
    public Vector2 ForceStart;
    void Start()
    {
        foreach (GameObject floor in Floors)
        {
            Timers.Add(Random.Range(WaitTime.x, WaitTime.y));
            GravityForce.Add(Random.Range(ForceStart.x, ForceStart.y));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            for (int i = 0; i < Floors.Count; i++)
            {
                if (Timers[i] > 0)
                {
                    Timers[i] -= Time.deltaTime;
                }
                else
                {
                    Timers[i] -= Time.deltaTime;
                    if (Timers[i] < -4)
                    {
                        Destroy(gameObject);
                    }
                    Vector3 temp = Floors[i].transform.position;
                    temp.y -= GravityForce[i] * Time.deltaTime;
                    GravityForce[i] += Acc * Time.deltaTime;
                    Floors[i].transform.position = temp;
                }
            }
        }
    }

}
