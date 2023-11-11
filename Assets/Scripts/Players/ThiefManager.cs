using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThiefManager : MonoBehaviour
{
    [SerializeField] private Transform cameraRoot;
    [SerializeField] private ThiefPhoto photo;
    [SerializeField] private ThiefMovementScript movement;
    [SerializeField] private ThiefTreasure treasure;

    public void TakeDamage()
    {
        Debug.Log("Hit a trap.");
        cameraRoot.rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
        ScrambleControls(ref movement.forwardButton, ref movement.backwardButton, ref movement.leftButton, ref movement.rightButton, ref movement.jumpButton, ref photo.photoButton);
    }

    public bool AttemptWin()
    {
        if (treasure.treasureCount >= treasure.goal)
        {
            Debug.Log("Win!");
            SceneManager.LoadScene("WinScene");
            return true;
        }
        return false;
    }

    private void ScrambleControls(ref string forward, ref string backward, ref string left, ref string right, ref string jump, ref string cam)
    {
        int buttonId1 = Random.Range(0,5);
        int buttonId2 = Random.Range(0,4);
        if (buttonId2 == buttonId1)
        {
            buttonId2++;
        }

        List<string> values = new List<string> { forward, backward, left, right, jump, cam };
        string temp = values[buttonId1];
        values[buttonId1] = values[buttonId2];
        values[buttonId2] = temp;

        forward = values[0];
        backward = values[1];
        left = values[2];
        right = values[3];
        jump = values[4];
        cam = values[5];
    }
}
