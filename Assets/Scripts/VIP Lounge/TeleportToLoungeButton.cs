
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class TeleportToLoungeButton : UdonSharpBehaviour {
	[Tooltip("The event message to send on a player interact event.")]
	public string interactEvent;
	private VRCPlayerApi playerLocal;
	public GameObject VIPLounge;

	void Start() {
		playerLocal = Networking.LocalPlayer;
	}

	public override void Interact() {
		playerLocal.TeleportTo(VIPLounge.transform.position, VIPLounge.transform.rotation);
	}
}
