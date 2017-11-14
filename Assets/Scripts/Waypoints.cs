using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Waypoints : MonoBehaviour {

    public static Transform[] points;

    private void Awake()
    {
        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
    private void Start()
    {
        StartCoroutine(getPoints());
    }


    IEnumerator getPoints()
    {
        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
        return null;
    }

}
