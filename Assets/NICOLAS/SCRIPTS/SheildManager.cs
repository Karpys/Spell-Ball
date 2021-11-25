using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheildManager : MonoBehaviour
{
    public GameObject prefabShield;
    public List<GameObject> sheilds;
    public BossAction.ShieldStats Stats;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        
        for(int i  = 0;i< Stats.Number; i++)
        {            
            GameObject shield = GameObject.Instantiate(prefabShield, gameObject.transform.position, Quaternion.identity);
            shield.GetComponent<Sheild>().SetColor(RandomColor());
            shield.transform.localScale = new Vector3(shield.transform.localScale.x + 0.25f * i, shield.transform.localScale.y + 0.25f * i, shield.transform.localScale.z + 0.25f * i); 
            shield.transform.SetParent(gameObject.transform);
            sheilds.Add(shield);
            if (i + 1 == Stats.Number)
                shield.GetComponent<Sheild>().lastSield = true;
        }
        BossBehavior.Boss.NextAction();
    }

    // Update is called once per frame
    void Update()
    {
        /*time += Time.deltaTime;
        if (time >= 5)
        {
            ChangeLastSheild();
            time = -10000000000;
        }*/

    }

    public void ChangeLastSheild()
    {
        StartCoroutine("DestroyShield");
    }

    ColorEnum RandomColor()
    {
        switch(Random.Range(0,4))
        {
            case 0:
                return ColorEnum.RED;

            case 1:
                return ColorEnum.BLEU;

            case 2:
                return ColorEnum.GREEN;

            case 3:
                return ColorEnum.ORANGE;
        }
        return ColorEnum.WHITE;
    }

    public IEnumerator DestroyShield()
    {
        int count = sheilds.Count - 1;
        sheilds[count].GetComponent<Sheild>().ChangeShader();
        yield return new WaitForSeconds(2f);
        Destroy(sheilds[count]);
        sheilds.RemoveAt(count);
        if(count>0)
            sheilds[count - 1].GetComponent<Sheild>().lastSield = true;
    }
}
