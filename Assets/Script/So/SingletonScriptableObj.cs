using System;
using System.Linq;
using UnityEngine;

namespace Script.So {
    public class SingletonScriptableObj<T> : ScriptableObject where T : SingletonScriptableObj<T> {
        private static T instance;

        public static T Instance {
            get {
                if (instance != null) return instance;

                var assets = Resources.LoadAll<T>("");
                if (assets == null)
                    throw new SystemException("Could not find SO, make sure it resides in resources.");
                if (assets.Length > 1)
                    Debug.LogWarning("Too many instances of SO");
                instance = assets.First();

                return instance;
            }
        }
    }
}