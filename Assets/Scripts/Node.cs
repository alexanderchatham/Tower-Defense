using UnityEngine;
using UnityEngine.EventSystems;
public class Node : MonoBehaviour {


    public Color hoverColor;
    public Color notenoughmoney;
    public Color finalRouteColor;
    public Vector3 positionOffSet;
    
    public int i;
    public int j;
    private bool usedInFinal = false;

    [Header("Optional")]
    public GameObject turret;

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;
    GameMap gameMap;
    Wave_Spawner waveSpawner;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
        gameMap = GameMap.instance;
        waveSpawner = Wave_Spawner.instance;
    }

    private void FixedUpdate()
    {
        if (gameMap.GetFinalBool(i, j) && rend.material.color != finalRouteColor)
        {
            rend.material.color = finalRouteColor;
            usedInFinal = true;
        }
        if (!gameMap.GetFinalBool(i, j) && rend.material.color != startColor)
        {
            rend.material.color = startColor;
        }
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
        if (Wave_Spawner.enemyCount > 0 || turret)
            return;
        buildManager.BuildTurretOn(this);
        }

    private void OnMouseEnter()
    {

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;
        if (buildManager.HasMoney && !gameMap.GetNodeBool(i,j))
        {
            rend.material.color = hoverColor;
        }
        
        if (!buildManager.HasMoney)
            rend.material.color = notenoughmoney;
        
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
