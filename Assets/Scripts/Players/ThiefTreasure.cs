using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThiefTreasure : MonoBehaviour
{
    public int treasureCount = 0;

    [SerializeField] private int goal = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (treasureCount >= goal)
        {
            SceneManager.LoadScene("WinScene");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Treasure"))
        {
            ScoreTracker.control.treasureValue++;
            treasureCount++;
            Destroy(other.gameObject);
            
        }
    }
}
