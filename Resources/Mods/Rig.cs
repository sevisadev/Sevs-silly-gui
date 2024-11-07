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
    class Rig
    {
        public static void LaggyRig()
        {
            if (Time.time > ModsVar.laggyRigDelay)
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
                ModsVar.riggyrigrigadig = true;
                ModsVar.laggyRigDelay = Time.time + 0.5f;
            }
            else
            {
                if (ModsVar.riggyrigrigadig) ModsVar.riggyrigrigadig = false;
                else
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
                }
            }
        }

        public static void UpdateRig()
        {
            if (ModsVar.buttonForMods)
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
            else GorillaTagger.Instance.offlineVRRig.enabled = false;
        }

        public static void FixRig()
        {
            GorillaTagger.Instance.offlineVRRig.enabled = true;
        }

        public static void Invismonke()
        {
            if (ModsVar.buttonForMods)
            {
                ((Behaviour)GorillaTagger.Instance.offlineVRRig).enabled = false;
                ((Component)GorillaTagger.Instance.offlineVRRig).transform.position = new Vector3(99999f, 99999f, 99999f);
                try
                {
                    ((Component)GorillaTagger.Instance.myVRRig).transform.position = new Vector3(99999f, 99999f, 99999f);
                }
                catch
                {
                }
            }
            else ((Behaviour)GorillaTagger.Instance.offlineVRRig).enabled = true;
        }
        public static void StandOn()
        {
            if (!(Plugin.DH == "R" ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab) && !Mouse.current.rightButton.isPressed)
            {
                return;
            }
            var GunData = gun.RenderGun();
            RaycastHit Ray = GunData.Ray;
            GameObject NewPointer = GunData.NewPointer;
            VRRig Player = GunData.playerrr;
            if (ControllerInputPoller.TriggerFloat(Plugin.DH == "R" ? UnityEngine.XR.XRNode.RightHand : UnityEngine.XR.XRNode.LeftHand) > 0.5f || Mouse.current.leftButton.isPressed)
            {
                ((Behaviour)GorillaTagger.Instance.offlineVRRig).enabled = false;
                ((Component)GorillaTagger.Instance.offlineVRRig).transform.position = Player.headMesh.transform.position + new Vector3(0f, 1f, 0f);
                ((Component)GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget).transform.position = Player.headMesh.transform.position;
                ((Component)GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget).transform.position = Player.headMesh.transform.position;
                try
                {
                    ((Component)GorillaTagger.Instance.offlineVRRig).transform.position = Player.headMesh.transform.position + new Vector3(0f, 1f, 0f);
                    ((Component)GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget).transform.position = Player.headMesh.transform.position;
                    ((Component)GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget).transform.position = Player.headMesh.transform.position;
                    return;
                }
                catch
                {
                    return;
                }
            }
            ((Behaviour)GorillaTagger.Instance.offlineVRRig).enabled = true;
        }
        public static void CopyGun()
        {
            if ((Plugin.DH == "R" ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab) || Mouse.current.rightButton.isPressed)
            {
                var GunData = gun.RenderGun();
                RaycastHit Ray = GunData.Ray;
                GameObject NewPointer = GunData.NewPointer;
                VRRig Player = GunData.playerrr;
                if ((ControllerInputPoller.TriggerFloat(Plugin.DH == "R" ? UnityEngine.XR.XRNode.RightHand : UnityEngine.XR.XRNode.LeftHand) > 0.5f || Mouse.current.leftButton.isPressed) && Player != null)
                {
                    ((Behaviour)GorillaTagger.Instance.offlineVRRig).enabled = false;
                    ((Component)GorillaTagger.Instance.offlineVRRig).transform.position = ((Component)Player).transform.position;
                    try
                    {
                        ((Component)GorillaTagger.Instance.myVRRig).transform.position = ((Component)Player).transform.position;
                    }
                    catch
                    {
                    }
                    ((Component)GorillaTagger.Instance.offlineVRRig).transform.rotation = ((Component)Player).transform.rotation;
                    try
                    {
                        ((Component)GorillaTagger.Instance.myVRRig).transform.rotation = ((Component)Player).transform.rotation;
                    }
                    catch
                    {
                    }
                    ((Component)GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget).transform.position = Player.leftHandTransform.position;
                    ((Component)GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget).transform.position = Player.rightHandTransform.position;
                    ((Component)GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget).transform.rotation = Player.leftHandTransform.rotation;
                    ((Component)GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget).transform.rotation = Player.rightHandTransform.rotation;
                    ((VRMap)GorillaTagger.Instance.offlineVRRig.leftIndex).calcT = ((VRMap)Player.leftIndex).calcT;
                    ((VRMap)GorillaTagger.Instance.offlineVRRig.leftMiddle).calcT = ((VRMap)Player.leftMiddle).calcT;
                    ((VRMap)GorillaTagger.Instance.offlineVRRig.leftThumb).calcT = ((VRMap)Player.leftThumb).calcT;
                    ((VRMap)GorillaTagger.Instance.offlineVRRig.leftIndex).LerpFinger(1f, false);
                    ((VRMap)GorillaTagger.Instance.offlineVRRig.leftMiddle).LerpFinger(1f, false);
                    ((VRMap)GorillaTagger.Instance.offlineVRRig.leftThumb).LerpFinger(1f, false);
                    ((VRMap)GorillaTagger.Instance.offlineVRRig.rightIndex).calcT = ((VRMap)Player.rightIndex).calcT;
                    ((VRMap)GorillaTagger.Instance.offlineVRRig.rightMiddle).calcT = ((VRMap)Player.rightMiddle).calcT;
                    ((VRMap)GorillaTagger.Instance.offlineVRRig.rightThumb).calcT = ((VRMap)Player.rightThumb).calcT;
                    ((VRMap)GorillaTagger.Instance.offlineVRRig.rightIndex).LerpFinger(1f, false);
                    ((VRMap)GorillaTagger.Instance.offlineVRRig.rightMiddle).LerpFinger(1f, false);
                    ((VRMap)GorillaTagger.Instance.offlineVRRig.rightThumb).LerpFinger(1f, false);
                    ((Component)GorillaTagger.Instance.offlineVRRig.head.rigTarget).transform.rotation = Player.headMesh.transform.rotation;
                }
                else ((Behaviour)GorillaTagger.Instance.offlineVRRig).enabled = true;
            }
            else ((Behaviour)GorillaTagger.Instance.offlineVRRig).enabled = true;
        }
        public static float waitb = 0;
        public static VRRig Playerb;
        public static void BEES()
        {
            if (!PhotonNetwork.InRoom)
            {
                Room.JoinRandom();
            }
            if (Time.time > waitb)
            {
                waitb = waitb + 1f;
                Playerb = RigUtils.GetRandomRig(false);
            }
            ((Behaviour)GorillaTagger.Instance.offlineVRRig).enabled = false;
            ((Component)GorillaTagger.Instance.offlineVRRig).transform.position = Playerb.headMesh.transform.position + new Vector3(0f, 1f, 0f);
            ((Component)GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget).transform.position = Playerb.headMesh.transform.position;
            ((Component)GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget).transform.position = Playerb.headMesh.transform.position;
            try
            {
                ((Component)GorillaTagger.Instance.offlineVRRig).transform.position = Playerb.headMesh.transform.position + new Vector3(0f, 1f, 0f);
                ((Component)GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget).transform.position = Playerb.headMesh.transform.position;
                ((Component)GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget).transform.position = Playerb.headMesh.transform.position;
                return;
            }
            catch
            {
                return;
            }
        }
        public static void ChaseGun()
        {
            if ((Plugin.DH == "R" ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab) || Mouse.current.rightButton.isPressed)
            {
                var GunData = gun.RenderGun();
                RaycastHit Ray = GunData.Ray;
                GameObject NewPointer = GunData.NewPointer;
                VRRig Player = GunData.playerrr;
                if ((ControllerInputPoller.TriggerFloat(Plugin.DH == "R" ? UnityEngine.XR.XRNode.RightHand : UnityEngine.XR.XRNode.LeftHand) > 0.5f || Mouse.current.leftButton.isPressed) && Player != null)
                {
                    float step = Plugin.ChaseSpeed * Time.deltaTime;
                    ((Behaviour)GorillaTagger.Instance.offlineVRRig).enabled = false;
                    ((Component)GorillaTagger.Instance.offlineVRRig).transform.LookAt(Player.headMesh.transform);
                    ((Component)GorillaTagger.Instance.offlineVRRig).transform.position = Vector3.MoveTowards(((Component)GorillaTagger.Instance.myVRRig).transform.position, Player.headMesh.transform.position, step);
                    try
                    {
                        ((Component)GorillaTagger.Instance.myVRRig).transform.LookAt(Player.headMesh.transform);
                        ((Component)GorillaTagger.Instance.myVRRig).transform.position = Vector3.MoveTowards(((Component)GorillaTagger.Instance.myVRRig).transform.position, Player.headMesh.transform.position, step);
                    }
                    catch
                    {
                    }
                    ((Component)GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget).transform.position = ((Component)GorillaTagger.Instance.myVRRig).transform.transform.position;
                    ((Component)GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget).transform.position = ((Component)GorillaTagger.Instance.myVRRig).transform.transform.position;
                    ((Component)GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget).transform.rotation = ((Component)GorillaTagger.Instance.myVRRig).transform.transform.rotation;
                    ((Component)GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget).transform.rotation = ((Component)GorillaTagger.Instance.myVRRig).transform.transform.rotation;
                    ((Component)GorillaTagger.Instance.offlineVRRig.head.rigTarget).transform.LookAt(Player.headMesh.transform);
                }
                else ((Behaviour)GorillaTagger.Instance.offlineVRRig).enabled = true;
            }
            else ((Behaviour)GorillaTagger.Instance.offlineVRRig).enabled = true;
        }
    }
}
