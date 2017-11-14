using UnityEngine;
using UnityEngine.EventSystems;

public class SellUpgrade : MonoBehaviour {

    public Color hoverColor;
    public Node parentNode;
    public GameObject selfReference;

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;
    public Turret turret;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }
    private void OnMouseDown()
    {

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        buildManager.SelectTurret(this);
        
    }

    private void OnMouseEnter()
    {

        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
            rend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }


    public void upgrade()
    {
        if (PlayerStats.Money >= (100 + 50 * turret.upgrades))
        {

            if (!turret.useLaser && !turret.useMissile)
                turret.fireRate += 10;
            turret.damageModifier += 10;
            PlayerStats.Money -= 100 + 50 * turret.upgrades;
            turret.upgrades++;
        }
    }

    public void sell()
    {
        PlayerStats.Money += 100 + 50 * turret.upgrades;
        Destroy(selfReference);

    }
}
