using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;
using System;
using UdonSharp;

//this script is crude but lists apparently don't want to work in udonSharp
//Easily expandable though lol
//Written by: Doost


public class VRCItemRespawn : UdonSharpBehaviour {

	public GameObject gameObjectRespawn;
	public GameObject gameObjectRespawn2;
	public GameObject gameObjectRespawn3;
	public GameObject gameObjectRespawn4;
	public GameObject gameObjectRespawn5;
	public GameObject gameObjectRespawn6;
	public GameObject gameObjectRespawn7;
	public GameObject gameObjectRespawn8;

	public int RespawnTimer;
	int origTimer;
	Vector3 origLocation;
	Vector3 origLocation2;
	Vector3 origLocation3;
	Vector3 origLocation4;
	Vector3 origLocation5;
	Vector3 origLocation6;
	Vector3 origLocation7;
	Vector3 origLocation8;


	private void Start() {
		//set the original location of the objects
		origLocation = gameObjectRespawn.transform.position;
		origLocation2 = gameObjectRespawn2.transform.position;
		origLocation3 = gameObjectRespawn3.transform.position;
		origLocation4 = gameObjectRespawn4.transform.position;
		origLocation5 = gameObjectRespawn5.transform.position;
		origLocation6 = gameObjectRespawn6.transform.position;
		origLocation7 = gameObjectRespawn6.transform.position;
		origLocation8 = gameObjectRespawn6.transform.position;
		//respawn timer increment to internal variable
		origTimer = RespawnTimer;
	}

	void LateUpdate() {
		if (Time.unscaledTime > RespawnTimer) {
			//set the location of the object
			gameObjectRespawn.transform.position = origLocation;
			gameObjectRespawn2.transform.position = origLocation2;
			gameObjectRespawn3.transform.position = origLocation3;
			gameObjectRespawn4.transform.position = origLocation4;
			gameObjectRespawn5.transform.position = origLocation5;
			gameObjectRespawn6.transform.position = origLocation6;
			gameObjectRespawn7.transform.position = origLocation7;
			gameObjectRespawn8.transform.position = origLocation8;
			//fix the velocity of the object so it doesn't fly away on respawn
			gameObjectRespawn.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
			gameObjectRespawn2.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
			gameObjectRespawn3.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
			gameObjectRespawn4.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
			gameObjectRespawn5.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
			gameObjectRespawn6.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
			gameObjectRespawn7.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
			gameObjectRespawn8.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
			//increment the respawn timer
			RespawnTimer += origTimer;
		}

	}
}