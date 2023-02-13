using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FSEvent;

public class CardPanelManager : MonoBehaviour {
    public Transform cardPanel;
    public GameObject cardPrefab;

	// Use this for initialization
	void Start () {
    
        //GameObject cardTransform = Instantiate(cardPrefab);
        //cardTransform.transform.SetParent(bg, false);


        //Image bg = cardPanel.add

        InitEvents();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDestroy() {
        RemoveEvents();
    }

    /*
     * 注册监听事件
    */
    private void InitEvents() {
        print("Card Panel 注册事件 ... ");

        EventListener.Instance.RegisterEvent(EventEnum.EVENT_PLAYER_SHUFFLING_CARD, ShufflingCard);
        EventListener.Instance.RegisterEvent(EventEnum.EVENT_CLEAR_CARDS, CleatCards);
    }

    /*
     * 移除监听事件
    */
    private void RemoveEvents() {

    }

    /*
     * 发牌
    */
    private void ShufflingCard() {
        // 如果手牌小于5，则
        if (BattleFieldManager.Instance.GetArmHandCards().Count < 5) {
            AddCard();

            Invoke("ShufflingCard", 1.0f);
        } else {
            Invoke("NoticeEnemyDeal", 2.0f);
        }
    }

    /*
     * 通知可以进入敌人出牌的回合了
    */
    private void NoticeEnemyDeal() {
        EventListener.Instance.PostEvent(EventEnum.EVENT_ENTER_ENEMY_DEAL_ROUND);
    }
     
    /*
     *  添加卡牌
     */
    public void AddCard() {
        // 创建一个卡牌
        GameObject card = Instantiate(cardPrefab);
        // 将卡牌加到指定的图层中
        card.transform.SetParent(cardPanel, false);
        BattleFieldManager.Instance.GetArmHandCards().Add(card.transform);

        ArrangeCards();
    }

    /*
     * 重新排列卡牌
    */
    private void ArrangeCards() {
        // 空隙
        float space = 50;
        // 配置的半径
        float radius = 300;
        // 获取偏移的角度
        float angle = Mathf.Atan(space / radius) * 180 / Mathf.PI;

        // 遍历数组
        for (int i = 0; i < BattleFieldManager.Instance.GetArmHandCards().Count; i++) {
            // 重定义位置
            float loc = i - (float)(BattleFieldManager.Instance.GetArmHandCards().Count - 1) / 2;
            
            Transform cardTransform = (Transform)BattleFieldManager.Instance.GetArmHandCards()[i];
            // 设置位置
            cardTransform.localPosition = new Vector3(loc * space, 0, 0);
            // 设置角度
            cardTransform.localRotation = Quaternion.Euler(0, 0, angle * (loc * -1));

            float scale = 1 - (Mathf.Abs(loc) / 5);
            cardTransform.localScale = new Vector3(scale, scale, 1);
        }
    }

    /*
     * 清理手牌
    */
    private void CleatCards() {
        foreach (Transform card in BattleFieldManager.Instance.GetArmHandCards()) {
            Destroy(card.gameObject);
        }

        BattleFieldManager.Instance.GetArmHandCards().Clear();

        EventListener.Instance.PostEvent(EventEnum.EVENT_ENTER_PLAY_A_HAND_ROUND);
    }
}
