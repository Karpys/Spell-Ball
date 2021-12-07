using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Charactere Selection/Data Container/Char Info", fileName = "CharInfo")]
public class SO_CharInfo : ScriptableObject
{

    public Dictionary<int, GameObject> playerPrefabForId = new Dictionary<int, GameObject>();
    public Dictionary<int, int> playerControllerToId = new Dictionary<int, int>();

}
