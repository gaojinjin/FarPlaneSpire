using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    配置单例模板
*/
public class Singleton<T> where T: new()
{
    private static T instance = default(T);
    private static object sync = new Object();

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                lock (sync)
                {
                    if (instance == null)
                    {
                        instance = new T();
                    }
                }
            }

            return instance;
        }
    }
}
