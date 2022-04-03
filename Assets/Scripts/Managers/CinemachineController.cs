using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineController : MonoBehaviour
{
    public CinemachineVirtualCamera playerCamera, treasureCamera;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1.0f);
        treasureCamera.Priority = 15;
        yield return new WaitForSeconds(3.0f);
        treasureCamera.Priority = 9;
    }
}
