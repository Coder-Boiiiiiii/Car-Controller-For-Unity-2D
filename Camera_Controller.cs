using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    [Header("Settings")]
    public Transform Camera;
    public Transform Target;
    public Vector3 Offset;

    void Update(){
        Camera.position = Target.position + Offset;
    }
}
