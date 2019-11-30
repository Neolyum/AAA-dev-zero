using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Object.FindObjectOfType(typeof(T)) as T;

                if (_instance == null)
                {
                    Debug.Log("No instance of " + typeof(T) + ", a temporary one is created.");
                    _instance = new GameObject("Temp Instance of " + typeof(T), typeof(T)).GetComponent<T>();
                }
            }

            return _instance;
        }
    }

    // If no other monobehaviour request the instance in an awake function
    // executing before this one, no need to search the object.
    private void Awake()
    {
        if (_instance == null)
        {
            DontDestroyOnLoad(gameObject);
            _instance = this as T;
        }
        //else
        //{
        //    Destroy(gameObject);
        //}
    }
}