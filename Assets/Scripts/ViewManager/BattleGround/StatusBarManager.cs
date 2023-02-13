using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FSEvent;

public class StatusBarManager : MonoBehaviour {
    public Text roundText;

	// Use this for initialization
	void Start () {
        InitEvents();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /*
     * 初始化监听
    */
    private void InitEvents() {
        EventListener.Instance.RegisterEvent(EventEnum.EVENT_UPDATE_ROUND_TEXT, UpdateRoundText);
    }

    /*
     * 更新回合文本
    */
    private void UpdateRoundText(Dictionary<string, object> info) {
        // 从提供的数据中获取要更新的值
        if (roundText != null) {
            roundText.text = (string)info["remind"];
            Invoke("ResetRoundText", 2.0f);
        }
    }

    /*
     * 重置提醒
    */
    private void ResetRoundText() {
        roundText.text = "";
    }
}
