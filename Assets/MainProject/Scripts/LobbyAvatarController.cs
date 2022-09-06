using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapTransformLobby
{
    public Transform vrTarget;
    public Transform IKTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void MapVRAvatarLobby()
    {
        IKTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        IKTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}
public class LobbyAvatarController : MonoBehaviour
{
    [SerializeField] private MapTransformLobby head;
    [SerializeField] private MapTransformLobby leftHand;
    [SerializeField] private MapTransformLobby rightHand;

    [SerializeField] private float turnSmoothness;

    [SerializeField] private Transform IKHead;

    [SerializeField] private Vector3 headBodyOffset;


    void LateUpdate()
    {
        transform.position = IKHead.position + headBodyOffset;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, head.vrTarget.rotation.eulerAngles.y, 0)), 0.05f);
        transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(IKHead.forward, Vector3.up).normalized, Time.deltaTime * turnSmoothness); ;
        head.MapVRAvatarLobby();
        leftHand.MapVRAvatarLobby();
        rightHand.MapVRAvatarLobby();
    }
}
