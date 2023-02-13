using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSEvent;

// 战场相关
namespace FSBattle {
    // 战场回合状态
    public enum BattleFieldRound {
        BATTLE_FIELD_READY_GAME         = 0X0010001,     // 游戏准备
        BATTLE_FIELD_DRAW_CARDS         = 0X0010002,     // 抽牌
        BATTLE_FIELD_ENEMY_DEAL         = 0X0010003,     // 敌方出牌
        BATTLE_FIELD_PLAYER_DEAL        = 0X0010004,     // 敌方出牌
        BATTLE_FIELD_COMPAIRE_CARDS     = 0X0010005,     // 比牌
        BATTLE_FIELD_MAKE_UP_CARDS      = 0X0010006,     // 补牌
        BATTLE_FIELD_PLAYER_PLAY_A_HAND = 0X0010007,     // 玩家出牌
        BATTLE_FIELD_ENEMY_ACTION       = 0X0010008,     // 敌人行动
        BATTLE_FIELD_ROUND_END          = 0X0010009,     // 回合结束
    }

    // 战场组件
    public class BattleField : MonoBehaviour {
        // 记录战场状态
        private BattleFieldRound round = BattleFieldRound.BATTLE_FIELD_READY_GAME;

        // Use this for initialization
        void Start() {
            InitEvent();

            Invoke("ReadyHandle", 1.0f);
        }

        // Update is called once per frame
        void Update() {
            
        }

        private void OnDestroy() {
            RemoveEvent();
        }

        /*
         * 注册事件
        */
        private void InitEvent() {
            EventListener.Instance.RegisterEvent(EventEnum.EVENT_ENTER_COMPAIRE_CARD_ROUND, EnterCompaireRound);
            EventListener.Instance.RegisterEvent(EventEnum.EVENT_ENTER_ENEMY_DEAL_ROUND, EnterEnemyDealRound);
            EventListener.Instance.RegisterEvent(EventEnum.EVENT_ENTER_PLAY_A_HAND_ROUND, EnterPlayAHandRound);
        }

        /*
         * 移除事件
        */
        private void RemoveEvent() {
            EventListener.Instance.RemoveEvent(EventEnum.EVENT_ENTER_COMPAIRE_CARD_ROUND);
            EventListener.Instance.RemoveEvent(EventEnum.EVENT_ENTER_PLAY_A_HAND_ROUND);
        }

        /*
         * 进入下一个回合
        */
        public void NextRound() {
            // 回合循环转化
            switch (round) {
                case BattleFieldRound.BATTLE_FIELD_READY_GAME:
                    round = BattleFieldRound.BATTLE_FIELD_DRAW_CARDS;
                    // 推送玩家抽牌和敌人抽牌事件
                    EventListener.Instance.PostEvent(EventEnum.EVENT_PLAYER_SHUFFLING_CARD);
                    // 为了需要再添加敌人抽牌的事件
                    print("抽牌");
                    break;

                case BattleFieldRound.BATTLE_FIELD_DRAW_CARDS:
                    round = BattleFieldRound.BATTLE_FIELD_ENEMY_DEAL;
                    // 暂无行动
                    // 等未来敌人管理类添加后再处理，当前过2秒后进入下一回合
                    Invoke("NextRound", 2.0f);
                    print("敌人出牌");
                    break;

                case BattleFieldRound.BATTLE_FIELD_ENEMY_DEAL:
                    round = BattleFieldRound.BATTLE_FIELD_PLAYER_DEAL;
                    print("我方出牌，直到按下回合结束按钮 ... ");
                    break;

                case BattleFieldRound.BATTLE_FIELD_PLAYER_DEAL:
                    round = BattleFieldRound.BATTLE_FIELD_COMPAIRE_CARDS;
                    // 暂无行动
                    // 未来在此实现比牌功能，当前过2秒后进入下一回合
                    Invoke("NextRound", 2.0f);
                    print("开始比牌 ... ");
                    break;

                case BattleFieldRound.BATTLE_FIELD_COMPAIRE_CARDS:
                    round = BattleFieldRound.BATTLE_FIELD_READY_GAME;
                    // 回合重新回到开始回合，过2秒后进入下一回合
                    Invoke("NextRound", 2.0f);
                    print("准备好了 ... ");
                    break;

                case BattleFieldRound.BATTLE_FIELD_MAKE_UP_CARDS:
                    break;
            }
        }

        /*
         * 准备游戏回合的事件处理 
        */
        private void ReadyHandle() {
            // 通过消息中心，发布带参数的消息
            Dictionary<string, object> info = new Dictionary<string, object>();
            info.Add("remind", "准备中 ... ");

            EventListener.Instance.PostEvent(EventEnum.EVENT_UPDATE_ROUND_TEXT, info);

            // 过1秒后进入下一回合
            Invoke("NextRound", 1.0f);
        }

        /*
         * 抽牌结束，进入敌人行动回合
        */
        private void EnterEnemyDealRound() {
            // 保证当前处于抽牌阶段
            if (round == BattleFieldRound.BATTLE_FIELD_DRAW_CARDS) {
                NextRound();
            }
        }

        /*
         * 进入比牌回合
        */
        private void EnterCompaireRound() {
            // 首先得保证上一个回合是我方出牌回合
            if (round == BattleFieldRound.BATTLE_FIELD_PLAYER_DEAL) {
                NextRound();
            }
        }

        /*
         * 回合轮回，重新开始
        */
        private void EnterPlayAHandRound() {
            // 首先需要保证上一个回合是结束回合
            if (round == BattleFieldRound.BATTLE_FIELD_ROUND_END) {
                NextRound();
            }
        }
    }
}
