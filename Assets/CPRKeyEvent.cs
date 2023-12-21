using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPRKeyEvent : MonoBehaviour
{
    public GameObject cpr;

    public GameObject checkstat; // ����Ȯ�� �г�
    public Transform CPRplayer;
    public Transform cprspot;
    private CPRPlayerAnimation pa;
    public bool is_statQ = false;

    public GameObject cprstartpanel;


    public bool has_AED = false; // �������� ������ �ִ���
    public GameObject AEDPanel;
    public GameObject startAEDpanel;

    public GameObject camera;
    private CPRCameraMovement cm;
    private void Start()
    {
        checkstat.SetActive(false);
        AEDPanel.SetActive(false);
        startAEDpanel.SetActive(false);
        pa = cpr.GetComponentInChildren<CPRPlayerAnimation>();
        cm = camera.GetComponent<CPRCameraMovement>(); 

    }


    private void OnTriggerStay(Collider other)
    {
        // Trigger ������ Stay ���� �� �г� Ȱ��ȭ
        if (other.CompareTag("CPRPlayer") && !is_statQ)
        {
            checkstat.SetActive(true);
            if (has_AED)
            {
                AEDPanel.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Q) && !is_statQ)
            {
                cm.isMove = false;
                pa._isCPR = true;
                is_statQ = true;
                checkstat.SetActive(false);
                cprstartpanel.SetActive(true);
                CPRplayer.position = cprspot.position;
                CPRplayer.rotation = cprspot.rotation;

                cm.isESC = true;
                cm.CameraArm.transform.parent.GetComponent<CPRPlayerMovement>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            if(Input.GetKeyDown(KeyCode.E) && !is_statQ && has_AED)
            {
                cm.isMove = false;
                pa._isCPR = true;
                is_statQ = true;
                checkstat.SetActive(false);
                startAEDpanel.SetActive(true);
                CPRplayer.position = cprspot.position;
                CPRplayer.rotation = cprspot.rotation;

                cm.isESC = true;
                cm.CameraArm.transform.parent.GetComponent<CPRPlayerMovement>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Trigger���� �������� �� �г� ��Ȱ��ȭ
        if (other.CompareTag("CPRPlayer"))
        {
            checkstat.SetActive(false);
            AEDPanel.SetActive(false);
        }
    }
}
