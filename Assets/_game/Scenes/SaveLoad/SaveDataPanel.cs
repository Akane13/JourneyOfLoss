using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveDataPanel : MonoBehaviour
{
    public GameObject null1;
    public GameObject null2;
    public GameObject null3;
    public GameObject have1;
    public GameObject have2;
    public GameObject have3;
    public Text playerNameText1;
    public Text saveTimeText1;
    public Text playerNameText2;
    public Text saveTimeText2;
    public Text playerNameText3;
    public Text saveTimeText3;
    private SaveData saveLoadData;

    void Awake()
    {
        saveLoadData = GetComponent<SaveData>();
        if (saveLoadData == null)
        {
            UnityEngine.Debug.Log("SaveLoadData component is not found on the same GameObject!");
        }
    }

    void OnEnable()
    {
        if (saveLoadData == null)
        {
            UnityEngine.Debug.Log("SaveLoadData component is not assigned!");
            return;
        }

        Button data1Button = GetButton("SaveDataUI", "Data1");
        Button data2Button = GetButton("SaveDataUI", "Data2");
        Button data3Button = GetButton("SaveDataUI", "Data3");

        if (data1Button == null || data2Button == null || data3Button == null)
        {
            UnityEngine.Debug.Log("One or more buttons not found!");
            return;
        }

        data1Button.onClick.RemoveAllListeners();
        data2Button.onClick.RemoveAllListeners();
        data3Button.onClick.RemoveAllListeners();

        data1Button.onClick.AddListener(delegate { saveLoadData.SaveData1(); });
        UnityEngine.Debug.Log("Found button in Data1 and added listener");
        data2Button.onClick.AddListener(delegate { saveLoadData.SaveData2(); });
        UnityEngine.Debug.Log("Found button in Data2 and added listener");
        data3Button.onClick.AddListener(delegate { saveLoadData.SaveData3(); });
        UnityEngine.Debug.Log("Found button in Data3 and added listener");

        UpdateSavePanelUI();
    }

    Button GetButton(string parentTag, string childName)
    {
        GameObject parentObject = GameObject.FindGameObjectWithTag(parentTag);
        if (parentObject == null)
        {
            UnityEngine.Debug.Log("Parent object with tag " + parentTag + " not found!");
            return null;
        }

        Transform parentTransform = parentObject.transform.GetChild(0).GetChild(0);
        Transform childTransform = parentTransform.Find(childName + "/Save");

        if (childTransform != null)
        {
            return childTransform.GetComponent<Button>();
        }
        else
        {
            UnityEngine.Debug.Log("Button in " + childName + "/Save not found!");
            return null;
        }
    }

    void UpdateSavePanelUI()
    {
        CheckAndSetData("Assets/_game/Resources/UpdateData/saveData/Data1.txt", null1, have1, playerNameText1, saveTimeText1);
        CheckAndSetData("Assets/_game/Resources/UpdateData/saveData/Data2.txt", null2, have2, playerNameText2, saveTimeText2);
        CheckAndSetData("Assets/_game/Resources/UpdateData/saveData/Data3.txt", null3, have3, playerNameText3, saveTimeText3);
    }

    void CheckAndSetData(string path, GameObject nullObj, GameObject haveObj, Text playerNameText, Text saveTimeText)
    {
        if (File.Exists(path))
        {
            string data = File.ReadAllText(path);
            SaveDataObject.DataForPlayer dataForPlayer = JsonUtility.FromJson<SaveDataObject.DataForPlayer>(data);

            nullObj.SetActive(false);
            haveObj.SetActive(true);

            playerNameText.text = dataForPlayer.name;
            saveTimeText.text = dataForPlayer.dataSaveTime;
        }
        else
        {
            nullObj.SetActive(true);
            haveObj.SetActive(false);
        }
    }
}
