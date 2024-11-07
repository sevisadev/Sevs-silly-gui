using BepInEx;
using ExitGames.Client.Photon;
using GorillaLocomotion.Gameplay;
using GorillaNetworking;
using GorillaTagScripts;
using GorillaTagScripts.ObstacleCourse;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.XR;

namespace SevsSillyGui.Patches
{
    class RPCS
    {
        public static void RPCProtection()
        {
            try
            {
                GorillaNot.instance.rpcErrorMax = int.MaxValue;
                GorillaNot.instance.rpcCallLimit = int.MaxValue;
                GorillaNot.instance.logErrorMax = int.MaxValue;

                PhotonNetwork.MaxResendsBeforeDisconnect = int.MaxValue;
                PhotonNetwork.QuickResends = int.MaxValue;

                //PhotonNetwork.OpCleanRpcBuffer(GorillaTagger.Instance.myVRRig.GetView);
                PhotonNetwork.OpCleanActorRpcBuffer(PhotonNetwork.LocalPlayer.ActorNumber);
                PhotonNetwork.SendAllOutgoingCommands();

                GorillaNot.instance.OnPlayerLeftRoom(PhotonNetwork.LocalPlayer);
            }
            catch { UnityEngine.Debug.Log("RPC protection failed, are you in a lobby?"); }
        }
    }
}
