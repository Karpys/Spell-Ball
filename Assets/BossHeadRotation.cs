using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHeadRotation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private float ActualRotation;
    [SerializeField]private GameObject Head;
    private float TargetRotation;
    private float timer;
    public float LerpDuration = 1.5f;

    public List<float> ListRotationHead = new List<float>();

    
    void Start()
    {
        ActualRotation = Head.transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer<LerpDuration)
            timer += Time.deltaTime;

        ActualRotation = Head.transform.eulerAngles.y;
        float NewRot = Mathf.Lerp(Head.transform.eulerAngles.y, TargetRotation, timer/LerpDuration);
        Head.transform.eulerAngles = new Vector3(Head.transform.eulerAngles.x, NewRot, Head.transform.eulerAngles.z);
    }

    public void SetTargetRotation(int id)
    {
        timer = 0;
        TargetRotation = ListRotationHead[id];
    }
}
