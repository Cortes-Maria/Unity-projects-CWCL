using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    float lastTime;

    // Update is called once per frame
    void Update()
    {
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if((Time.time - lastTime) > 2f)
            {
                Instantiate(dogPrefab, new Vector3(14, 0.9f, -1), dogPrefab.transform.rotation);
                lastTime = Time.time;
            }
        }
    }
}
