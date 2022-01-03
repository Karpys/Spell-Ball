using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheildManager : MonoBehaviour
{
    public GameObject prefabShield;
    public List<GameObject> sheilds;
    public BossAction.ShieldStats Stats;
    public BossBehavior Boss;
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
            shield.SetActive(false);
            if (i + 1 == Stats.Number)
            {
                shield.GetComponent<Sheild>().lastSield = true;
                shield.SetActive(true);
            }

        }
        Boss.NextAction();
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
        Debug.Log("last");
        StartCoroutine(DestroyShield());
    }

    ColorEnum RandomColor()
    {
        switch(Random.Range(0,GameOver.instance.player.Count))
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
        Debug.Log("disolve");
        int count = sheilds.Count - 1;
        if (count - 1 > 0)
        {

            yield break;
        }
        sheilds[count].GetComponent<Sheild>().ChangeShader();
        yield return new WaitForSeconds(2f);
        //count = sheilds.Count - 1;
        if (count < 0)
        {
            Destroy(gameObject);
            yield break;
        }
        Destroy(sheilds[count]);
        sheilds.RemoveAt(count);
        if (count > 0)
        {
            sheilds[count - 1].GetComponent<Sheild>().lastSield = true;
            sheilds[count - 1].SetActive(true);
        }


    }
}
