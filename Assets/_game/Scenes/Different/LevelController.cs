using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : Singleton<LevelController>
{
    public List<GameObject> levelList;

    public int LevelIndex;

    public void NextLevel()
    {
        if(LevelIndex < levelList.Count - 1)
        {
            Debug.Log("Change Next Level");
            foreach (var o in levelList)
            {
                o.SetActive(false);
            }
            LevelIndex += 1;
            levelList[LevelIndex].SetActive(true);
        }
        else
        {
            Debug.Log("Already Last Level");
        }
    }

    public void LastLevel()
{
    // Ensure LevelIndex is greater than 0 before trying to move to the previous level
    if (LevelIndex > 0)
    {
        Debug.Log("Change Last Level");
        foreach (var o in levelList)
        {
            o.SetActive(false);
        }
        LevelIndex -= 1;
        levelList[LevelIndex].SetActive(true);
    }
    else
    {
        Debug.Log("Already First Level");
    }
}


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
