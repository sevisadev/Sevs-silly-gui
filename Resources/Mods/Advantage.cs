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
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace SevsSillyGui.Resources.Mods
{
    class Advantage
    {
        public static void TagAura()
        {
			foreach (VRRig vrrig in ((GorillaParent)GorillaParent.instance).vrrigs)
			{
				Vector3 position = vrrig.headMesh.transform.position;
				Vector3 position2 = GorillaTagger.Instance.offlineVRRig.head.rigTarget.position;
				float num = Vector3.Distance(position, position2);
				if (RigUtils.PlayerIsTagged(GorillaTagger.Instance.offlineVRRig) && !RigUtils.PlayerIsTagged(vrrig) && !GorillaLocomotion.Player.Instance.disableMovement && num < 4f)
				{
					if (Plugin.DH == "L")
					{
                        GorillaLocomotion.Player.Instance.rightControllerTransform.position = position;
					}
					else
					{
                        GorillaLocomotion.Player.Instance.leftControllerTransform.position = position;
					}
				}
			}
		}
		public static void NoTagOnJoin()
		{
			PlayerPrefs.SetString("tutorial", "nope");
			PlayerPrefs.SetString("didTutorial", "nope");
			Hashtable val = new Hashtable();
			((Dictionary<object, object>)(object)val).Add((object)"didTutorial", (object)false);
			PhotonNetwork.LocalPlayer.SetCustomProperties(val, (Hashtable)null, (WebFlags)null);
			PlayerPrefs.Save();
		}
		public static void TagSelf()
		{
			if (!RigUtils.PlayerIsTagged(GorillaTagger.Instance.offlineVRRig))
            {
				foreach (VRRig vrrig in ((GorillaParent)GorillaParent.instance).vrrigs)
				{
					if (RigUtils.PlayerIsTagged(vrrig))
					{
						((Behaviour)GorillaTagger.Instance.offlineVRRig).enabled = false;
						((Component)GorillaTagger.Instance.offlineVRRig).transform.position = vrrig.rightHandTransform.position;
						((Component)GorillaTagger.Instance.myVRRig).transform.position = vrrig.rightHandTransform.position;
					}
				}
			}
			else ((Behaviour)GorillaTagger.Instance.offlineVRRig).enabled = true;
		}
	}
}
