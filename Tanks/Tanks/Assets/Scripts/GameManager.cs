using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> scoreTexts = new List<TextMeshProUGUI>();

    [SerializeField]
    private List<Camera> cameras = new List<Camera>();

    [HideInInspector]
    public int greenScore = 0, blueScore = 0, redScore = 0, yellowScore = 0;

    [SerializeField]
    private GameObject postProcessing;

    private Light lamp;
    //private float deltaTime;

    public TextMeshProUGUI fpsText;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
        lamp = GameObject.Find("Illumination").GetComponent<Light>();
        for (int i = 0; i < 8; i++)
        {
            cameras[i].enabled = false;
        }
        cameras[0].enabled = true;
        InvokeRepeating(nameof(ChangeCameras), 15.0f, 15.0f);
    }

    void Update()
    {
        scoreTexts[0].text = "Green Score : " + greenScore;
        scoreTexts[1].text = "Blue Score : " + blueScore;
        scoreTexts[2].text = "Red Score : " + redScore;
        scoreTexts[3].text = "Yellow Score : " + yellowScore;

        /*
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = Mathf.Ceil(fps).ToString();
        */
    }

    private void ChangeCameras()
    {
        for (int i = 0; i < 8; i++)
        {
            cameras[i].enabled = false;
        }
        cameras[Random.Range(0, 8)].enabled = true;
    }

    public void ChangeSettings()
    {
        if (postProcessing.activeInHierarchy)
        {
            postProcessing.SetActive(false);
            lamp.intensity = 0.9f;
        }
        else
        {
            postProcessing.SetActive(true);
            lamp.intensity = 0.1f;
        }
    }
}
