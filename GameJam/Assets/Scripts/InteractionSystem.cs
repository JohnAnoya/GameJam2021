using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InteractionSystem : MonoBehaviour
{
    List<string> Items = new List<string>();
    List<string> Interactables = new List<string>();

    public GameObject InteractionPopUp;
    GameObject tempPopup;
    bool showingPopup = false;

    bool firePlace = true;
    public ParticleSystem fireplaceEmitter;

    [SerializeField]
    Image note1;
    bool showingNote = false;
    //[SerializeField]
    //Image hintBook;
    
    // Start is called before the first frame update
    void Start()
    {
        Items.Add("Key");
        Interactables.Add("FireplaceSwitch");
        Interactables.Add("Note_1");
        //Interactables.Add("Book_Open");
    }

    // Update is called once per frame
    void Update()
    {
        CheckForInteractions(); 
    }

    void CheckForInteractions()
    {
        var PlayerScript = gameObject.GetComponent<Player>(); 

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1.0f))
        {
            Debug.DrawRay(Camera.main.transform.position, ray.direction, Color.green);
            if (hit.transform.name == "Key")
            {
                if (!showingPopup && !firePlace)
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x, hit.transform.position.y + 0.25f, hit.transform.position.z - 0.4f), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Pick up Key");
                }

                if (!PlayerScript.CheckInventory(hit.transform.name) && Input.GetMouseButtonDown(0) && !firePlace)
                {
                    Items.Remove(hit.transform.name);
                    PlayerScript.AddToInventory(hit.transform.name);
                    Destroy(hit.transform.gameObject);
                    Destroy(tempPopup);
                    showingPopup = false;
                    Debug.Log("Key picked up");
                }
            }

            else if (hit.transform.name == "FireplaceSwitch")
            {
                if (!showingPopup)
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x + 0.5f, hit.transform.position.y + 0.9f, hit.transform.position.z), Quaternion.identity);

                    if (firePlace)
                    {
                        tempPopup.GetComponentInChildren<TMP_Text>().SetText("Turn off Fireplace");
                    }

                    else
                    {
                        tempPopup.GetComponentInChildren<TMP_Text>().SetText("Turn on Fireplace");
                    }
                }

                if (Interactables.Contains(hit.transform.name) && Input.GetMouseButtonDown(0))
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

            else if (hit.transform.name == "Note_1")
            {
                if (!showingPopup)
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x, hit.transform.position.y + 1.0f, hit.transform.position.z - 0.4f), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Pick up note");
                }

                if (Interactables.Contains(hit.transform.name) && Input.GetMouseButtonDown(0) && !showingNote)
                {
                    note1.enabled = true;
                    showingNote = true;
                }
                else if (showingNote && Input.GetMouseButtonDown(0))
                {
                    note1.enabled = false;
                    showingNote = false;
                }
            }

            else if (hit.transform.name == "Hint")
            {
                if (!showingPopup)
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Find the key to open the gate");
                }

                if (Interactables.Contains(hit.transform.name) && Input.GetMouseButtonDown(0) && !showingNote)
                {
                    
                    showingNote = true;
                }
                else if (showingNote && Input.GetMouseButtonDown(0))
                {
                   
                    showingNote = false;
                }
            }
        }

        else if (showingPopup)
        {
            showingPopup = false;
            Destroy(tempPopup);
        }
    }
}
