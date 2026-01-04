using System.Collections;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{

    private static CameraHandler _instance;
    public static CameraHandler Instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject go = new GameObject("Camera Handler");
                _instance = go.AddComponent<CameraHandler>();
            }
            return _instance;
        }
    }

    private Coroutine MoveCameraRoutine;
    public float transitionDuration { get; private set; } = 1f;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void MoveCamera(Camera camera, Vector2 position)
    {
        //if (MoveCameraRoutine != null)
        //{
        //    StopCoroutine(MoveCameraRoutine);
        //}
        MoveCameraRoutine = StartCoroutine(LerpCamera(camera.gameObject, position));
    }

    IEnumerator LerpCamera(GameObject camObject, Vector2 DesiredPos)
    {
        
        Vector3 originalPos = new Vector3(camObject.transform.position.x, camObject.transform.position.y, -10);
        Vector3 myFinalPos = new Vector3(DesiredPos.x, DesiredPos.y, -10);
        float elapsedTime = 0f;
        float duration = transitionDuration;
        while(elapsedTime <= duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            camObject.transform.position = Vector3.Lerp(originalPos, myFinalPos, elapsedTime / duration);
            yield return null;
        }
        yield return null;


    }
}
