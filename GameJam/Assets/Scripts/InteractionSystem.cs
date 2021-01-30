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

    [SerializeField] private Animator RightDoubleDoor = null;
    [SerializeField] private Animator LeftDoubleDoor = null;
    bool DoubleDoorisOpen = false;

    int PotionCount = 0;

    AudioSource audioData;
    
    // Start is called before the first frame update
    void Start()
    {
        Items.Add("Key");
        Items.Add("Potion");
        Interactables.Add("FireplaceSwitch");
        Interactables.Add("Note1");
        Interactables.Add("Note2");
        Interactables.Add("Note3");
        Interactables.Add("Note4");
        Interactables.Add("Note5");
        Interactables.Add("DoubleDoorTrigger1");
        Interactables.Add("DoubleDoorTrigger2");
        Interactables.Add("DoubleDoorTrigger3");
        Interactables.Add("Brew");
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
            if (hit.transform.tag == "Key")
            {
                if (!showingPopup && !firePlace)
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x, hit.transform.position.y + 0.25f, hit.transform.position.z - 0.4f), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Pick up Key");
                }

                if (!PlayerScript.CheckInventory("Key") && Input.GetMouseButtonDown(0) && !firePlace)
                {
                    Items.Remove("Key");
                    PlayerScript.AddToInventory("Key");
                    Destroy(hit.transform.gameObject);
                    Destroy(tempPopup);
                    showingPopup = false;
                    Debug.Log("Key picked up");
                }
            }

            else if (hit.transform.tag == "FireplaceSwitch")
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

                if (Interactables.Contains("FireplaceSwitch") && Input.GetMouseButtonDown(0))
                {
                    if (firePlace) //if fireplace is on
                    {
                        tempPopup.GetComponentInChildren<TMP_Text>().SetText("Turn on Fireplace"); //Update text
                        Debug.Log("Turned off Fireplace");
                        firePlace = false;
                        fireplaceEmitter.Stop();

                        Debug.Log("Sound Light");
                    }

                    else
                    {
                        tempPopup.GetComponentInChildren<TMP_Text>().SetText("Turn off Fireplace"); //Update text
                        Debug.Log("Turned on Fireplace");

                        firePlace = true;
                        fireplaceEmitter.Play();
                    }

                }
            }

            else if (hit.transform.tag == "Note1")
            {
                if (!showingPopup)
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x, hit.transform.position.y + 1.0f, hit.transform.position.z - 0.4f), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Pick up note");
                }

                if (Interactables.Contains("Note1") && Input.GetMouseButtonDown(0) && !showingNote)
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

            else if (hit.transform.tag == "Hint1")
            {
                if (!showingPopup)
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Find the key to open the gate");
                }

                if (Interactables.Contains("Hint1") && Input.GetMouseButtonDown(0) && !showingNote)
                {
                    showingNote = true;
                }
                else if (showingNote && Input.GetMouseButtonDown(0))
                {

                    showingNote = false;
                }
            }

            else if (hit.transform.tag == "DoubleDoorTrigger1")
            {
                if (!showingPopup && !DoubleDoorisOpen && PlayerScript.CheckInventory("Key"))
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x - 0.5f, hit.transform.position.y + 0.5f, hit.transform.position.z + 1.0f), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Open Door");
                }

                else if (!showingPopup && !DoubleDoorisOpen && !PlayerScript.CheckInventory("Key"))
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x - 0.5f, hit.transform.position.y + 0.5f, hit.transform.position.z + 1.0f), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Door is locked");
                }


                else if (Interactables.Contains("DoubleDoorTrigger1") && Input.GetMouseButtonDown(0) && !DoubleDoorisOpen && PlayerScript.CheckInventory("Key"))
                {
                    DoubleDoorisOpen = true;
                    RightDoubleDoor.Play("RightDoubleDoorOpen", 0, 0.0f);
                    LeftDoubleDoor.Play("LeftDoubleDoorOpen", 0, 0.0f);
                    Destroy(tempPopup);
                    showingPopup = false;
                }
            }

            else if (hit.transform.tag == "Potion")
            {
                if (!showingPopup)
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Pickup Potion");
                }

                else if (Items.Contains("Potion") && Input.GetMouseButtonDown(0) && PotionCount < 4)
                {
                    Destroy(hit.transform.parent.gameObject);
                    PotionCount += 1;
                    PlayerScript.AddToInventory("Potion" + PotionCount.ToString());
                    Debug.Log("Potion Count " + PotionCount);
                }
            }

            else if (hit.transform.tag == "Brew")
            {
                if (!showingPopup)
                {
                    showingPopup = true;
                    tempPopup = Instantiate(InteractionPopUp, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z), Quaternion.identity);
                    tempPopup.GetComponentInChildren<TMP_Text>().SetText("Brew Potions " + PotionCount + " /3");
                }

                else if (PlayerScript.CheckInventory("Potion1") &&
                   PlayerScript.CheckInventory("Potion2") &&
                   PlayerScript.CheckInventory("Potion3") &&
                   Input.GetMouseButtonDown(0) && !DoubleDoorisOpen)
                {
                    DoubleDoorisOpen = true;
                    RightDoubleDoor.Play("RightDoubleDoorOpen", 0, 0.0f);
                    LeftDoubleDoor.Play("LeftDoubleDoorOpen", 0, 0.0f);
                    Debug.Log("Player has all potions and is trying to brew");
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
