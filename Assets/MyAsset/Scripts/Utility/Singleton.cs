using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 泛型单例，子类必须声明一个私有的无参构造方法
/// </summary>
/// <typeparam name="T">泛型</typeparam>
public abstract class Singleton<T> where T : class
{
    private static T instance;
    //protected GameObject gameObject = Main.Instance.gameObject;
    //protected MonoBehaviour mono = Main.Instance.Mono;

    //public GameObject GameObject { get { return gameObject; } set { gameObject = value; } }
    //public MonoBehaviour Mono { get { return mono; } set { mono = value; } }

    public static T Instance
    {
        get 
        {
            if (instance == null)
            {
                // 通过反射得到的私有无参构造方法来实例化对象
                instance = Activator.CreateInstance(typeof(T), true) as T;
            }
            return instance; 
        }
    }
}