using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePercist : MonoBehaviour
{

    void Awake()
    {
        int numScenePersists = FindObjectsOfType<ScenePercist>().Length;
        if (numScenePersists > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetScenePersist()
    {
        Destroy(gameObject);
    }

}
