using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorFreezeButton : Pressable
{
      public Camera source;
      // How long to actually freeze for
      public float freezeDuration = 30;
      // How long it's currently frozen
      private float currentFreezeTime = 0;

      public void Update() {
            if (currentFreezeTime > 0) {
                  currentFreezeTime -= Time.deltaTime;
                  if (currentFreezeTime <= 0) {
                        source.enabled = true;
                        Debug.Log("Unfroze camera: "+source.name);
                  }
            }
      }

      override public void Press() {
            source.enabled = false;
            currentFreezeTime = freezeDuration;
            Debug.Log("Froze camera: "+source.name);
      }
}