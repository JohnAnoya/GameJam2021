using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    List<string> Items = new List<string>();
    List<string> Interactables = new List<string>();
    List<string> Inventory = new List<string>();
    
    public Camera camera; 
    CharacterController characterController; 
    Vector3 playerVel;
    float WalkSpeed = 8.0f;
    float gravity = -9.81f;
    float thickness = 1.0f;

    public GameObject InteractionPopUp;
    GameObject tempPopup; 
    bool showingPopup = false;

    bool firePlace = true;
    public ParticleSystem fireplaceEmitter; 

    // Start is called before the first frame update
    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        Items.Add("Key");
        Interactables.Add("Fireplace");
    }

    // Update is called once per frame
    void Update()
    {
        float XAxis = Input.GetAxis("Horizontal");
        float ZAxis = Input.GetAxis("Vertical");

        Vector3 move = transform.right * XAxis + transform.forward * ZAxis;
        characterController.Move(move * Time.deltaTime * WalkSpeed);
  
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1.0f))
        {
            Debug.DrawRay(camera.transform.position, ray.direction, Color.green);
            if (hit.transform.name == "Key")
            {
                if (!showingPopup && !firePlace)
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x, hit.transform.position.y + 0.25f, hit.transform.position.z - 0.4f), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Pick up Key");
                } 

                if (!Inventory.Contains(hit.transform.name) && Input.GetMouseButtonDown(0) && !firePlace) {
                    Items.Remove(hit.transform.name);
                    Inventory.Add(hit.transform.name);  
                    Destroy(hit.transform.gameObject);
                    Debug.Log(hit.transform.name + " picked up");
                }               
            }

            else if (hit.transform.name == "Fireplace")
            {
                if (!showingPopup)
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x, hit.transform.position.y + 1.0f, hit.transform.position.z - 0.4f), Quaternion.identity);

                    if (firePlace)
                    {
                        tempPopup.GetComponentInChildren<TMP_Text>().SetText("Turn off Fireplace");
                    }

                    else
                    {
                        tempPopup.GetComponentInChildren<TMP_Text>().SetText("Turn on Fireplace");
                    }
                }

                if(Interactables.Contains(hit.transform.name) && Input.GetMouseButtonDown(0))
                {
                    if (firePlace)
                    {
                        tempPopup.GetComponentInChildren<TMP_Text>().SetText("Turn on Fireplace");
                        Debug.Log("Turned off Fireplace");
                  
                        firePlace = false;
                        fireplaceEmitter.Stop();
                    }

                    else
                    {
                        tempPopup.GetComponentInChildren<TMP_Text>().SetText("Turn off Fireplace");
                        Debug.Log("Turned on Fireplace");
                        firePlace = true;
                        fireplaceEmitter.Play();
                    }
                    
                }
            }
        }

        else if (showingPopup)
        {
            showingPopup = false;
            Destroy(tempPopup);
        }

        playerVel.y += gravity * Time.deltaTime;
        characterController.Move(playerVel * Time.deltaTime);
    }
}
