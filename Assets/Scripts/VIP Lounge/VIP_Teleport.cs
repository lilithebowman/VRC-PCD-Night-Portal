
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.SDK3.Components;
using VRC.Udon;
using System;

public class VIP_Teleport : UdonSharpBehaviour {
	public GameObject VIPLounge;
	public string[] VIPList;
	public string[] VIPListUIDs;
	public AudioSource alertSource;

	private VRCPlayerApi playerLocal;
	private float intervalStart;
	private VRCPlayerApi[] players;
	readonly float timeout = 1f;

    void Start() {
		playerLocal = Networking.LocalPlayer;
		intervalStart = Time.timeSinceLevelLoad;
		players = new VRCPlayerApi[100];
		Debug.Log("Script initialized **********");
		VRCPlayerApi.GetPlayers(players);
	}

	private void Update() {
		if (Time.timeSinceLevelLoad - intervalStart > timeout) {
			foreach (VRCPlayerApi player in players) {
				if (player == null) continue;
				// Debug.Log(player.displayName);
				// Debug.Log(VRCPlayerApi.GetPlayerId(player));
				foreach (string vip in VIPList) {
					// Teleport the player to the "VIP" lounge!
					if (player.displayName == vip) {
						/* If the local player is a VIP, teleport. Otherwise, notify others so they get jealous of the player's VIP privilege! */
						if (playerLocal.displayName == player.displayName) {
							playerLocal.TeleportTo(VIPLounge.transform.position, VIPLounge.transform.rotation);
						} else if (!alertSource.isPlaying) {
							Debug.Log("Found a VIP!");
							player.TeleportTo(VIPLounge.transform.position, VIPLounge.transform.rotation);
							alertSource.Play();
						}
					}
				}
			}

			intervalStart = Time.timeSinceLevelLoad;
		}
	}
}
