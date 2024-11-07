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
    class OP
    {
        public static void LucyChaseSelf()
        {
            HalloweenGhostChaser hgc = GameObject.Find("Environment Objects/05Maze_PersistentObjects/Halloween2024_PersistentObjects/Halloween Ghosts/Lucy/Halloween Ghost/FloatingChaseSkeleton").GetComponent<HalloweenGhostChaser>();
            if (PhotonNetwork.IsMasterClient)
            {
                hgc.currentState = HalloweenGhostChaser.ChaseState.Chasing;
                hgc.targetPlayer = NetworkSystem.Instance.LocalPlayer;
                hgc.followTarget = GorillaTagger.Instance.offlineVRRig.transform;
            }
            else Plugin.PlayErrorSound();
        }
        public static void StartMoonEvent()
        {
            GreyZoneManager gzm = GameObject.Find("Environment Objects/05Maze_PersistentObjects/Halloween2024_PersistentObjects/GreyZoneManager").GetComponent<GreyZoneManager>();
            if (PhotonNetwork.IsMasterClient)
            {
                gzm.ActivateGreyZoneAuthority();
            }
            else Plugin.PlayErrorSound();
        }

        public static void EndMoonEvent()
        {
            GreyZoneManager gzm = GameObject.Find("Environment Objects/05Maze_PersistentObjects/Halloween2024_PersistentObjects/GreyZoneManager").GetComponent<GreyZoneManager>();
            if (PhotonNetwork.IsMasterClient)
            {
                gzm.DeactivateGreyZoneAuthority();
            }
            else Plugin.PlayErrorSound();
        }

        private static float yuuu = 0f;
        public static void SpazzMoon()
        {
            GreyZoneManager gzm = GameObject.Find("Environment Objects/05Maze_PersistentObjects/Halloween2024_PersistentObjects/GreyZoneManager").GetComponent<GreyZoneManager>();
            if (PhotonNetwork.IsMasterClient)
            {
                if (Time.time > yuuu)
                {
                    yuuu = Time.time + 0.1f;
                    if (gzm.GreyZoneActive)
                    {
                        gzm.DeactivateGreyZoneAuthority();
                    }
                    else
                    {
                        gzm.ActivateGreyZoneAuthority();
                    }
                }
            }
            else Plugin.PlayErrorSound();
        }

        private static float skib = 0f;
        public static void SpazLucy()
        {
            HalloweenGhostChaser hgc = GameObject.Find("Environment Objects/05Maze_PersistentObjects/Halloween2024_PersistentObjects/Halloween Ghosts/Lucy/Halloween Ghost/FloatingChaseSkeleton").GetComponent<HalloweenGhostChaser>();
            if (PhotonNetwork.IsMasterClient)
            {
                if (Time.time > skib)
                {
                    hgc.timeGongStarted = Time.time;
                    hgc.grabTime = Time.time;
                    hgc.currentState = hgc.currentState == HalloweenGhostChaser.ChaseState.Gong ? HalloweenGhostChaser.ChaseState.Grabbing : HalloweenGhostChaser.ChaseState.Gong;
                    hgc.targetPlayer = RigUtils.GetRandomPlayer(true);
                    skib = Time.time + 0.1f;
                }
            }
            else Plugin.PlayErrorSound();
        }

        public static void SpawnBlueLucy()
        {
            HalloweenGhostChaser hgc = GameObject.Find("Environment Objects/05Maze_PersistentObjects/Halloween2024_PersistentObjects/Halloween Ghosts/Lucy/Halloween Ghost/FloatingChaseSkeleton").GetComponent<HalloweenGhostChaser>();
            if (PhotonNetwork.IsMasterClient)
            {
                hgc.timeGongStarted = Time.time;
                hgc.currentState = HalloweenGhostChaser.ChaseState.Gong;
                hgc.isSummoned = false;
            }
            else Plugin.PlayErrorSound();
        }

        public static void SpawnRedLucy()
        {
            HalloweenGhostChaser hgc = GameObject.Find("Environment Objects/05Maze_PersistentObjects/Halloween2024_PersistentObjects/Halloween Ghosts/Lucy/Halloween Ghost/FloatingChaseSkeleton").GetComponent<HalloweenGhostChaser>();
            if (PhotonNetwork.IsMasterClient)
            {
                hgc.timeGongStarted = Time.time;
                hgc.currentState = HalloweenGhostChaser.ChaseState.Gong;
                hgc.isSummoned = true;
            }
            else Plugin.PlayErrorSound();
        }

        public static void DespawnLucy()
        {
            HalloweenGhostChaser hgc = GameObject.Find("Environment Objects/05Maze_PersistentObjects/Halloween2024_PersistentObjects/Halloween Ghosts/Lucy/Halloween Ghost/FloatingChaseSkeleton").GetComponent<HalloweenGhostChaser>();
            if (PhotonNetwork.IsMasterClient)
            {
                hgc.currentState = HalloweenGhostChaser.ChaseState.Dormant;
                hgc.isSummoned = false;
            }
            else Plugin.PlayErrorSound();
        }

        public static void LucyChaseGun()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                if (ControllerInputPoller.instance.rightGrab || Mouse.current.rightButton.isPressed)
                {
                    var GunData = gun.RenderGun();
                    RaycastHit Ray = GunData.Ray;
                    GameObject NewPointer = GunData.NewPointer;

                    if (ControllerInputPoller.TriggerFloat(XRNode.RightHand) > 0.5f || Mouse.current.leftButton.isPressed)
                    {
                        HalloweenGhostChaser hgc = GameObject.Find("Environment Objects/05Maze_PersistentObjects/Halloween2024_PersistentObjects/Halloween Ghosts/Lucy/Halloween Ghost/FloatingChaseSkeleton").GetComponent<HalloweenGhostChaser>();
                        VRRig possibly = Ray.collider.GetComponentInParent<VRRig>();
                        if (possibly && possibly != GorillaTagger.Instance.offlineVRRig)
                        {
                            hgc.currentState = HalloweenGhostChaser.ChaseState.Chasing;
                            hgc.targetPlayer = RigUtils.GetPlayerFromVRRig(possibly);
                            hgc.followTarget = possibly.transform;
                        }
                    }
                }
            }
            else Plugin.PlayErrorSound();
        }



        private static float afafa = 0f;
        public static void LucyFlyGun()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                VRRig User = null;
                if (ControllerInputPoller.instance.rightGrab || Mouse.current.rightButton.isPressed)
                {
                    var GunData = gun.RenderGun();
                    RaycastHit Ray = GunData.Ray;
                    GameObject NewPointer = GunData.NewPointer;
                    HalloweenGhostChaser hgc = GameObject.Find("Environment Objects/05Maze_PersistentObjects/Halloween2024_PersistentObjects/Halloween Ghosts/Lucy/Halloween Ghost/FloatingChaseSkeleton").GetComponent<HalloweenGhostChaser>();

                    if (ControllerInputPoller.TriggerFloat(XRNode.RightHand) > 0.5f || Mouse.current.leftButton.isPressed)
                    {
                        if (hgc.IsMine)
                        {
                            VRRig possibly = Ray.collider.GetComponentInParent<VRRig>();
                            if (possibly && possibly != GorillaTagger.Instance.offlineVRRig)
                            {
                                if (Time.time > hgc.grabTime + hgc.grabDuration + 0.1f)
                                {
                                    if (possibly != null)
                                    {
                                        User = possibly;
                                    }
                                }
                            }
                        }
                    }
                    if (User != null && PhotonNetwork.IsMasterClient)
                    {
                        hgc.currentState = HalloweenGhostChaser.ChaseState.Grabbing;
                        hgc.grabTime = Time.time;
                        hgc.targetPlayer = RigUtils.GetPlayerFromVRRig(User);
                    }
                }
                else DespawnLucy();
                User = null;
            }
            else Plugin.PlayErrorSound();
        }
        public static void LucyFlySelf()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                if (ControllerInputPoller.instance.rightGrab)
                {
                    HalloweenGhostChaser hgc = GameObject.Find("Environment Objects/05Maze_PersistentObjects/Halloween2024_PersistentObjects/Halloween Ghosts/Lucy/Halloween Ghost/FloatingChaseSkeleton").GetComponent<HalloweenGhostChaser>();
                    if (PhotonNetwork.IsMasterClient)
                    {
                        hgc.currentState = HalloweenGhostChaser.ChaseState.Grabbing;
                        hgc.grabTime = Time.time;
                        hgc.targetPlayer = NetworkSystem.Instance.LocalPlayer;
                    }
                }
                else DespawnLucy();
            }
            else Plugin.PlayErrorSound();
        }

        public static void GuardianAnyGM()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                foreach (GorillaGuardianZoneManager zoneManager in GorillaGuardianZoneManager.zoneManagers)
                {
                    if (((Behaviour)zoneManager).enabled)
                    {
                        zoneManager.SetGuardian(NetworkSystem.Instance.LocalPlayer);
                    }
                }
                return;
            }
            else Plugin.PlayErrorSound();
        }
        public static void NotGuardian()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                foreach (GorillaGuardianZoneManager zoneManager in GorillaGuardianZoneManager.zoneManagers)
                {
                    if (((Behaviour)zoneManager).enabled)
                    {
                        zoneManager.SetGuardian(null);
                    }
                }
                return;
            }
            else Plugin.PlayErrorSound();
        }

        public static float kgDebounce;
        public static void GrabAll()
        {
            kgDebounce = Time.time + 0.1f;
            GorillaGuardianManager component = GameObject.Find("GT Systems/GameModeSystem/Gorilla Guardian Manager").GetComponent<GorillaGuardianManager>();
            if (component.IsPlayerGuardian(NetworkSystem.Instance.LocalPlayer))
            {
                foreach (VRRig vrrig in ((GorillaParent)GorillaParent.instance).vrrigs)
                {
                    RigUtils.GetNetworkViewFromVRRig(vrrig).SendRPC("GrabbedByPlayer", (RpcTarget)0, new object[3] { true, false, false });
                }
                return;
            }
            Plugin.PlayErrorSound();
        }

        public static void ReleaseAll()
        {
            kgDebounce = Time.time + 0.1f;
            GorillaGuardianManager component = GameObject.Find("GT Systems/GameModeSystem/Gorilla Guardian Manager").GetComponent<GorillaGuardianManager>();
            if (component.IsPlayerGuardian(NetworkSystem.Instance.LocalPlayer))
            {
                foreach (VRRig vrrig in ((GorillaParent)GorillaParent.instance).vrrigs)
                {
                    RigUtils.GetNetworkViewFromVRRig(vrrig).SendRPC("DroppedByPlayer", (RpcTarget)0, new object[1] { (object)new Vector3(0f, 0f, 0f) });
                }
                return;
            }
            Plugin.PlayErrorSound();
        }

        public static void FlingAll()
        {
            kgDebounce = Time.time + 0.1f;
            GorillaGuardianManager component = GameObject.Find("GT Systems/GameModeSystem/Gorilla Guardian Manager").GetComponent<GorillaGuardianManager>();
            if (component.IsPlayerGuardian(NetworkSystem.Instance.LocalPlayer))
            {
                foreach (VRRig vrrig in ((GorillaParent)GorillaParent.instance).vrrigs)
                {
                    if (!vrrig.isLocal)
                    {
                        RigUtils.GetNetworkViewFromVRRig(vrrig).SendRPC("GrabbedByPlayer", (RpcTarget)0, new object[3] { true, false, false });
                        RigUtils.GetNetworkViewFromVRRig(vrrig).SendRPC("DroppedByPlayer", (RpcTarget)0, new object[1] { (object)new Vector3(0f, 20f, 0f) });
                    }
                }
                return;
            }
            Plugin.PlayErrorSound();
        }

        public static float dumbdelay;
        public static TappableGuardianIdol[] archivetgi = null;
        public static TappableGuardianIdol[] GetGuardianIdols()
        {
            archivetgi = null;
            if (archivetgi == null)
            {
                archivetgi = Object.FindObjectsOfType<TappableGuardianIdol>();
            }
            return archivetgi;
        }
        public static bool shasdhakjsh = true;
        public static void AlwaysGuardian()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                foreach (GorillaGuardianZoneManager zoneManager in GorillaGuardianZoneManager.zoneManagers)
                {
                    if (((Behaviour)zoneManager).enabled)
                    {
                        zoneManager.SetGuardian(NetworkSystem.Instance.LocalPlayer);
                    }
                }
                return;
            }
            TappableGuardianIdol[] guardianIdols = GetGuardianIdols();
            foreach (TappableGuardianIdol val in guardianIdols)
            {
                if (!((Behaviour)val).enabled || !val.isActivationReady)
                {
                    continue;
                }
                GorillaGuardianManager component = GameObject.Find("GT Systems/GameModeSystem/Gorilla Guardian Manager").GetComponent<GorillaGuardianManager>();
                if (!component.IsPlayerGuardian(NetworkSystem.Instance.LocalPlayer))
                {
                    if (shasdhakjsh)
                    {
                        ((Behaviour)GorillaTagger.Instance.offlineVRRig).enabled = false;
                        ((Component)GorillaTagger.Instance.offlineVRRig).transform.position = ((Component)val).transform.position;
                        ((Component)GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget).transform.rotation = Quaternion.Euler(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
                        ((Component)GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget).transform.rotation = Quaternion.Euler(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
                        ((Component)GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget).transform.position = ((Component)GorillaTagger.Instance.offlineVRRig).transform.position + ((Component)GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget).transform.forward * 3f;
                        ((Component)GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget).transform.position = ((Component)GorillaTagger.Instance.offlineVRRig).transform.position + ((Component)GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget).transform.forward * 3f;
                        shasdhakjsh = false;
                    }
                    else
                    {
                        ((Behaviour)GorillaTagger.Instance.offlineVRRig).enabled = false;
                        ((Component)GorillaTagger.Instance.offlineVRRig).transform.position = ((Component)val).transform.position;
                        ((Component)GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget).transform.rotation = Quaternion.Euler(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
                        ((Component)GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget).transform.rotation = Quaternion.Euler(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
                        ((Component)GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget).transform.position = ((Component)GorillaTagger.Instance.offlineVRRig).transform.position;
                        ((Component)GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget).transform.position = ((Component)GorillaTagger.Instance.offlineVRRig).transform.position;
                        shasdhakjsh = true;
                    }
                    if (Time.time > dumbdelay)
                    {
                        dumbdelay = Time.time + 0.01f;
                        ((Tappable)val).OnTap(Random.Range(0.2f, 0.4f));
                    }
                }
                else
                {
                    ((Behaviour)GorillaTagger.Instance.offlineVRRig).enabled = true;
                }
            }
        }

        public static void GiveGuardian()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                if (Plugin.DH == "R" ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab || Mouse.current.rightButton.isPressed)
                {
                    var GunData = gun.RenderGun(false);
                    RaycastHit Ray = GunData.Ray;
                    GameObject NewPointer = GunData.NewPointer;
                    if (ControllerInputPoller.TriggerFloat(Plugin.DH == "R" ? XRNode.RightHand : XRNode.LeftHand) > 0.5f || Mouse.current.leftButton.isPressed)
                    {
                        foreach (GorillaGuardianZoneManager zoneManager in GorillaGuardianZoneManager.zoneManagers)
                        {
                            if (((Behaviour)zoneManager).enabled)
                            {
                                zoneManager.SetGuardian(RigUtils.GetPlayerFromVRRig(Ray.collider.GetComponentInParent<VRRig>()));
                            }
                        }
                    }
                }
            }
            else Plugin.PlayErrorSound();
        }

        public static void FlingGun()
        {
            kgDebounce = Time.time + 0.1f;
            GorillaGuardianManager component = GameObject.Find("GT Systems/GameModeSystem/Gorilla Guardian Manager").GetComponent<GorillaGuardianManager>();
            if (component.IsPlayerGuardian(NetworkSystem.Instance.LocalPlayer))
            {
                if (Plugin.DH == "R" ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab || Mouse.current.rightButton.isPressed)
                {
                    var GunData = gun.RenderGun();
                    RaycastHit Ray = GunData.Ray;
                    GameObject NewPointer = GunData.NewPointer;
                    VRRig targetrpc = GunData.playerrr;
                    if (ControllerInputPoller.TriggerFloat(Plugin.DH == "R" ? XRNode.RightHand : XRNode.LeftHand) > 0.5f || Mouse.current.leftButton.isPressed)
                    {
                        RigUtils.GetNetworkViewFromVRRig(targetrpc).SendRPC("GrabbedByPlayer", RigUtils.GetPlayerFromVRRig(targetrpc), new object[3] { true, false, false });
                        RigUtils.GetNetworkViewFromVRRig(targetrpc).SendRPC("DroppedByPlayer", RigUtils.GetPlayerFromVRRig(targetrpc), new object[1] { (object)new Vector3(0f, 20f, 0f) });
                        return;
                    }
                }
            }
            else Plugin.PlayErrorSound();
        }
        public static void ReleaseGun()
        {
            kgDebounce = Time.time + 0.1f;
            GorillaGuardianManager component = GameObject.Find("GT Systems/GameModeSystem/Gorilla Guardian Manager").GetComponent<GorillaGuardianManager>();
            if (component.IsPlayerGuardian(NetworkSystem.Instance.LocalPlayer))
            {
                if (Plugin.DH == "R" ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab || Mouse.current.rightButton.isPressed)
                {
                    var GunData = gun.RenderGun();
                    RaycastHit Ray = GunData.Ray;
                    GameObject NewPointer = GunData.NewPointer;
                    VRRig targetrpc = GunData.playerrr;
                    if (ControllerInputPoller.TriggerFloat(Plugin.DH == "R" ? XRNode.RightHand : XRNode.LeftHand) > 0.5f || Mouse.current.leftButton.isPressed)
                    {
                        RigUtils.GetNetworkViewFromVRRig(targetrpc).SendRPC("DroppedByPlayer", RigUtils.GetPlayerFromVRRig(targetrpc), new object[1] { (object)new Vector3(0f, 0, 0f) });
                        return;
                    }
                }
            }
            else Plugin.PlayErrorSound();
        }
        public static void GrabGun()
        {
            kgDebounce = Time.time + 0.1f;
            GorillaGuardianManager component = GameObject.Find("GT Systems/GameModeSystem/Gorilla Guardian Manager").GetComponent<GorillaGuardianManager>();
            if (component.IsPlayerGuardian(NetworkSystem.Instance.LocalPlayer))
            {
                if (Plugin.DH == "R" ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab || Mouse.current.rightButton.isPressed)
                {
                    var GunData = gun.RenderGun();
                    RaycastHit Ray = GunData.Ray;
                    GameObject NewPointer = GunData.NewPointer;
                    VRRig targetrpc = GunData.playerrr;
                    if (ControllerInputPoller.TriggerFloat(Plugin.DH == "R" ? XRNode.RightHand : XRNode.LeftHand) > 0.5f || Mouse.current.leftButton.isPressed)
                    {
                        RigUtils.GetNetworkViewFromVRRig(targetrpc).SendRPC("GrabbedByPlayer", RigUtils.GetPlayerFromVRRig(targetrpc), new object[3] { true, false, false });
                        return;
                    }
                }
            }
            else Plugin.PlayErrorSound();
        }
        public static void KillAll()
        {
            kgDebounce = Time.time + 0.1f;
            GorillaGuardianManager component = GameObject.Find("GT Systems/GameModeSystem/Gorilla Guardian Manager").GetComponent<GorillaGuardianManager>();
            if (component.IsPlayerGuardian(NetworkSystem.Instance.LocalPlayer))
            {
                foreach (VRRig vrrig in ((GorillaParent)GorillaParent.instance).vrrigs)
                {
                    if (!vrrig.isLocal)
                    {
                        RigUtils.GetNetworkViewFromVRRig(vrrig).SendRPC("GrabbedByPlayer", (RpcTarget)0, new object[3] { true, false, false });
                        RigUtils.GetNetworkViewFromVRRig(vrrig).SendRPC("DroppedByPlayer", (RpcTarget)0, new object[1] { (object)new Vector3(0f, 100000000000000000000f, 0f) });
                    }
                }
                return;
            }
            Plugin.PlayErrorSound();
        }
        public static void KillGun()
        {
            kgDebounce = Time.time + 0.1f;
            GorillaGuardianManager component = GameObject.Find("GT Systems/GameModeSystem/Gorilla Guardian Manager").GetComponent<GorillaGuardianManager>();
            if (component.IsPlayerGuardian(NetworkSystem.Instance.LocalPlayer))
            {
                if (Plugin.DH == "R" ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab || Mouse.current.rightButton.isPressed)
                {
                    var GunData = gun.RenderGun();
                    RaycastHit Ray = GunData.Ray;
                    GameObject NewPointer = GunData.NewPointer;
                    VRRig targetrpc = GunData.playerrr;
                    if (ControllerInputPoller.TriggerFloat(Plugin.DH == "R" ? XRNode.RightHand : XRNode.LeftHand) > 0.5f || Mouse.current.leftButton.isPressed)
                    {
                        RigUtils.GetNetworkViewFromVRRig(targetrpc).SendRPC("GrabbedByPlayer", RigUtils.GetPlayerFromVRRig(targetrpc), new object[3] { true, false, false });
                        RigUtils.GetNetworkViewFromVRRig(targetrpc).SendRPC("DroppedByPlayer", RigUtils.GetPlayerFromVRRig(targetrpc), new object[1] { (object)new Vector3(0f, 100000000000000000000f, 0f) });
                        return;
                    }
                }
            }
            else Plugin.PlayErrorSound();
        }
    }
}
