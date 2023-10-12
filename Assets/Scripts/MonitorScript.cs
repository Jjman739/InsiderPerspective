using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorScript : MonoBehaviour
{
    public Camera source;
    private float freezeTime = 0;

    public void Update() {
          if (freezeTime > 0) {
                freezeTime -= Time.deltaTime;
                if (freezeTime <= 0) {
                      source.enabled = true;
                      Debug.Log("Unfroze camera: "+source.name);
                }
          }
    }

    public void Freeze() {
          source.enabled = false;
          freezeTime = 5;
          Debug.Log("Froze camera: "+source.name);
    }

}
