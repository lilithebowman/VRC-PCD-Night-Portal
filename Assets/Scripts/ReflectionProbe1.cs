
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ReflectionProbe1 : UdonSharpBehaviour
{
    public GameObject probeGameObject;

    private VRCPlayerApi playerLocal;
    void Start() {
        playerLocal = Networking.LocalPlayer;
    }

    void LateUpdate() {
        // move realtime probe to player head
        probeGameObject.transform.position = playerLocal.GetBonePosition(UnityEngine.HumanBodyBones.Head);
    }
}
