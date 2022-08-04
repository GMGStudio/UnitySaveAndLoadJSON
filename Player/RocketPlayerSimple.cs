using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RocketPlayerSimple : MonoBehaviour
{
    [SerializeField]
    private GameObject stars;
    [SerializeField]
    private GameObject input;
    [SerializeField]
    private GameObject playerNameUI;
    [SerializeField]
    private GameObject scoreUI;
    [SerializeField]
    private GameObject saveButton;

    private PlayerSaveValueObject saveObject;


    private void Awake()
    {
        string json = SaveHelper.instance.GetJsonFromFile();
        saveObject = JsonUtility.FromJson<PlayerSaveValueObject>(json);

        stars.GetComponent<ParticleSystem>().Stop();
        if (saveObject != null)
        {
            HandlePlayerAlreadyKnown();
        }
    }

    public void OnPlayerNameInput()
    {
        saveObject = new();
        saveObject.PlayerName = input.GetComponent<TMP_InputField>().text;
        saveObject.score = 0;
        HandlePlayerAlreadyKnown();

    }


    private void HandlePlayerAlreadyKnown()
    {
        input.SetActive(false);
        saveButton.SetActive(false);
        playerNameUI.GetComponent<TextMeshProUGUI>().SetText(saveObject.PlayerName);
        scoreUI.GetComponent<TextMeshProUGUI>().SetText(saveObject.score.ToString());
        stars.GetComponent<ParticleSystem>().Play();

        StartCoroutine(CountScore());
    }

  
    IEnumerator CountScore()
    {
        do
        {
            yield return new WaitForSeconds(.3f);
            saveObject.score += 1;
            scoreUI.GetComponent<TextMeshProUGUI>().SetText(saveObject.score.ToString());
        } while (true);
    }


    private void OnApplicationQuit()
    {

        string json = JsonUtility.ToJson(saveObject);
        SaveHelper.instance.SaveJsonToFile(json);
    }
}
