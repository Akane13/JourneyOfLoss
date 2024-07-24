using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataPanel : MonoBehaviour
{
    public int dataType = 1;
    public GameObject null1;
    public GameObject null2;
    public GameObject null3;
    public GameObject have1;
    public GameObject have2;
    public GameObject have3;
    private SaveData saveData;
    // Start is called before the first frame update
    void Awake()
    {
        // Find the SaveData component on the same GameObject
        saveData = GetComponent<SaveData>();
        if (saveData == null)
        {
            Debug.LogError("SaveData component is not found on the same GameObject!");
        }
    }

    public void OnEnable()
    {
        if (saveData == null)
        {
            Debug.LogError("SaveData component is not assigned!");
            return;
        }

        if (dataType == 1)
        {
            // Get buttons
            Button data1Button = GetButton("SaveDataUI", "Data1");
            Button data2Button = GetButton("SaveDataUI", "Data2");
            Button data3Button = GetButton("SaveDataUI", "Data3");

            if (data1Button == null || data2Button == null || data3Button == null)
            {
                Debug.LogError("One or more buttons not found!");
                return;
            }

            //清除所有的绑定方法
            data1Button.onClick.RemoveAllListeners();
            data2Button.onClick.RemoveAllListeners();
            data3Button.onClick.RemoveAllListeners();
            
            //绑定方法
            data1Button.onClick.AddListener(delegate{ saveData.SaveData1(); });
            Debug.Log("Found button in Data1 and added listener");
            data2Button.onClick.AddListener(delegate{ saveData.SaveData2(); });
            Debug.Log("Found button in Data2 and added listener");
            data3Button.onClick.AddListener(delegate{ saveData.SaveData3(); });
            Debug.Log("Found button in Data3 and added listener"); 
            
            //获取存档数据文件
            TextAsset data1 = Resources.Load<TextAsset>("UpdateData/saveData/Data1");
            TextAsset data2 = Resources.Load<TextAsset>("UpdateData/saveData/Data2");
            TextAsset data3 = Resources.Load<TextAsset>("UpdateData/saveData/Data3");
            
            //判断是否有对应文件，显示不同的画面
            if(!data1)
            {
                null1.SetActive(true);
                have1.SetActive(false);
            }
            else{
                null1.SetActive(false);
                have1.SetActive(true);
            }

            if(!data2)
            {
                null2.SetActive(true);
                have2.SetActive(false);
            }
            else{
                null2.SetActive(false);
                have2.SetActive(true);
            }

            if(!data3)
            {
                null3.SetActive(true);
                have3.SetActive(false);
            }
            else{
                null3.SetActive(false);
                have3.SetActive(true);
            }
        }
        else{
            // Get buttons
            Button data1Button = GetButton("SaveDataUI", "Data1");
            Button data2Button = GetButton("SaveDataUI", "Data2");
            Button data3Button = GetButton("SaveDataUI", "Data3");

            if (data1Button == null || data2Button == null || data3Button == null)
            {
                Debug.LogError("One or more buttons not found!");
                return;
            }

            //清除所有的绑定方法
            data1Button.onClick.RemoveAllListeners();
            data2Button.onClick.RemoveAllListeners();
            data3Button.onClick.RemoveAllListeners();
            
            //绑定方法
            data1Button.onClick.AddListener(delegate{ saveData.LoadData1(); });
            Debug.Log("Found button in Data1 and added listener");
            data2Button.onClick.AddListener(delegate{ saveData.LoadData2(); });
            Debug.Log("Found button in Data2 and added listener");
            data3Button.onClick.AddListener(delegate{ saveData.LoadData3(); });
            Debug.Log("Found button in Data3 and added listener"); 
            
            //获取存档数据文件
            TextAsset data1 = Resources.Load<TextAsset>("UpdateData/saveData/Data1");
            TextAsset data2 = Resources.Load<TextAsset>("UpdateData/saveData/Data2");
            TextAsset data3 = Resources.Load<TextAsset>("UpdateData/saveData/Data3");
            
            //判断是否有对应文件，显示不同的画面
            if(!data1)
            {
                null1.SetActive(true);
                have1.SetActive(false);
            }
            else{
                null1.SetActive(false);
                have1.SetActive(true);
            }

            if(!data2)
            {
                null2.SetActive(true);
                have2.SetActive(false);
            }
            else{
                null2.SetActive(false);
                have2.SetActive(true);
            }

            if(!data3)
            {
                null3.SetActive(true);
                have3.SetActive(false);
            }
            else{
                null3.SetActive(false);
                have3.SetActive(true);
            }
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

    // Update is called once per frame
    void Update()
    {
        
    }

}
