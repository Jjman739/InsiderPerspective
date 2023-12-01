using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorFreezeButton : Pressable
{
      public Camera source;
      public GameObject indicatorGauge;
      // How long to actually freeze for
      public float freezeDuration = 30;
      // How long it's currently frozen
      private float currentFreezeTime = 0;
      private Vector3 gaugeBaseScale;

      public void Start() {
            gaugeBaseScale = indicatorGauge.transform.localScale;
      }

      public void Update() {
            if (currentFreezeTime > 0) {
                  currentFreezeTime -= Time.deltaTime;
                  indicatorGauge.transform.localScale = new Vector3(currentFreezeTime / freezeDuration, gaugeBaseScale.y, gaugeBaseScale.z);
            } else {
                  indicatorGauge.transform.localScale = new Vector3(0, gaugeBaseScale.y, gaugeBaseScale.z);
            }
      }

      override public void Press() {
            currentFreezeTime = freezeDuration;
      }
}
