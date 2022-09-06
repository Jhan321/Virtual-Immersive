using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReadyPlayerMe;

public class LobbyAvatar : MonoBehaviour
{
    string nameAvatar;
    private void Start()
    {
        if (PlayerPrefs.HasKey("avatar"))
        {
            string n = PlayerPrefs.GetString("avatar");
            string noGlb = n.Split('/')[3];
            nameAvatar = noGlb.Split('.')[0];
            Debug.Log("Avatar name instance is: " + nameAvatar);
            Load_Mesh_Avatar(n);
        }
        //string n = "https://d1a370nemizbjq.cloudfront.net/749a2c08-03de-4c55-8110-dc2d421396b6.glb";
        //string noGlb = n.Split('/')[3];
        //nameAvatar = noGlb.Split('.')[0];
        //Load_Mesh_Avatar(n);
    }
    void Load_Mesh_Avatar(string avatarURL)
    {
        Debug.Log($"Started loading avatar");
        AvatarLoader avatarLoader = new AvatarLoader();
        avatarLoader.OnCompleted += AvatarLoadComplete;
        avatarLoader.OnFailed += AvatarLoadFail;
        avatarLoader.LoadAvatar(avatarURL);
    }

    private void AvatarLoadComplete(object sender, CompletionEventArgs args)
    {
        Debug.Log($"Avatar loaded");
        GameObject userAvatar = GameObject.Find(nameAvatar);
        GameObject avatar = GameObject.Find("Avatar");
        userAvatar.transform.parent = avatar.transform;
        userAvatar.transform.localPosition = new Vector3(0, 0, 0);
        userAvatar.transform.localRotation = Quaternion.Euler(0, 0, 0);
        int countMesh = 0;
        for (int i = 0; i < userAvatar.transform.childCount; i++)
        {
            if (userAvatar.transform.GetChild(i).transform.GetComponent<SkinnedMeshRenderer>())
            {
                countMesh++;
            }
        }
        for (int i = 0; i < countMesh; i++)
        {
            avatar.transform.GetChild(i).GetComponent<SkinnedMeshRenderer>().sharedMesh = userAvatar.transform.GetChild(i).GetComponent<SkinnedMeshRenderer>().sharedMesh;
            avatar.transform.GetChild(i).GetComponent<SkinnedMeshRenderer>().material = userAvatar.transform.GetChild(i).GetComponent<SkinnedMeshRenderer>().material;
        }
        for (int i = 0; i < userAvatar.transform.childCount; i++)
        {
            userAvatar.transform.GetChild(i).gameObject.SetActive(false);
        }
        //transform.GetComponent<EyeAnimationHandler>().enabled = true;
        if (userAvatar.transform.GetComponent<Animator>())
        {
            Destroy(userAvatar.transform.GetComponent<Animator>());
        }
        avatar.name = nameAvatar;
    }

    private void AvatarLoadFail(object sender, FailureEventArgs args)
    {
        Debug.Log($"Avatar loading failed with error message: {args.Message}");
    }
}
