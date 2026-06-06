using UnityEngine;
using System;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class PortalSprite:MonoBehaviour
{
    public string TargetSceneName;
    public bool closPortal = false;
    private void Update()
    {
        if (closPortal&&Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(TargetSceneName);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            closPortal = true;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        closPortal = true;
    }

}
