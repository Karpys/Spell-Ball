using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeFrame : MonoBehaviour
{
    // Start is called before the first frame update
    private static FreezeFrame inst;
    public static FreezeFrame Freezer { get => inst; }

    private bool _OnFreeze = false;

    public bool GetFreeze
    {
        get { return _OnFreeze; }
    }

    void Awake()
    {
        if (Freezer != null && Freezer != this)
            Destroy(gameObject);

        inst = this;
    }

    public void TryFreeze(float duration)
    {
        if (!_OnFreeze)
        {
            StartCoroutine(FreezeScreen(duration));
        }
    }

    public void TryFreeze(float duration, Ball Ball, GameObject VisualCharacter)
    {
        if (!_OnFreeze)
        {
            StartCoroutine(FreezeScreen(duration,Ball,VisualCharacter));
        }
    }

    IEnumerator FreezeScreen(float duration)
    {
        _OnFreeze = true;
        float TimeScale = Time.timeScale;
        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = TimeScale;
        _OnFreeze = false;
    }

    IEnumerator FreezeScreen(float duration,Ball Ball,GameObject VisualCharacter)
    {
        _OnFreeze = true;
        float TimeScale = Time.timeScale;
        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(duration);
        Ball.SetDirection(new Vector3(0, VisualCharacter.transform.eulerAngles.y, 0));
        Time.timeScale = TimeScale;
        _OnFreeze = false;
    }
}
