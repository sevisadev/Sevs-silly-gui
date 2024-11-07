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
    class Projectile
    {
        private static GorillaVelocityEstimator velocityEstimatorInstance;
        private static float projectileDebounce;
        private static float projectileDebounceDuration = 0.15f;
        public static void FireProjectile(string name, Vector3 spawnPos, Vector3 initialVel, Color color, bool noDelay = false) 
        {
            if (velocityEstimatorInstance == null)
                velocityEstimatorInstance = new GameObject("Blank GVE").AddComponent<GorillaVelocityEstimator>();


            SnowballThrowable RetrieveProjectile(string projectileName)
            {
                if (ModsVar.projectileDictionary == null)
                {
                    ModsVar.projectileDictionary = new Dictionary<string, SnowballThrowable>();
                    foreach (var projectile in UnityEngine.Object.FindObjectsOfType<SnowballThrowable>(true))
                    {
                        string GetHierarchyPath(Transform currentTransform)
                        {
                            var path = new System.Text.StringBuilder();
                            while (currentTransform != null)
                            {
                                path.Insert(0, currentTransform.name + "/");
                                currentTransform = currentTransform.parent;
                            }
                            return path.ToString().TrimEnd('/');
                        }

                        if (GetHierarchyPath(projectile.transform.parent).ToLower().Contains("player objects/local vrrig/local gorilla player/"))
                        {
                            ModsVar.projectileDictionary[projectile.gameObject.name] = projectile;
                        }
                    }

                    if (ModsVar.projectileDictionary.Count < 14)
                        ModsVar.projectileDictionary = null;
                }

                if (ModsVar.projectileDictionary != null && ModsVar.projectileDictionary.TryGetValue(projectileName, out var retrievedProjectile))
                    return retrievedProjectile;

                return null;
            }

            var projectile = RetrieveProjectile(ModsVar.InternalProjectiles[Array.IndexOf(ModsVar.ExternalProjectiles, name)]);

            if (!projectile.gameObject.activeSelf)
            {
                projectile.EnableSnowballLocal(true);
                projectile.velocityEstimator = velocityEstimatorInstance;
                projectile.transform.SetPositionAndRotation(GorillaTagger.Instance.leftHandTransform.position, GorillaTagger.Instance.leftHandTransform.rotation);
            }

            if (Time.time > projectileDebounce)
            {
                try
                {
                    var rb = GorillaTagger.Instance.GetComponent<Rigidbody>();
                    Vector3 originalVel = rb.velocity;

                    projectile.randomizeColor = true;
                    projectile.transform.position = spawnPos;
                    rb.velocity = initialVel;

                    GorillaTagger.Instance.offlineVRRig.SetThrowableProjectileColor(true, color);
                    typeof(SnowballThrowable).GetMethod("LaunchSnowball", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(projectile, null);
                    rb.velocity = originalVel;
                    projectile.randomizeColor = false;
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }

                if (!noDelay) projectileDebounce = Time.time + projectileDebounceDuration + 0.05f;
            }
        }
        public static void ProShooter()
        {
            if (Plugin.ProHandLeft == true ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab)
            {
                FireProjectile(ModsVar.protype, ModsVar.prohand.position, ModsVar.prohand.up * 12, ModsVar.procolor);
            }
        }
        public static void ProShooterInsta()
        {
            if (Plugin.ProHandLeft == true ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab)
            {
                FireProjectile(ModsVar.protype, ModsVar.prohand.position, ModsVar.prohand.up * 100000000, ModsVar.procolor);
            }
        }
        public static void ProDropper()
        {
            if (Plugin.ProHandLeft == true ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab)
            {
                FireProjectile(ModsVar.protype, ModsVar.prohand.position, Vector3.zero, ModsVar.procolor);
            }
        }
        public static void Rain()
        {
            FireProjectile(ModsVar.protype, GorillaTagger.Instance.headCollider.transform.position + new Vector3(UnityEngine.Random.Range(-2f, 2f), 3f, UnityEngine.Random.Range(-2f, 2f)), Vector3.up, ModsVar.procolor);
        }
        public static void GiveClosestSpammer()
        {
            string type = ModsVar.protype;
            if (RigUtils.GetClosestVRRig() is null)
            {
                return;
            }
            else
            {
                if (RigUtils.GetClosestVRRig().rightMiddle.calcT > .5f)
                {
                    if (!V3Utils.IsThisNearThat(RigUtils.FindClosestVRRig().transform.position, GorillaTagger.Instance.offlineVRRig.headMesh.transform.position, 3f))
                    {
                        FireProjectile(type, RigUtils.FindClosestVRRig().rightHandTransform.position, RigUtils.FindClosestVRRig().rightHandTransform.up * 6, ModsVar.procolor);
                    }
                }
                else if (RigUtils.GetClosestVRRig().leftMiddle.calcT > .5f)
                {
                    if (!V3Utils.IsThisNearThat(RigUtils.FindClosestVRRig().transform.position, GorillaTagger.Instance.offlineVRRig.headMesh.transform.position, 3f))
                    {
                        FireProjectile(type, RigUtils.FindClosestVRRig().leftHandTransform.position, RigUtils.FindClosestVRRig().leftHandTransform.up * 6, ModsVar.procolor);
                    }
                }
            }
        }
        public static void GiveClosestDropper()
        {
            string type = ModsVar.protype;
            if (RigUtils.GetClosestVRRig() is null)
            {
                return;
            }
            else
            {
                if (RigUtils.GetClosestVRRig().rightMiddle.calcT > .5f)
                {
                    if (!V3Utils.IsThisNearThat(RigUtils.FindClosestVRRig().transform.position, GorillaTagger.Instance.offlineVRRig.headMesh.transform.position, 3f))
                    {
                        FireProjectile(type, RigUtils.FindClosestVRRig().rightHandTransform.position, Vector3.zero, ModsVar.procolor);
                    }
                }
                else if (RigUtils.GetClosestVRRig().leftMiddle.calcT > .5f)
                {
                    if (!V3Utils.IsThisNearThat(RigUtils.FindClosestVRRig().transform.position, GorillaTagger.Instance.offlineVRRig.headMesh.transform.position, 3f))
                    {
                        FireProjectile(type, RigUtils.FindClosestVRRig().leftHandTransform.position, Vector3.zero, ModsVar.procolor);
                    }
                }
            }
        }
        public static void BCSELF()
        {
            FireProjectile(ModsVar.protype, GorillaTagger.Instance.headCollider.transform.position + new Vector3(0, 1f, 0), new Vector3(0, 100, 0), RigUtils.GetVRRigFromPlayer(PhotonNetwork.LocalPlayer).playerColor);
        }
        public static void ProTela()
        {
            if (Plugin.ProHandLeft == true ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab)
            {
                FireProjectile(ModsVar.protype, ModsVar.prohand.position + ModsVar.prohand.up * 3, Vector3.zero, ModsVar.procolor);
            }
        }
        public static void ProOrbit()
        {
            FireProjectile(ModsVar.protype, GorillaTagger.Instance.headCollider.transform.position + new Vector3(MathF.Cos((float)Time.frameCount / 30), 1f, MathF.Sin((float)Time.frameCount / 30)), Vector3.zero, ModsVar.procolor);
        }
        public static void GiveShoot()
        {
            if (Plugin.DH == "R" ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab || Mouse.current.rightButton.isPressed)
            {
                var GunData = gun.RenderGun();
                RaycastHit Ray = GunData.Ray;
                VRRig Player = GunData.playerrr;
                GameObject NewPointer = GunData.NewPointer;
                if (ControllerInputPoller.TriggerFloat(Plugin.DH == "R" ? XRNode.RightHand : XRNode.LeftHand) > 0.5f || Mouse.current.leftButton.isPressed)
                {
                    if (Player.rightMiddle.calcT > .5f || Player.leftMiddle.calcT > .5f)
                    {
                        var target = (Player.rightMiddle.calcT > .5f == true ? Player.rightHandTransform : Player.leftHandTransform);
                        FireProjectile(ModsVar.protype, target.position, target.up * 6, ModsVar.procolor);
                        if (!V3Utils.IsThisNearThat(target.transform.position, GorillaTagger.Instance.offlineVRRig.headMesh.transform.position, 3f))
                        {
                            GorillaTagger.Instance.offlineVRRig.enabled = false;
                            GorillaTagger.Instance.offlineVRRig.transform.position = Player.transform.position - new Vector3(0, 1.2f, 0);
                        }
                        else
                        {
                            if (!GorillaTagger.Instance.offlineVRRig.enabled)
                            {
                                GorillaTagger.Instance.offlineVRRig.enabled = true;
                            }
                        }
                    }
                    else
                    {
                        if (!GorillaTagger.Instance.offlineVRRig.enabled)
                        {
                            GorillaTagger.Instance.offlineVRRig.enabled = true;
                        }
                    }
                }
            }
        }
        public static void ProOrbitHand()
        {
            float radius = 0.15f;
            float devide = 12;
            if (ControllerInputPoller.instance.rightGrab)
            {
                FireProjectile(ModsVar.protype, GorillaTagger.Instance.rightHandTransform.position + new Vector3(MathF.Cos((float)Time.frameCount / devide) * radius, 0.1f, MathF.Sin((float)Time.frameCount / devide) * radius), Vector3.zero, ModsVar.procolor);
            }
            if (ControllerInputPoller.instance.leftGrab)
            {
                FireProjectile(ModsVar.protype, GorillaTagger.Instance.leftHandTransform.position + new Vector3(MathF.Cos((float)Time.frameCount / devide) * radius, 0.1f, MathF.Sin((float)Time.frameCount / devide) * radius), Vector3.zero, ModsVar.procolor);
            }
        }
        public static void GiveSpawn()
        {
            if (Plugin.DH == "R" ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab || Mouse.current.rightButton.isPressed)
            {
                var GunData = gun.RenderGun();
                RaycastHit Ray = GunData.Ray;
                GameObject NewPointer = GunData.NewPointer;
                VRRig Player = GunData.playerrr;
                if (ControllerInputPoller.TriggerFloat(Plugin.DH == "R" ? XRNode.RightHand : XRNode.LeftHand) > 0.5f || Mouse.current.leftButton.isPressed)
                {
                    if (Player.rightMiddle.calcT > .5f || Player.leftMiddle.calcT > .5f)
                    {
                        var target = (Player.rightMiddle.calcT > .5f == true ? Player.rightHandTransform : Player.leftHandTransform);
                        FireProjectile(ModsVar.protype, target.position, Vector3.zero, ModsVar.procolor);
                        if (!V3Utils.IsThisNearThat(target.transform.position, GorillaTagger.Instance.offlineVRRig.headMesh.transform.position, 3f))
                        {
                            GorillaTagger.Instance.offlineVRRig.enabled = false;
                            GorillaTagger.Instance.offlineVRRig.transform.position = Player.transform.position - new Vector3(0, 1.2f, 0);
                        }
                        else
                        {
                            if (!GorillaTagger.Instance.offlineVRRig.enabled)
                            {
                                GorillaTagger.Instance.offlineVRRig.enabled = true;
                            }
                        }
                    }
                    else
                    {
                        if (!GorillaTagger.Instance.offlineVRRig.enabled)
                        {
                            GorillaTagger.Instance.offlineVRRig.enabled = true;
                        }
                    }
                }
            }
        }
        public static void GiveHandOrbit()
        {
            float radius = 0.15f;
            float devide = 12;
            if (Plugin.DH == "R" ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab || Mouse.current.rightButton.isPressed)
            {
                var GunData = gun.RenderGun();
                RaycastHit Ray = GunData.Ray;
                GameObject NewPointer = GunData.NewPointer;
                VRRig Player = GunData.playerrr;
                if (ControllerInputPoller.TriggerFloat(Plugin.DH == "R" ? XRNode.RightHand : XRNode.LeftHand) > 0.5f || Mouse.current.leftButton.isPressed)
                {
                    if (Player.rightMiddle.calcT > .5f || Player.leftMiddle.calcT > .5f)
                    {
                        var target = (Player.rightMiddle.calcT > .5f == true ? Player.rightHandTransform : Player.leftHandTransform);
                        FireProjectile(ModsVar.protype, target.position + new Vector3(MathF.Cos((float)Time.frameCount / devide) * radius, 0.1f, MathF.Sin((float)Time.frameCount / devide) * radius), Vector3.zero, ModsVar.procolor);
                        if (!V3Utils.IsThisNearThat(target.transform.position, GorillaTagger.Instance.offlineVRRig.headMesh.transform.position, 3f))
                        {
                            GorillaTagger.Instance.offlineVRRig.enabled = false;
                            GorillaTagger.Instance.offlineVRRig.transform.position = Player.transform.position - new Vector3(0, 1.2f, 0);
                        }
                        else
                        {
                            if (!GorillaTagger.Instance.offlineVRRig.enabled)
                            {
                                GorillaTagger.Instance.offlineVRRig.enabled = true;
                            }
                        }
                    }
                    else
                    {
                        if (!GorillaTagger.Instance.offlineVRRig.enabled)
                        {
                            GorillaTagger.Instance.offlineVRRig.enabled = true;
                        }
                    }
                }
            }
        }
        public static void GiveOrbit()
        {
            if (Plugin.DH == "R" ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab || Mouse.current.rightButton.isPressed)
            {
                var GunData = gun.RenderGun();
                VRRig Player = GunData.playerrr;
                RaycastHit Ray = GunData.Ray;
                GameObject NewPointer = GunData.NewPointer;
                if (ControllerInputPoller.TriggerFloat(Plugin.DH == "R" ? XRNode.RightHand : XRNode.LeftHand) > 0.5f || Mouse.current.leftButton.isPressed)
                {
                    var target = (Player.rightMiddle.calcT > .5f == true ? Player.rightHandTransform : Player.leftHandTransform);
                    FireProjectile(ModsVar.protype, Player.headMesh.transform.position + new Vector3(MathF.Cos((float)Time.frameCount / 30), 1f, MathF.Sin((float)Time.frameCount / 30)), Vector3.zero, ModsVar.procolor);
                    if (!V3Utils.IsThisNearThat(target.transform.position, GorillaTagger.Instance.offlineVRRig.headMesh.transform.position, 3f))
                    {
                        GorillaTagger.Instance.offlineVRRig.enabled = false;
                        GorillaTagger.Instance.offlineVRRig.transform.position = Player.transform.position - new Vector3(0, 1.2f, 0);
                    }
                    else
                    {
                        if (!GorillaTagger.Instance.offlineVRRig.enabled)
                        {
                            GorillaTagger.Instance.offlineVRRig.enabled = true;
                        }
                    }
                }
                else
                {
                    if (!GorillaTagger.Instance.offlineVRRig.enabled)
                    {
                        GorillaTagger.Instance.offlineVRRig.enabled = true;
                    }
                }
            }
        }
        public static void GiveTela()
        {
            if (Plugin.DH == "R" ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab || Mouse.current.rightButton.isPressed)
            {
                var GunData = gun.RenderGun();
                VRRig Player = GunData.playerrr;
                RaycastHit Ray = GunData.Ray;
                GameObject NewPointer = GunData.NewPointer;
                if (ControllerInputPoller.TriggerFloat(Plugin.DH == "R" ? XRNode.RightHand : XRNode.LeftHand) > 0.5f || Mouse.current.leftButton.isPressed)
                {
                    if (Player.rightMiddle.calcT > .5f || Player.leftMiddle.calcT > .5f)
                    {
                        var target = (Player.rightMiddle.calcT > .5f == true ? Player.rightHandTransform : Player.leftHandTransform);
                        FireProjectile(ModsVar.protype, target.position + target.up * 3, Vector3.zero, ModsVar.procolor);
                        if (!V3Utils.IsThisNearThat(target.transform.position, GorillaTagger.Instance.offlineVRRig.headMesh.transform.position, 3f))
                        {
                            GorillaTagger.Instance.offlineVRRig.enabled = false;
                            GorillaTagger.Instance.offlineVRRig.transform.position = Player.transform.position - new Vector3(0, 1.2f, 0);
                        }
                        else
                        {
                            if (!GorillaTagger.Instance.offlineVRRig.enabled)
                            {
                                GorillaTagger.Instance.offlineVRRig.enabled = true;
                            }
                        }
                    }
                    else
                    {
                        if (!GorillaTagger.Instance.offlineVRRig.enabled)
                        {
                            GorillaTagger.Instance.offlineVRRig.enabled = true;
                        }
                    }
                }
            }
        }
        public static void ConfuseGun()
        {
            if (Plugin.DH == "R" ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab || Mouse.current.rightButton.isPressed)
            {
                var GunData = gun.RenderGun();
                RaycastHit Ray = GunData.Ray;
                GameObject NewPointer = GunData.NewPointer;
                VRRig Player = GunData.playerrr;
                if (ControllerInputPoller.TriggerFloat(Plugin.DH == "R" ? XRNode.RightHand : XRNode.LeftHand) > 0.5f || Mouse.current.leftButton.isPressed)
                {
                    var target = (Player.rightMiddle.calcT > .5f == true ? Player.rightHandTransform : Player.leftHandTransform);
                    FireProjectile("WaterBalloonLeft", Player.headMesh.transform.position + Player.headMesh.transform.forward * 0.5f, Vector3.zero, Player.playerColor);
                }
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.transform.position = Player.transform.position - new Vector3(0, 1.2f, 0);
            }
            else
            {
                if (!GorillaTagger.Instance.offlineVRRig.enabled)
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = true;
                }
            }
        }
        public static void GiveBecon()
        {
            if (Plugin.DH == "R" ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab || Mouse.current.rightButton.isPressed)
            {
                var GunData = gun.RenderGun();
                RaycastHit Ray = GunData.Ray;
                GameObject NewPointer = GunData.NewPointer;
                VRRig Player = GunData.playerrr;
                if (ControllerInputPoller.TriggerFloat(Plugin.DH == "R" ? XRNode.RightHand : XRNode.LeftHand) > 0.5f || Mouse.current.leftButton.isPressed)
                {
                    FireProjectile(ModsVar.protype, Player.headMesh.transform.position + new Vector3(0, 1f, 0), new Vector3(0, 100, 0), Player.playerColor);
                    if (!V3Utils.IsThisNearThat(Player.headMesh.transform.position, GorillaTagger.Instance.offlineVRRig.headMesh.transform.position, 3f))
                    {
                        GorillaTagger.Instance.offlineVRRig.enabled = false;
                        GorillaTagger.Instance.offlineVRRig.transform.position = Player.transform.position - new Vector3(0, 1.2f, 0);
                    }
                    else
                    {
                        if (!GorillaTagger.Instance.offlineVRRig.enabled)
                        {
                            GorillaTagger.Instance.offlineVRRig.enabled = true;
                        }
                    }
                }
            }
        }
        public static void GiveRain()
        {
            if (Plugin.DH == "R" ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab || Mouse.current.rightButton.isPressed)
            {
                var GunData = gun.RenderGun();
                RaycastHit Ray = GunData.Ray;
                GameObject NewPointer = GunData.NewPointer;
                VRRig Player = GunData.playerrr;
                if (ControllerInputPoller.TriggerFloat(Plugin.DH == "R" ? XRNode.RightHand : XRNode.LeftHand) > 0.5f || Mouse.current.leftButton.isPressed)
                {
                    FireProjectile(ModsVar.protype, Player.headMesh.transform.position + new Vector3(UnityEngine.Random.Range(-2f, 2f), 3f, UnityEngine.Random.Range(-2f, 2f)), Vector3.up, ModsVar.procolor);
                    if (!V3Utils.IsThisNearThat(Player.headMesh.transform.position, GorillaTagger.Instance.offlineVRRig.headMesh.transform.position, 3f))
                    {
                        GorillaTagger.Instance.offlineVRRig.enabled = false;
                        GorillaTagger.Instance.offlineVRRig.transform.position = Player.transform.position - new Vector3(0, 1.2f, 0);
                    }
                    else
                    {
                        if (!GorillaTagger.Instance.offlineVRRig.enabled)
                        {
                            GorillaTagger.Instance.offlineVRRig.enabled = true;
                        }
                    }
                }
            }
        }
        public static void GiveIntsShoot()
        {
            if (Plugin.DH == "R" ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab || Mouse.current.rightButton.isPressed)
            {
                var GunData = gun.RenderGun();
                RaycastHit Ray = GunData.Ray;
                GameObject NewPointer = GunData.NewPointer;
                VRRig Player = GunData.playerrr;
                if (ControllerInputPoller.TriggerFloat(Plugin.DH == "R" ? XRNode.RightHand : XRNode.LeftHand) > 0.5f || Mouse.current.leftButton.isPressed)
                {
                    if (Player.rightMiddle.calcT > .5f || Player.leftMiddle.calcT > .5f)
                    {
                        var target = (Player.rightMiddle.calcT > .5f == true ? Player.rightHandTransform : Player.leftHandTransform);
                        FireProjectile(ModsVar.protype, target.position, target.up * 100000000, ModsVar.procolor);
                        if (!V3Utils.IsThisNearThat(target.transform.position, GorillaTagger.Instance.offlineVRRig.headMesh.transform.position, 3f))
                        {
                            GorillaTagger.Instance.offlineVRRig.enabled = false;
                            GorillaTagger.Instance.offlineVRRig.transform.position = Player.transform.position - new Vector3(0, 1.2f, 0);
                        }
                        else
                        {
                            if (!GorillaTagger.Instance.offlineVRRig.enabled)
                            {
                                GorillaTagger.Instance.offlineVRRig.enabled = true;
                            }
                        }
                    }
                    else
                    {
                        if (!GorillaTagger.Instance.offlineVRRig.enabled)
                        {
                            GorillaTagger.Instance.offlineVRRig.enabled = true;
                        }
                    }
                }
            }
        }
    }
}
