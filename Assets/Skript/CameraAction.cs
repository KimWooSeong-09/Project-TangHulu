using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CameraActionHandler : MonoBehaviour 
{
    public GameObject Target;
    
    public float offsetX = 0f;
    public float offsetY = 25f;
    public float offsetZ = 0f;

    public float cameraSpeed = 3f;
    private Vector3 TargetPosition;

    public void FixedUpdate()
    {
        Vector3 cameraAction = new Vector3(
            Target.transform.localPosition.x + offsetX,
            Target.transform.localPosition.y + offsetY,
            Target.transform.localPosition.z + offsetZ
        );
        
        transform.position = Vector3.Lerp(transform.position, cameraAction, Time.deltaTime * cameraSpeed);
    }
    
    
}
