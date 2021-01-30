using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    List<string> Items = new List<string>();
    List<string> Inventory = new List<string>(); 
    
    public Camera camera; 
    CharacterController characterController; 
    Vector3 playerVel;
    float WalkSpeed = 8.0f;
    float gravity = -9.81f;

    RaycastHit hit;
    float thickness = 1.0f; 

    // Start is called before the first frame update
    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        Items.Add("Key");
    }

    // Update is called once per frame
    void Update()
    {
        float XAxis = Input.GetAxis("Horizontal");
        float ZAxis = Input.GetAxis("Vertical");

        Vector3 move = transform.right * XAxis + transform.forward * ZAxis;
        characterController.Move(move * Time.deltaTime * WalkSpeed);


        Ray ray = camera.ScreenPointToRay(Input.mousePosition); 

        if(Physics.SphereCast(ray, thickness, out hit))
        {
            if (hit.transform.name == "Key" && !Inventory.Contains(hit.transform.name) && Input.GetMouseButtonDown(0))
            {       
                Items.Remove(hit.transform.name);
                Inventory.Add(hit.transform.name);
                Debug.Log("Key picked up");

            }
            
        }

        //playerVel.y += gravity * Time.deltaTime;
        //characterController.Move(playerVel * Time.deltaTime);
    }
}
