using UnityEngine;

public class DontDestroyA: MonoBehaviour
{
    void Awake()
    {
 
        DontDestroyOnLoad(gameObject);

    }
}
