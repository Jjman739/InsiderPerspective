using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{

    public static ScoreTracker control;
    public int treasureValue = 0;

    private void Awake()
    {
        if(control == null)
        {
            control = this;
            DontDestroyOnLoad(gameObject);

        }
        else if(control != this){
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
