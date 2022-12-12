using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTon<T> where T:class,new()
{
    private static T instance;
    private static object LocObj = new object();
    public static T Instance
    {
        get
        {
            if (instance==null)
            {
                lock (LocObj)
                {
                    if (instance==null)
                    {
                        instance = new T();
                    }
                }
            }
            return instance;
        }
    }
}
