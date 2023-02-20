using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


public class FOVFixer : MonoBehaviour
{
    [SerializeField]
    private Camera m_camera;
    
    [SerializeField]
    private float targetFOV = 90;
    private float targetFOV_V;
    private float hFOVrad;
    private const float DefaultAspectRatio = 16f / 10f;

    private const float checkDelay = 0.2f;
    private WaitForSeconds waitForSecondsCheck;

    private float prevAspect;
    private float curAspect;
    

    private void Awake()
    {
        targetFOV_V = targetFOV * 0.75f;
        hFOVrad = targetFOV * Mathf.Deg2Rad;
        waitForSecondsCheck = new WaitForSeconds(checkDelay);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        curAspect = prevAspect = m_camera.aspect;
        Resize(curAspect);

        StartCoroutine(DoCheckForResize());
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private IEnumerator DoCheckForResize() 
    {
        while (true) 
        {
            curAspect = m_camera.aspect;
            if (curAspect != prevAspect) 
            {
                Resize(curAspect);
                prevAspect = curAspect;
            }
            yield return waitForSecondsCheck;
        }
    }

    private void Resize(float aspect) 
    {
        if (aspect < DefaultAspectRatio)
        {
            float camH = Mathf.Tan(hFOVrad * 0.5f) / aspect;
            float vFOVrad = Mathf.Atan(camH) * 2;
            m_camera.fieldOfView = vFOVrad * Mathf.Rad2Deg;
        }
        else
        {
            m_camera.fieldOfView = targetFOV_V;
        }
    }
}
