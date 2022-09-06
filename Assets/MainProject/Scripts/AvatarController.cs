using UnityEngine;
using Mirror;
using Unity.VisualScripting;

[System.Serializable]
public class MapTransform
{
    public Transform vrTarget;
    public Transform IKTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void MapVRAvatar()
    {
        IKTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        IKTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}
public class AvatarController : NetworkBehaviour
{
    [SerializeField] private GameObject avatar;
    [SerializeField] private MapTransform head;
    [SerializeField] private MapTransform leftHand;
    [SerializeField] private MapTransform rightHand;

    [SerializeField] private float turnSmoothness;

    [SerializeField] private Transform IKHead;

    [SerializeField] private Vector3 headBodyOffset;

    private void Start()
    {
        if(!isLocalPlayer)
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    void LateUpdate()
    {
        if(!isLocalPlayer)
        {
            return;
        }
        avatar.transform.position = IKHead.position + headBodyOffset;
        avatar.transform.rotation = Quaternion.Lerp(avatar.transform.rotation, Quaternion.Euler(new Vector3(0, head.vrTarget.rotation.eulerAngles.y, 0)), 0.05f);
        avatar.transform.forward = Vector3.Lerp(avatar.transform.forward, Vector3.ProjectOnPlane(IKHead.forward, Vector3.up).normalized, Time.deltaTime * turnSmoothness); ;
        head.MapVRAvatar();
        leftHand.MapVRAvatar();
        rightHand.MapVRAvatar();
    }
}
