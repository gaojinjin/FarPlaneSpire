using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 战场单例，用来存放战场的各种数据
*/
public class BattleFieldManager : Singleton<BattleFieldManager> {
    // 我方和敌人的手牌都放在
    private ArrayList armyHandCards;
    private ArrayList enemyHandCards;

    /*
     * 获取我方手牌
    */
    public ArrayList GetArmHandCards() {
        return armyHandCards;
    }

    public BattleFieldManager() {
        // 初始化手牌
        armyHandCards = new ArrayList();
        enemyHandCards = new ArrayList();
    }
}
