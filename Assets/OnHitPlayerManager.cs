using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitPlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static OnHitPlayerManager inst;
    public static OnHitPlayerManager HitPlayer { get => inst; }

    public GameObject BlueHit;
    public GameObject GreenHit;
    public GameObject YellowHit;
    public GameObject RedHit;

    void Awake()
    {
        if (HitPlayer != null && HitPlayer != this)
            Destroy(gameObject);

        inst = this;
    }

    public void OnHit(ColorEnum Color,Vector3 Position)
    {
        switch (Color)
        {
            case ColorEnum.BLEU:
                Instantiate(BlueHit, Position, Quaternion.identity);
                return;
            case ColorEnum.GREEN:
                Instantiate(GreenHit, Position, Quaternion.identity);
                return;
            case ColorEnum.ORANGE:
                Instantiate(YellowHit, Position, Quaternion.identity);
                return;
            case ColorEnum.RED:
                Instantiate(RedHit, Position, Quaternion.identity);
                return;
            default:
                return;

        }
    }
}
