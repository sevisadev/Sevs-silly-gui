using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using SevsSillyGui;
using BepInEx;
using ExitGames.Client.Photon;
using GorillaLocomotion.Gameplay;
using GorillaNetworking;
using GorillaTagScripts;
using GorillaTagScripts.ObstacleCourse;
using HarmonyLib;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.XR;



namespace SevsSillyGui.Resources
{
	public class RigUtils
	{
		public static VRRig GetVRRigFromPlayer(NetPlayer p)
		{
			return GorillaGameManager.instance.FindPlayerVRRig(p);
		}
		public static Player GetRandomPlayer(bool includeSelf)
		{
			Player result;
			if (includeSelf)
			{
				result = PhotonNetwork.PlayerList[UnityEngine.Random.Range(0, PhotonNetwork.PlayerList.Length - 1)];
			}
			else
			{
				result = PhotonNetwork.PlayerListOthers[UnityEngine.Random.Range(0, PhotonNetwork.PlayerListOthers.Length - 1)];
			}
			return result;
		}
		public static VRRig GetRandomRig(bool includeMyRig, bool HasToBeTagged = false, bool HasToBeSurvivor = false)
		{
			Photon.Realtime.Player[] array = includeMyRig ? PhotonNetwork.PlayerList : PhotonNetwork.PlayerListOthers;
			Player val = array[UnityEngine.Random.Range(0, array.Length)];
			NetPlayer netPlayer = val; // Implicit conversion happens here
			VRRig val2 = GorillaGameManager.instance.FindPlayerVRRig(netPlayer);

			if (HasToBeTagged)
			{
				if (isThisPlayerTagged(val2.OwningNetPlayer))
				{
					return val2;
				}
				return GetRandomRig(includeMyRig, HasToBeTagged);
			}

			if (HasToBeSurvivor)
			{
				if (!isThisPlayerTagged(val2.OwningNetPlayer))
				{
					return val2;
				}
				if (!IsEveryoneTagged())
				{
					return GetRandomRig(includeMyRig, HasToBeTagged, HasToBeSurvivor);
				}
				return val2;
			}

			return val2;
		}


		public static VRRig FindClosestVRRig()
		{
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			float num = float.MaxValue;
			VRRig result = null;
			foreach (VRRig vrrig in ((GorillaParent)GorillaParent.instance).vrrigs)
			{
				float num2 = Vector3.Distance(((Component)GorillaTagger.Instance.bodyCollider).transform.position, ((Component)vrrig).transform.position);
				if (num2 < num && (System.Object)(object)vrrig != (System.Object)(object)GorillaTagger.Instance.offlineVRRig)
				{
					num = num2;
					result = vrrig;
				}
			}
			return result;
		}

		public static VRRig GetClosestVRRig(bool mustbetaggethed = false, bool mustbesurviverd = false)
		{
			float num = float.MaxValue;
			VRRig result = null;
			List<VRRig> vrrigs = ((GorillaParent)GorillaParent.instance).vrrigs;
			int num2 = vrrigs.Count - 1;
			for (int num3 = num2; num3 >= 0; num3--)
			{
				VRRig val = vrrigs[num3];
				if (Vector3.Distance(((Component)GorillaTagger.Instance.bodyCollider).transform.position, ((Component)val).transform.position) < num && (System.Object)(object)val != (System.Object)(object)GorillaTagger.Instance.offlineVRRig && Vector3.Distance(((Component)GorillaTagger.Instance.bodyCollider).transform.position, ((Component)val).transform.position) < float.MaxValue)
				{
					if (mustbetaggethed)
					{
						if (isThisPlayerTagged(val.OwningNetPlayer))
						{
							num = Vector3.Distance(((Component)GorillaTagger.Instance.bodyCollider).transform.position, ((Component)val).transform.position);
							result = val;
							break;
						}
					}
					else
					{
						if (!mustbesurviverd)
						{
							num = Vector3.Distance(((Component)GorillaTagger.Instance.bodyCollider).transform.position, ((Component)val).transform.position);
							result = val;
							break;
						}
						if (!isThisPlayerTagged(val.OwningNetPlayer))
						{
							num = Vector3.Distance(((Component)GorillaTagger.Instance.bodyCollider).transform.position, ((Component)val).transform.position);
							result = val;
							break;
						}
					}
				}
			}
			return result;
		}

		public static bool isThisPlayerTagged(NetPlayer player)
		{
			if (GetGorillaTagManager().currentInfected.Contains(player))
			{
				return true;
			}
			if (GetGorillaTagManager().currentIt == player)
			{
				return true;
			}
			return false;
		}

		public static NetworkView GetNetworkViewFromVRRig(VRRig p)
		{
			return (NetworkView)Traverse.Create((object)p).Field("netView").GetValue();
		}

		public static VRRig GetRigFromName(string name)
		{
			Photon.Realtime.Player[] playerList = PhotonNetwork.PlayerList;
			Player val = playerList[UnityEngine.Random.Range(0, playerList.Length)];
			NetPlayer netPlayer = val; // Implicit conversion happens here
			VRRig val2 = GorillaGameManager.instance.FindPlayerVRRig(netPlayer);

			if (val2.playerName == name)
			{
				return val2;
			}

			return GetRigFromName(name);
		}



		public static bool IsEveryoneTagged()
		{
			bool result = false;
			foreach (VRRig vrrig in ((GorillaParent)GorillaParent.instance).vrrigs)
			{
				if (!isThisPlayerTagged(vrrig.OwningNetPlayer))
				{
					result = true;
					break;
				}
			}
			return result;
		}

		public static GorillaTagManager GetGorillaTagManager()
		{
			return GameObject.Find("GT Systems/GameModeSystem/Gorilla Tag Manager").GetComponent<GorillaTagManager>();
		}
		public static Player NetPlayerToPlayer(NetPlayer p)
		{
			return p.GetPlayerRef();
		}

		public static NetPlayer GetPlayerFromVRRig(VRRig p)
		{
			//return GetPhotonViewFromVRRig(p).Owner;
			return p.Creator;
		}

		public static NetPlayer GetPlayerFromID(string id)
		{
			NetPlayer found = null;
			foreach (Photon.Realtime.Player target in PhotonNetwork.PlayerList)
			{
				if (target.UserId == id)
				{
					found = target;
					break;
				}
			}
			return found;
		}
		public static bool PlayerIsTagged(VRRig who)
		{
			string text = ((UnityEngine.Object)((Renderer)who.mainSkin).material).name.ToLower();
			return text.Contains("fected") || text.Contains("it") || text.Contains("stealth") || !who.nameTagAnchor.activeSelf;
		}
	}

	public class gun
    {

		public static VRRig Locked = null;
		// GUN (base) FROM IIDK
		public static (RaycastHit Ray, GameObject NewPointer, VRRig playerrr) RenderGun(bool PlayerLock = true)
		{
			var DHTSF = Plugin.DH == "R" ? GorillaLocomotion.Player.Instance.rightControllerTransform : GorillaLocomotion.Player.Instance.leftControllerTransform;
			if (Mouse.current.rightButton.isPressed)
            {
				DHTSF = GorillaLocomotion.Player.Instance.headCollider.transform;
			}
			bool disableGunLine = false;
			bool smallGunPointer = true;
			bool legacyGunDirection = true;
			bool disableGunPointer = false;
			Physics.Raycast(DHTSF.position - (legacyGunDirection ? (Mouse.current.rightButton.isPressed == true ? -DHTSF.forward : DHTSF.up) : Vector3.zero), legacyGunDirection ? (Mouse.current.rightButton.isPressed == true ? DHTSF.forward : -DHTSF.up): DHTSF.forward, out var Ray, 512f);

			GameObject NewPointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			NewPointer.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
			NewPointer.GetComponent<Renderer>().material.color = ((ControllerInputPoller.TriggerFloat(Plugin.DH == "R" ? XRNode.RightHand : XRNode.LeftHand) > 0.5f) || Mouse.current.leftButton.isPressed) ? Plugin.ColorOne : Plugin.ColorTwo;
			NewPointer.transform.localScale = smallGunPointer ? new Vector3(0.1f, 0.1f, 0.1f) : new Vector3(0.2f, 0.2f, 0.2f);
			NewPointer.transform.position = Ray.point;
			if (disableGunPointer)
			{
				NewPointer.GetComponent<Renderer>().enabled = false;
			}
			UnityEngine.Object.Destroy(NewPointer.GetComponent<Collider>());
			UnityEngine.Object.Destroy(NewPointer.GetComponent<Rigidbody>());
			UnityEngine.Object.Destroy(NewPointer, Time.deltaTime);
			if ((ControllerInputPoller.TriggerFloat(Plugin.DH == "R" ? XRNode.RightHand : XRNode.LeftHand) > 0.5f) && PlayerLock && Ray.collider.GetComponentInParent<VRRig>() != null && RigUtils.GetVRRigFromPlayer(PhotonNetwork.LocalPlayer) != Ray.collider.GetComponentInParent<VRRig>())
            {
				Locked = Ray.collider.GetComponentInParent<VRRig>();
			}
			if (Mouse.current.leftButton.isPressed && PlayerLock && Ray.collider.GetComponentInParent<VRRig>() != null && RigUtils.GetVRRigFromPlayer(PhotonNetwork.LocalPlayer) != Ray.collider.GetComponentInParent<VRRig>())
			{
				Locked = Ray.collider.GetComponentInParent<VRRig>();
			}
			if (ControllerInputPoller.TriggerFloat(Plugin.DH == "R" ? XRNode.RightHand : XRNode.LeftHand) < 0.5 && !Mouse.current.leftButton.isPressed) Locked = null;
			if (Locked != null) NewPointer.transform.position = Locked.headMesh.transform.position;
			VRRig playerrr = Locked;
			if (!Mouse.current.rightButton.isPressed)
			{
				GameObject line = new GameObject("Line");
				LineRenderer liner = line.AddComponent<LineRenderer>();
				liner.material.shader = Shader.Find("GUI/Text Shader");
				liner.startColor = Plugin.ColorOne;
				liner.endColor = Plugin.ColorTwo;
				liner.startWidth = 0.025f;
				liner.endWidth = 0.025f;
				liner.positionCount = 2;
				liner.useWorldSpace = true;
				liner.SetPosition(0, DHTSF.position);
				liner.SetPosition(1, NewPointer.transform.position);
				UnityEngine.Object.Destroy(line, Time.deltaTime);
			}

			return (Ray, NewPointer, playerrr);
		}

	}

	public class True
    {
		public static (Vector3 position, Quaternion rotation, Vector3 up, Vector3 forward, Vector3 right) TrueLeftHand()
		{
			Quaternion rot = GorillaTagger.Instance.leftHandTransform.rotation * GorillaLocomotion.Player.Instance.leftHandRotOffset;
			return (GorillaTagger.Instance.leftHandTransform.position + GorillaTagger.Instance.leftHandTransform.rotation * GorillaLocomotion.Player.Instance.leftHandOffset, rot, rot * Vector3.up, rot * Vector3.forward, rot * Vector3.right);
		}

		public static (Vector3 position, Quaternion rotation, Vector3 up, Vector3 forward, Vector3 right) TrueRightHand()
		{
			Quaternion rot = GorillaTagger.Instance.rightHandTransform.rotation * GorillaLocomotion.Player.Instance.rightHandRotOffset;
			return (GorillaTagger.Instance.rightHandTransform.position + GorillaTagger.Instance.rightHandTransform.rotation * GorillaLocomotion.Player.Instance.rightHandOffset, rot, rot * Vector3.up, rot * Vector3.forward, rot * Vector3.right);
		}
	}

	public class V3Utils
	{
		public static bool IsThisNearThat(Vector3 A, Vector3 B, float Distance)
		{
			if (Vector3.Distance(A, B) < Distance) return true;
			else return false;
		}
	}

	public class Files
    {
		public static string RemoveFileExtension(string file)
		{
			int num = 0;
			string text = "";
			string[] array = file.Split(".");
			string[] array2 = array;
			foreach (string text2 in array2)
			{
				num++;
				if (num != array.Length)
				{
					if (num > 1)
					{
						text += ".";
					}
					text += text2;
				}
			}
			return text;
		}
	}
}
