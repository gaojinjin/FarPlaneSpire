using System;
namespace FSEvent {
    public enum EventEnum {
        EVENT_BATTLE_READY_GAME                 = 0x0001001,
        EVENT_PLAYER_SHUFFLING_CARD             = 0x0001002,      // 玩家抽牌
        EVENT_ENTER_COMPAIRE_CARD_ROUND         = 0x0001003,      // 进入敌方行动回合
        EVENT_BATTLE_FIELD_ROUND_END            = 0x0001004,      // 回合结束
        EVENT_CLEAR_CARDS                       = 0x0001005,      // 清理手牌
        EVENT_ENTER_PLAY_A_HAND_ROUND           = 0x0001006,      // 清理手牌
        EVENT_ENTER_ENEMY_DEAL_ROUND            = 0x0001007,      // 进入敌人出牌的回合

        EVENT_UPDATE_ROUND_TEXT                 = 0x0002003,      // 更新回合文本
    }
}
