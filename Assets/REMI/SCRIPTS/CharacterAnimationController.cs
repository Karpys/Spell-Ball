using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    // Start is called before the first frame update
    public float CharacterAngle;
    public Vector3 Direction;
    
    public float AngleInput;
    public Vector3 Coordone;

    public GameObject CharacterVisual;
    public CharacterMovement Movement;
    public Animator Anim;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CharacterAngle = CharacterVisual.transform.rotation.eulerAngles.y;
        Direction = DegreeToVector2(CharacterAngle);

        AngleInput = Mathf.Atan2(Movement.MovementDir.x, Movement.MovementDir.y) * Mathf.Rad2Deg;
        Coordone = DegreeToVector2(AngleInput - CharacterAngle);

        Anim.SetFloat("DirectionUpDown", Coordone.x);
        Anim.SetFloat("DirectionRightLeft", Coordone.z);
    }

    public static Vector3 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }

    public static Vector3 RadianToVector2(float radian)
    {
        return new Vector3(Mathf.Cos(radian), 0, Mathf.Sin(radian));
    }

}
