using System.Collections;
//using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadCanvas : MonoBehaviour
{
    static private SceneLoadCanvas instance;
    static public SceneLoadCanvas Instance {
        get
        {
            if (instance != null)
                return instance;
            else
               return instance = Instantiate<SceneLoadCanvas>(Resources.Load("UI/SceneLoader").GetComponent<SceneLoadCanvas>());
        }
    private set { }
    }
    const float sceneLoadTime=1f;
    [SerializeField] private Image _background;

    Coroutine coroutine;
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(this);
    }
    public void LoadScene(int number)
    {
        if (coroutine!=null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        coroutine=StartCoroutine(LoadRoutine(number));
    }
    
    private IEnumerator LoadRoutine( int number )
    {
        Debug.Log("lul");
        var loader = SceneManager.LoadSceneAsync(number);
        loader.allowSceneActivation = false;
        var startTime = Time.time;
        Debug.Log(loader.isDone);
        while ((Time.time < (startTime+sceneLoadTime))&&!loader.isDone)
        {
            var progress = (Time.time - startTime) / sceneLoadTime;
            Debug.Log(progress);
            _background.color =new Color(0,0,0, progress);
            yield return null;
        }
        loader.allowSceneActivation = true;
        startTime = Time.time;
        while (Time.time < startTime + sceneLoadTime)
        {
            _background.color = new Color(0,0,0, (sceneLoadTime -(Time.time - startTime)) / sceneLoadTime);
         yield return null;
        }
    }
}
