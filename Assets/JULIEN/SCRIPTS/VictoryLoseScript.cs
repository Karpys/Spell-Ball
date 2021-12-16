using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryLoseScript : MonoBehaviour
{

    [SerializeField] private GameInfo _gameInfo;
    [SerializeField] private TextMeshProUGUI _text;
    
    [SerializeField] private Animator _animatorRedRat;
    [SerializeField] private Animator _animatorBlueRat;
    [SerializeField] private Animator _animatorGreenRat;
    [SerializeField] private Animator _animatorYellowRat;

    [SerializeField] private GameObject bossHeads;

    [SerializeField] private int NumberOfAnims;

    [SerializeField] private AK.Wwise.Event StopMusic;

    
    void Start()
    {
        StopMusic.Post(gameObject);
        ControllerHaptics.instance.StopAllControllers(0);
        Time.timeScale = 1f;
        if (_gameInfo.victory)
        {
            _animatorRedRat.SetInteger("Dance", Random.Range(0, NumberOfAnims));
            _animatorBlueRat.SetInteger("Dance", Random.Range(0, NumberOfAnims));
            _animatorGreenRat.SetInteger("Dance", Random.Range(0, NumberOfAnims));
            _animatorYellowRat.SetInteger("Dance", Random.Range(0, NumberOfAnims));

            _animatorRedRat.transform.parent.position = new Vector3(_animatorRedRat.transform.parent.position.x, 1f, _animatorRedRat.transform.parent.position.z);
            _animatorBlueRat.transform.parent.position = new Vector3(_animatorBlueRat.transform.parent.position.x, 1f, _animatorBlueRat.transform.parent.position.z);
            _animatorGreenRat.transform.parent.position = new Vector3(_animatorGreenRat.transform.parent.position.x, 1f, _animatorGreenRat.transform.parent.position.z);
            _animatorYellowRat.transform.parent.position = new Vector3(_animatorYellowRat.transform.parent.position.x, 1f, _animatorYellowRat.transform.parent.position.z);

            bossHeads.SetActive(false);
            
            _text.text = "\nVICTORY";
        }
        else
        {
            _text.text = "\nGAME OVER";
        }
        Invoke("GoBackToMenu", 10f);
    }

    public void GoBackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
