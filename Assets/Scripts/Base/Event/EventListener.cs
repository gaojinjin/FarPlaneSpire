using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FSEvent {
    /*
     * 事件系统，用来各个模块之间的通
     * 用一个字典来保存数据
    */
    public class EventListener : Singleton<EventListener> {
        private Dictionary<EventEnum, object> actions;

        public EventListener() {
            Debug.Log("监听系统初始化...");

            actions = new Dictionary<EventEnum, object>();
        }

        /*
         * 注册事件，往字典中添加数据
        */
        public void RegisterEvent(EventEnum key, UnityAction value) {
            // 首先判断是否已经存在该方法
            if (actions.ContainsKey(key))
                throw new System.Exception("该方法已被注册...");

            actions.Add(key, value);
        }

        /*
         * 注册事件，带参数的事件
        */
        public void RegisterEvent(EventEnum key, UnityAction<Dictionary<string, object>> value) {
            // 首先判断是否已经存在该方法
            if (actions.ContainsKey(key))
                throw new System.Exception("该方法已被注册...");

            actions.Add(key, value);
        }

        /*
         * 通知监听中心，执行指定的方法
        */
        public void PostEvent(EventEnum key) {
            // 当事件已被注册时，执行指定的方法
            if (actions.ContainsKey(key)) {
                // 获取到事件
                object value = null;
                actions.TryGetValue(key, out value);

                UnityAction action = (UnityAction)value;

                // 如果方法可用
                if (action != null)
                    action();
            }
        }

        /*
         * 执行带参数的方法
         * 传入键和参数字典
        */
        public void PostEvent(EventEnum key, Dictionary<string, object> info) {
            // 当事件已被注册时，执行指定的方法
            if (actions.ContainsKey(key)) {
                // 获取到事件
                object value = null;
                actions.TryGetValue(key, out value);

                // 此处用带参数的来接收
                UnityAction<Dictionary<string, object>> action = (UnityAction<Dictionary<string, object>>)value;

                // 如果方法可用
                if (action != null)
                    action(info);
            }
        }

        /*
         * 根据key来移除指定的方法
        */
        public void RemoveEvent(EventEnum key) {
            // 当字典中存在该方法时，将其移除
            if (actions.ContainsKey(key)) {
                actions.Remove(key);
            }
        }
    }
}
