using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [Header("Vehicle Stats")]//is a header for the attributes of this class (script)
    //it shows in the inspector

    [SerializeField] int example1; //is private but can be change in the inspector
    static int example2; //it can only be one instance of this variable
    // readonly, const : other examples
    public GameObject player;
    private Vector3 offset = new Vector3(0, 5, -11);

    // Start is called before the first frame update
    void Start()
    {
        
    }
    //unity event functions: unity calls them automatically
    // fixedUpdate(), awake() etc..
    // Update is called once per frame
    void FixedUpdate()//usually use for physics (runs according to the physics calculations)
    {
        transform.position = player.transform.position + offset;
    }

}
