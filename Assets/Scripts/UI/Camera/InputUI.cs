using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputUI : MonoBehaviour
{

    private List<string> controls;
    [SerializeField] private TextMeshProUGUI forward;
    [SerializeField] private TextMeshProUGUI back;
    [SerializeField] private TextMeshProUGUI left;
    [SerializeField] private TextMeshProUGUI right;
    [SerializeField] private TextMeshProUGUI jump;
    [SerializeField] private TextMeshProUGUI shrink;

    private string allInputs;

    // Update is called once per frame
    void Update()
    {
        controls = GameObject.Find("Thief").GetComponent<ThiefManager>().ReturnControls();

        for (int i = 0; i<controls.Count; i++ ) {
            if (controls[i].Equals("ThiefMoveUp")) {
                allInputs += "W ";
            }else if (controls[i].Equals("ThiefMoveDown"))
            {
                allInputs += "S ";
            }else if (controls[i].Equals("ThiefMoveLeft"))
            {
                allInputs += "A ";
            }else if (controls[i].Equals("ThiefMoveRight"))
            {
                allInputs += "D ";
            }else if (controls[i].Equals("Jump"))
            {
                allInputs += "Space ";
            }
            else if (controls[i].Equals("Shrink"))
            {
                allInputs += "Q ";
            }
        }

        string[] updatedInputs = allInputs.Split(' ');

        allInputs = "";

        forward.SetText(updatedInputs[0]);
        back.SetText(updatedInputs[1]);
        left.SetText(updatedInputs[2]);
        right.SetText(updatedInputs[3]);
        jump.SetText(updatedInputs[4]);
        shrink.SetText(updatedInputs[5]);
    }
}
