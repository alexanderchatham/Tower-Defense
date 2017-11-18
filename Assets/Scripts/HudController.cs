using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudController : MonoBehaviour {

    public GameObject Camera;

    private float panSpeed = 30f;
    private float panBorderThickness = 10f;
    private float scrollSpeed = 5;
    private float minY = 10f;
    private float maxY = 80f;

    // Update is called once per frame
    void Update()
    {

        if (GameManager.gameIsOver)
        {
            this.enabled = false;
            return;
        }

        if (Input.GetKeyDown("w") || Input.mousePosition.y >= Screen.height - panBorderThickness && this.transform.position.z < 92)
        {
                transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKeyDown("s") || Input.mousePosition.y <= panBorderThickness && this.transform.position.z > 64)
        {
                transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }



    }
}
