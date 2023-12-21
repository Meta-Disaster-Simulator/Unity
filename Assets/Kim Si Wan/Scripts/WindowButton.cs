using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowButton : MonoBehaviour
{
    public GameObject Btn;
    public Button use;
    public Button Cancel;
    public GameObject player;
    public GameObject Tape;
    public GameObject Window;

    private bool isTape = false;

    void Update()
    {
        for (int i = 0; i < 20; i++)
        {
            if (player.GetComponent<PlayerStatus>().belongings[i] != null)
            {
                if (player.GetComponent<PlayerStatus>().belongings[i].itemName == "Tape")
                {
                    isTape = true;
                    break;
                }
            }
        }

        if (isTape)
            use.GetComponent<Button>().interactable = true;
        else
            use.GetComponent<Button>().interactable = false;
    }
    public void OnClickUse()
    {
        SWAudio.instance.playSound("Button");

        ++player.GetComponent<PlayerStatus>().countTape;

        if (player.GetComponent<PlayerStatus>().countTape == 4)
            player.GetComponent<PlayerStatus>().usedTape = true;

        Tape.SetActive(true);
        Window.GetComponent<WindowHover>().isTape = true;

        CameraMovement cmm = Camera.main.GetComponent<CameraMovement>();
        cmm.isESC = false;
        cmm.CameraArm.transform.parent.GetComponent<PlayerMovement>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;

        Btn.SetActive(false);
    }
    public void OnClickCancel()
    {
        SWAudio.instance.playSound("Button");

        CameraMovement cmm = Camera.main.GetComponent<CameraMovement>();
        cmm.isESC = false;
        cmm.CameraArm.transform.parent.GetComponent<PlayerMovement>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Window.GetComponent<WindowHover>().isActable = true;
        StartCoroutine(Wait());
        Btn.SetActive(false);
    }

    IEnumerator Wait()
    {
        yield return null;
    }
}
