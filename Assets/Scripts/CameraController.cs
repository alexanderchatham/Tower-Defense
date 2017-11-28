using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float panSpeed = 30f;
    public float panBorderThickness = 10f;

    public float scrollSpeed = 5;
    public float minY = 10f;
    public float maxY = 80f;

	// Update is called once per frame
	void Update () {

        if (GameManager.gameIsOver)
        {
            this.enabled = false;
            return;
        }

        if (Input.GetKeyDown("w")|| Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            if(transform.position.z<95f)
                transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKeyDown("s") || Input.mousePosition.y <= panBorderThickness)
        {
            if(transform.position.z>62f)
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }


        
        if (Input.GetKeyDown("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            if (transform.position.x < 3f)
                transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKeyDown("a") || Input.mousePosition.x <= panBorderThickness)
        {
            if (transform.position.x > -3f)
                transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        


        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;

        pos.y -= scroll * 100 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
    }
}
