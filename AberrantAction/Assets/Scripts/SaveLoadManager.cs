using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadManager : MonoBehaviour {

    [SerializeField]
    private Button[] gameLevelButtons;

	void Start () {
        SaveLoad.Load();

        if(SaveLoad.levelsUnlocked == 0)
        {
            SaveLoad.levelsUnlocked = 1;
            SaveLoad.Save();
            Debug.Log("New save file generated");
        }

        RefreshUnlockedLevels();    
	}

    public void RefreshUnlockedLevels()
    {
        //for (int i = 0; i < SaveLoad.levelsUnlocked; i++)
        //{
        //    gameLevelButtons[i].interactable = true;
        //}

        for(int i = 0; i < gameLevelButtons.Length; i++)
        {
            if(i < SaveLoad.levelsUnlocked)
            {
                gameLevelButtons[i].interactable = true;
            }
            else
            {
                gameLevelButtons[i].interactable = false;
            }
        }
    }
}
