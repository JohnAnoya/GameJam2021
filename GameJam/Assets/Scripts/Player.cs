using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera camera; 
    CharacterController characterController; 
    Vector3 playerVel;
    float WalkSpeed = 8.0f;
    float gravity = -9.81f;

    RaycastHit hit; 

    // Start is called before the first frame update
    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float XAxis = Input.GetAxis("Horizontal");
        float ZAxis = Input.GetAxis("Vertical");

        Vector3 move = transform.right * XAxis + transform.forward * ZAxis;
        characterController.Move(move * Time.deltaTime * WalkSpeed);


        Ray ray = camera.ScreenPointToRay(Input.mousePosition); 

        if(Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.transform.name);
        }

        //playerVel.y += gravity * Time.deltaTime;
        //characterController.Move(playerVel * Time.deltaTime);
    }
}
