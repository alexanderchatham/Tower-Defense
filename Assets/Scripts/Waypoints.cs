using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class Waypoints : MonoBehaviour {

    public static Transform[] points;
    public static Waypoints instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one GameMap in scene!");
            return;
        }

        instance = this;

        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }

    public Transform getPoints(int i)
    {
        return points[i];
    }


    public void setPoints(ArrayList points_)
    {
        for (int i = 0; i < points_.Count; i++)
            try {
                points[i] = points_[i] as Transform;
            } catch (System.Exception e)
            {

            }
    }


}
