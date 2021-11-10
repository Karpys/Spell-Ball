using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtilities
{
    public static class GameUtilities
    {
        // Start is called before the first frame update

        //Return le Joueur le plus proche / Si la Liste est vide return null//
        public static GameObject GetClosestGameObject(Vector3 Point,List<GameObject> ListGameObjects)
        {
            if (ListGameObjects.Count == 0)
                return null;

            GameObject ClosestObject = ListGameObjects[0];
            foreach (GameObject Object in ListGameObjects)
            {
                if (Vector3.Distance(Point, Object.transform.position) 
                    < Vector3.Distance(Point, ClosestObject.transform.position))
                {
                    ClosestObject = Object;
                }
            }

            return ClosestObject;
        }
        //Transforme une List en une List de GameObject//
        public static List<GameObject> ListToListGameObjects<T>(List<T> ListObject) where T : Component
        {
            List<GameObject> returnList = new List<GameObject>();

            foreach (T Objects in ListObject)
            {
                returnList.Add(Objects.gameObject);
            }

            return returnList;
        }
    }
}
