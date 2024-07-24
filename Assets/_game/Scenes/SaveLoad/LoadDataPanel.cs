using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadDataPanel : MonoBehaviour
{
    public GameObject null1;
    public GameObject null2;
    public GameObject null3;
    public GameObject have1;
    public GameObject have2;
    public GameObject have3;
    public GameObject esccanvas;
    public playerControl playerControl;
    private SaveData saveData;
    private CameraFollow cameraFollow;
    public Text playerNameText1;
    public Text saveTimeText1;
    public Text playerNameText2;
    public Text saveTimeText2;
    public Text playerNameText3;
    public Text saveTimeText3;

    void Awake()
    {
        saveData = GetComponent<SaveData>();
        if (saveData == null)
        {
            Debug.LogError("SaveData component is not found on the same GameObject!");
        }

        cameraFollow = Camera.main.GetComponent<CameraFollow>();
        if (cameraFollow == null)
        {
            Debug.LogError("CameraFollow component is not found on the main camera!");
        }
    }

    void OnEnable()
    {
        if (saveData == null)
        {
            Debug.LogError("SaveData component is not assigned!");
            return;
        }

        Button data1Button = GetButton("SaveDataUI", "Data1");
        Button data2Button = GetButton("SaveDataUI", "Data2");
        Button data3Button = GetButton("SaveDataUI", "Data3");

        if (data1Button == null || data2Button == null || data3Button == null)
        {
            Debug.LogError("One or more buttons not found!");
            return;
        }

        data1Button.onClick.RemoveAllListeners();
        data2Button.onClick.RemoveAllListeners();
        data3Button.onClick.RemoveAllListeners();

        data1Button.onClick.AddListener(delegate { LoadDataAndCloseCanvas(1); });
        Debug.Log("Found button in Data1 and added listener");
        data2Button.onClick.AddListener(delegate { LoadDataAndCloseCanvas(2); });
        Debug.Log("Found button in Data2 and added listener");
        data3Button.onClick.AddListener(delegate { LoadDataAndCloseCanvas(3); });
        Debug.Log("Found button in Data3 and added listener");

        UpdateLoadPanelUI();
    }

    private void LoadDataAndCloseCanvas(int slot)
    {
        switch (slot)
        {
            case 1:
                saveData.LoadData1();
                break;
            case 2:
                saveData.LoadData2();
                break;
            case 3:
                saveData.LoadData3();
                break;
            default:
                Debug.LogError("Invalid slot number");
                return;
        }

        Time.timeScale = 1f;
        playerControl.SetMovement(true);

        if (esccanvas != null)
        {
            esccanvas.SetActive(false);
        }

        // Ensure the camera follows the player
        if (cameraFollow != null)
        {
            Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            cameraFollow.target = playerTransform;
        }
    }

    Button GetButton(string parentTag, string childName)
    {
        GameObject parentObject = GameObject.FindGameObjectWithTag(parentTag);
        if (parentObject == null)
        {
            Debug.LogError("Parent object with tag " + parentTag + " not found!");
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
            Debug.LogError("Button in " + childName + "/Save not found!");
            return null;
        }
    }

    void UpdateLoadPanelUI()
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