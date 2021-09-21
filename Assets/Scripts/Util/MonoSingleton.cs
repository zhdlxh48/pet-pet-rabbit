using UnityEngine;

namespace Rabbit
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get {
                var objs = FindObjectsOfType<T>();
                if (objs.Length == 0) {
                    Debug.LogError($"Instance({typeof(T).Name}) is null. Recreate.");
                    var instanceObj = new GameObject(typeof(T).ToString());
                    _instance = instanceObj.AddComponent<T>();
                }
                if (objs.Length > 1) {
                    Debug.LogError($"Instance({typeof(T).Name}) is not only one. Destroy all objects except one.");
                    for (var i = 1; i < objs.Length; i++) {
                        DestroyImmediate(objs[i].transform.root.gameObject);
                    }
                }
                _instance = objs[0];
                
                return _instance;
            }
        }
    }
}