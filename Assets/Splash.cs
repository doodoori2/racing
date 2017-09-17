using System.Collections;
using UnityEngine;

public class Splash : MonoBehaviour
{
    public GameObject[] textArray;
    private bool okToSkip = false;
    public AudioClip clip;

    private IEnumerator Start()
    {
        foreach (var t in textArray)
        {
            AudioSource.PlayClipAtPoint(clip, Vector3.zero);
            t.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            
        }
        okToSkip = true;
    }

    private void Update()
    {
        if (okToSkip && Input.GetKey(KeyCode.S))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        }
    }
}
