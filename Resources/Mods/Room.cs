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
    class Room
    {
        public static string RandomRoomName()
        {
            string text = "";
            for (int i = 0; i < 4; i++)
            {
                text += "ABCDEFGHIJKLMNPQRSTUVWXYZ123456789".Substring(Random.Range(0, "ABCDEFGHIJKLMNPQRSTUVWXYZ123456789".Length), 1);
            }
            if (((GorillaComputer)GorillaComputer.instance).CheckAutoBanListForName(text))
            {
                return text;
            }
            return RandomRoomName();
        }
        public static void CreatePublic()
        {
            ((PhotonNetworkController)PhotonNetworkController.Instance).currentJoinTrigger = ((GorillaComputer)GorillaComputer.instance).GetJoinTriggerForZone("forest");
            Debug.Log((object)(string)typeof(PhotonNetworkController).GetField("platformTag", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(PhotonNetworkController.Instance));
            RoomConfig val = new RoomConfig();
            val.createIfMissing = true;
            val.isJoinable = true;
            val.isPublic = true;
            val.MaxPlayers = ((PhotonNetworkController)PhotonNetworkController.Instance).GetRoomSize(((PhotonNetworkController)PhotonNetworkController.Instance).currentJoinTrigger.networkZone);
            ExitGames.Client.Photon.Hashtable val2 = new ExitGames.Client.Photon.Hashtable();
            ((Dictionary<object, object>)(object)val2).Add((object)"gameMode", (object)((PhotonNetworkController)PhotonNetworkController.Instance).currentJoinTrigger.GetFullDesiredGameModeString());
            ((Dictionary<object, object>)(object)val2).Add((object)"platform", (object)(string)typeof(PhotonNetworkController).GetField("platformTag", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(PhotonNetworkController.Instance));
            val.CustomProps = val2;
            RoomConfig val3 = val;
            NetworkSystem.Instance.ConnectToRoom(RandomRoomName(), val3, -1);
        }

        public static void EUServers()
        {
            PhotonNetwork.ConnectToRegion("eu");
        }

        public static void USServers()
        {
            PhotonNetwork.ConnectToRegion("us");
        }

        public static void USWServers()
        {
            PhotonNetwork.ConnectToRegion("usw");
        }

        public static void JoinRandom()
        {
            switch (((PhotonNetworkController)PhotonNetworkController.Instance).currentJoinTrigger.networkZone)
            {
                case "forest":
                    ((GorillaTriggerBox)GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Forest, Tree Exit").GetComponent<GorillaNetworkJoinTrigger>()).OnBoxTriggered();
                    break;
                case "city":
                    ((GorillaTriggerBox)GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - City Front").GetComponent<GorillaNetworkJoinTrigger>()).OnBoxTriggered();
                    break;
                case "canyons":
                    ((GorillaTriggerBox)GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Canyon").GetComponent<GorillaNetworkJoinTrigger>()).OnBoxTriggered();
                    break;
                case "mountains":
                    ((GorillaTriggerBox)GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Mountain For Computer").GetComponent<GorillaNetworkJoinTrigger>()).OnBoxTriggered();
                    break;
                case "beach":
                    ((GorillaTriggerBox)GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Beach from Forest").GetComponent<GorillaNetworkJoinTrigger>()).OnBoxTriggered();
                    break;
                case "sky":
                    ((GorillaTriggerBox)GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Clouds").GetComponent<GorillaNetworkJoinTrigger>()).OnBoxTriggered();
                    break;
                case "basement":
                    ((GorillaTriggerBox)GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Basement For Computer").GetComponent<GorillaNetworkJoinTrigger>()).OnBoxTriggered();
                    break;
                case "metro":
                    ((GorillaTriggerBox)GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Metropolis from Computer").GetComponent<GorillaNetworkJoinTrigger>()).OnBoxTriggered();
                    break;
                case "arcade":
                    ((GorillaTriggerBox)GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - City frm Arcade").GetComponent<GorillaNetworkJoinTrigger>()).OnBoxTriggered();
                    break;
                case "rotating":
                    ((GorillaTriggerBox)GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Rotating Map").GetComponent<GorillaNetworkJoinTrigger>()).OnBoxTriggered();
                    break;
                case "bayou":
                    ((GorillaTriggerBox)GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - BayouComputer2").GetComponent<GorillaNetworkJoinTrigger>()).OnBoxTriggered();
                    break;
                case "caves":
                    ((GorillaTriggerBox)GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Cave").GetComponent<GorillaNetworkJoinTrigger>()).OnBoxTriggered();
                    break;
            }
        }
    }
}
