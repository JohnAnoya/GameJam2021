using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneName;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("ENTERED COLLISION COMMAND");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("COLLIDED");
            SceneManager.LoadScene(sceneName);
        }
    }
}
