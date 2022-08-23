using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuTankRotator : MonoBehaviour
{
    public GameObject RotationObject;
    private float z_delta;
    public void RotateLeft(float delta)
    {
        Vector3 rotation = RotationObject.transform.rotation.eulerAngles;
        RotationObject.transform.rotation = Quaternion.Euler(rotation.x, rotation.y + delta, rotation.z+ z_delta);
    }
    public void RotateRight(float delta)
    {
        Vector3 rotation = RotationObject.transform.rotation.eulerAngles;
        RotationObject.transform.rotation = Quaternion.Euler(rotation.x, rotation.y + delta, rotation.z + z_delta);
    }
}
