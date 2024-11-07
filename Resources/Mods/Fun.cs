using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BepInEx;
using UnityEngine.XR;
using Photon.Pun;
using Utilla;
using System.Collections;
using System;
using Photon.Realtime;
using SevsSillyGui;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine.Networking;
using SevsSillyGui.Resources;
using GorillaLocomotion;
using BepInEx.Configuration;
using ExitGames.Client.Photon;
using GorillaLocomotion.Gameplay;
using GorillaNetworking;
using GorillaTagScripts;
using HarmonyLib;
using Photon.Voice.Unity;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
using GorillaGameModes;
using SevsSillyGui.Patches;
using UnityEngine;
using Valve.VR;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;
using Fusion;
using Behaviour = UnityEngine.Behaviour;

namespace SevsSillyGui.Resources.Mods
{
    class Fun
    {
        public static void FixHead()
        {
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.x = 0f;
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.y = 0f;
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.z = 0f;
        }
        public static void UpsideDownHead()
        {
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.z = 180f;
        }

        public static void BrokenNeck()
        {
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.z = 90f;
        }

        public static void BackwardsHead()
        {
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.y = 180f;
        }

		public static void ConfuseGun()
		{
			if (Plugin.DH == "R" ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab || Mouse.current.rightButton.isPressed)
			{
				var GunData = gun.RenderGun();
				RaycastHit Ray = GunData.Ray;
				GameObject NewPointer = GunData.NewPointer;
				VRRig targetrpc = GunData.playerrr;
				if (ControllerInputPoller.TriggerFloat(Plugin.DH == "R" ? XRNode.RightHand : XRNode.LeftHand) > 0.5f || Mouse.current.leftButton.isPressed)
				{
					((Behaviour)GorillaTagger.Instance.offlineVRRig).enabled = false;
					((Component)GorillaTagger.Instance.offlineVRRig).transform.position = ((Component)targetrpc).transform.position - new Vector3(0f, 1f, 0f);
					try
					{
						((Component)GorillaTagger.Instance.myVRRig).transform.position = ((Component)targetrpc).transform.position - new Vector3(0f, 1f, 0f);
					}
					catch
					{
					}
					if (Time.time > Plugin.splashDelllllat)
					{
						GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlaySplashEffect", RigUtils.GetPlayerFromVRRig(targetrpc), new object[6]
						{
						((Component)targetrpc).transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)),
						Quaternion.Euler(new Vector3((float)Random.Range(0, 360), (float)Random.Range(0, 360), (float)Random.Range(0, 360))),
						4f,
						100f,
						true,
						false
						});
						RPCS.RPCProtection();
						Plugin.splashDelllllat = Time.time + 0.1f;
					}
				}
				else ((Behaviour)GorillaTagger.Instance.offlineVRRig).enabled = true;
			}
		}
	}
}
