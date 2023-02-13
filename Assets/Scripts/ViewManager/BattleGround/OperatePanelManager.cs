using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSEvent;

public class OperatePanelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /*
     * 结束回合
     * 通知进入比牌回合
     * 同时将手牌移除
    */
    public void EndRound() {
        EventListener.Instance.PostEvent(EventEnum.EVENT_ENTER_COMPAIRE_CARD_ROUND);
        EventListener.Instance.PostEvent(EventEnum.EVENT_CLEAR_CARDS);
    }
}
