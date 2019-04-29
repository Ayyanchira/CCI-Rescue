using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.transform.tag == "Player")
        {
            if(other.GetComponent<BatteryController>().count == other.GetComponent<BatteryController>().m_count)
            {
                if (SceneManager.GetActiveScene().name == "Level1")
                    SceneManager.LoadScene("Level2");
                else
                    Application.Quit();
            }
        }
    }
}
