using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPC : MonoBehaviour
{
    [Header("NPC 資料")]
    public NPCdata data;
    [Header("image")]
    public GameObject dialog;
    [Header("對話內容")]
    public Text textConten;
    [Header("對話者名稱")]
    public Text textName;
    [Header("對話間隔")]
    public float interval = 0.2f;

    /// <summary>
    /// 是否進入感應區
    /// </summary>
    public bool playerInArea;

    public enum NPCState
    {
        Firstdialog,Missioning,Finish
    }

    public  NPCState state = NPCState.Firstdialog;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "腳色1")
        {
            playerInArea = true;
            StartCoroutine (Dialog());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "腳色1")
        {
            playerInArea = false;
            StopDialog();
        }
    }

    /// <summary>
    /// 停止對話
    /// </summary>
    private void StopDialog()
    {
        dialog.SetActive(false);
        StopAllCoroutines();
    }

    /// <summary>
    /// 開始對話
    /// </summary>
   
    private IEnumerator Dialog()
    {
        dialog.SetActive(true);
       
        textConten.text = "";

        textName.text = name;

        //要說的對話
        string dialogString = data.dialougA;

        switch (state)
        {
            case NPCState.Firstdialog:
                dialogString = data.dialougA;
                break;
            case NPCState.Missioning:
                dialogString = data.dialougB;
                break;
            case NPCState.Finish:
                dialogString = data.dialougC;
                break;

        }

        //字串長度 dialougA.Length
        for (int i = 0; i < dialogString.Length; i++)
        {
            //print(data.dialougA[i]);
            textConten.text += dialogString[i] + "";
            yield return new WaitForSeconds(interval);
        }
    }
}
