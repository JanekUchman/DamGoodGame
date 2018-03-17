using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Functions {

    public static float HitTimer = 1f;
    public static int CollisionLayer = LayerMask.NameToLayer("Obstacle");
    public static void GetInterfaces<T>(out List<T> resultList, GameObject objectToSearch) where T : class
    {
        MonoBehaviour[] list = objectToSearch.GetComponents<MonoBehaviour>();
        resultList = new List<T>();
        foreach (MonoBehaviour mb in list)
        {
            if (mb is T)
            {
                //found one
                resultList.Add((T)((System.Object)mb));
            }
        }
    }

    public static void ToggleObject(bool toggle, GameObject objectToggle)
    {
        MonoBehaviour[] list = objectToggle.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour mb in list)
        {

            mb.enabled = toggle;

        }
    }
}
