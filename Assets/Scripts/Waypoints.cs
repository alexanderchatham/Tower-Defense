using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class Waypoints : MonoBehaviour {

    public static Transform[] points;
    public static Waypoints instance;
    private static Transform end;
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
            end = points[i];
        }
    }

    public Transform getPoints(int i)
    {
        return points[i];
    }


    public void setPoints(ArrayList points_)
    {
        points = new Transform[points_.Count];
        for (int i = 0; i < points_.Count -1; i++)
        {
            points[i] = points_[i] as Transform;
        }
        points[points_.Count - 1] = end;
    }

}
