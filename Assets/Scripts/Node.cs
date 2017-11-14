using UnityEngine;
using UnityEngine.EventSystems;
public class Node : MonoBehaviour {


    public Color hoverColor;
    public Color notenoughmoney;
    public Vector3 positionOffSet;

    public int arrayPosition;

    [Header("Optional")]
    public GameObject turret;

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition ()
    {
        return transform.position + positionOffSet;
    }

    private void OnMouseDown()
    {

        if (EventSystem.current.IsPointerOverGameObject())
            return;



        if (!buildManager.CanBuild)
            return;

        buildManager.BuildTurretOn(this);
        }

    private void OnMouseEnter()
    {

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;
        if (buildManager.HasMoney)
            rend.material.color = hoverColor;
        else
            rend.material.color = notenoughmoney;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
