using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject _firstButton;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _clickSound;
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_firstButton);
    }
    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSecondsRealtime(_clickSound.length);
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        StartCoroutine(QuitGameCoroutine());
    }

    IEnumerator QuitGameCoroutine()
    {
        yield return new WaitForSecondsRealtime(_clickSound.length);
        Application.Quit();
    }

    public void PlayClickSound()
    {
        _audioSource.PlayOneShot(_clickSound);
    }
}
