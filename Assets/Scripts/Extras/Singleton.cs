using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
   private static T miInstance;
   public static T Instance
   {
       get
       {
           if (miInstance == null)
           {
               miInstance = FindObjectOfType<T>();
               if (miInstance == null)
               {
                   GameObject nuevoGO = new GameObject();
                   miInstance = nuevoGO.AddComponent<T>();
               }
           }
           return miInstance;
       }
       
   }

protected virtual void Awake() {
    
miInstance = this as T;

}

}
