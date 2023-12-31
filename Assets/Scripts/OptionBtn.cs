using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionBtn : MonoBehaviour
{
    public GameMode GmMode;

    public GameObject optionsPanel; // 인스펙터에서 할당할 옵션 패널
    public Button setOptionBtn; // 인스펙터에서 할당할 설정 버튼
    public Button noSaveReturnBtn; // 저장 안 하고 옵션 닫기
    public Button saveReturnBtn2; // 저장하고 옵션 닫기
    public GameObject[] buttons; // 숨길 버튼들
    
    public Slider MainSound;
    public Slider EffectSound;
    public Slider MouseSensitivity;
    public Button _1stView;
    public Button _3rdView;

    public AudioSource ButtonSound;

    private int PersonValue;

    private bool isOpen = false;
    void Start()
    {
        setOptionBtn.onClick.AddListener(OptionOpen); // 리스너 추가
        noSaveReturnBtn.onClick.AddListener(OptionClose); // 리스너 추가
        saveReturnBtn2.onClick.AddListener(SaveOptionClose); // 리스너 추가
        _1stView.onClick.AddListener(
            () =>
            {
                ButtonSound.Play();
                PersonValue = 1;
                _1stView.interactable = false;
                _3rdView.interactable = true;
            });
        
        _3rdView.onClick.AddListener(
            () =>
            {
                ButtonSound.Play();
                PersonValue = 3;
                _1stView.interactable = true;
                _3rdView.interactable = false;
            });
    }

    void OptionOpen()
    {
        isOpen = true;
        ButtonSound.Play();

        MainSound.value = LocalPlayerManager.instance.MainSound;
        EffectSound.value = LocalPlayerManager.instance.EffectSound;
        MouseSensitivity.value = LocalPlayerManager.instance.MouseSensitivity;
        PersonValue = LocalPlayerManager.instance.PlayerPerson;

        if (LocalPlayerManager.instance.PlayerPerson == 1)
        {
            _1stView.interactable = false;
            _3rdView.interactable = true;
        }
        else
        {
            _1stView.interactable = true;
            _3rdView.interactable = false;
        }
        foreach (GameObject button in buttons)
        {
            button.SetActive(false);
        }
        // 기존의 버튼 숨기기
        
        optionsPanel.SetActive(true); // 옵션 패널 활성화, Canvas Group의 Alpha 값을 1로 설정하여 불투명하게 만들 수 있습니다.
        CanvasGroup canvasGroup = optionsPanel.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f; // 패널을 완전히 불투명하게 만듭니다.
            canvasGroup.blocksRaycasts = true; // 패널이 레이캐스트를 막도록 합니다.
        }
    }

    // 옵션 패널을 비활성화하는 메서드
    public void OptionClose()
    {
        ButtonSound.Play();
        MainSound.value = LocalPlayerManager.instance.MainSound;
        EffectSound.value = LocalPlayerManager.instance.EffectSound;
        MouseSensitivity.value = LocalPlayerManager.instance.MouseSensitivity;
        PersonValue = LocalPlayerManager.instance.PlayerPerson;

        foreach (GameObject button in buttons)
        {
            button.SetActive(!button.activeSelf);
        }
        // 기존의 버튼 다시 활성화
        
        optionsPanel.SetActive(false); // 옵션 패널 비활성화 Canvas Group의 Alpha 값을 다시 0으로 설정합니다.
        CanvasGroup canvasGroup = optionsPanel.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f; // 패널을 투명하게 만듭니다.
            canvasGroup.blocksRaycasts = false; // 패널이 레이캐스트를 막지 않도록 합니다.
        }
        Camera.main.GetComponent<CameraMovement>().RevertMouseInput();

        isOpen = false;
    }

    // 옵션들을 저장하고 비활성화 하는 메서드
    public void SaveOptionClose()
    {
        ButtonSound.Play();
        LocalPlayerManager.instance.MainSound = MainSound.value;
        LocalPlayerManager.instance.EffectSound = EffectSound.value;
        LocalPlayerManager.instance.MouseSensitivity = MouseSensitivity.value;
        LocalPlayerManager.instance.PlayerPerson = PersonValue;
        GmMode.GetComponent<AudioSource>().volume = MainSound.value * 0.5f;
        AudioSource EffectSounds = Camera.main.GetComponent<AudioSource>();

        foreach (GameObject button in buttons)
        {
            button.SetActive(!button.activeSelf);
        }
        // 기존의 버튼 다시 활성화

        optionsPanel.SetActive(false); // 옵션 패널 비활성화 Canvas Group의 Alpha 값을 다시 0으로 설정합니다.
        CanvasGroup canvasGroup = optionsPanel.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f; // 패널을 투명하게 만듭니다.
            canvasGroup.blocksRaycasts = false; // 패널이 레이캐스트를 막지 않도록 합니다.
        }
        Camera.main.GetComponent<CameraMovement>().RevertMouseInput();

        isOpen = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isOpen)
            {
                OptionOpen();
                Camera.main.GetComponent<CameraMovement>().RevertMouseInput();
            }
        }
    }
}
