using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ���͵����������������һ��˽�е��޲ι��췽��
/// </summary>
/// <typeparam name="T">����</typeparam>
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
                // ͨ������õ���˽���޲ι��췽����ʵ��������
                instance = Activator.CreateInstance(typeof(T), true) as T;
            }
            return instance; 
        }
    }
}