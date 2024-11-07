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
    class Movement
    {
        public static void Noclip()
        {
            if (ModsVar.buttonForMods)
            {
                Collider[] array = GameObject.FindObjectsOfType<Collider>();
                foreach (Collider val in array)
                {
                    if (val.gameObject.name != "SSGUIPF")
                    {
                        ((Collider)val).enabled = false;
                    }
                }
            }
            else
            {
                Collider[] array = GameObject.FindObjectsOfType<Collider>();
                foreach (Collider val in array)
                {
                    if (val.gameObject.name != "SSGUIPF")
                    {
                        ((Collider)val).enabled = true;
                    }
                }
            }
        }
        public static void Fly()
        {
            if (ModsVar.buttonForMods)
            {
                GorillaLocomotion.Player.Instance.transform.position += GorillaTagger.Instance.headCollider.transform.forward * Time.deltaTime * (float)ModsVar.flySpeed;
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
        public static void Platforms()
        {
            if (ControllerInputPoller.instance.leftGrab)
            {
                if (!leftPlatformEnabled)
                {
                    leftPlatform = GameObject.CreatePrimitive((PrimitiveType)3);
                    leftPlatform.name = "SSGUIPF";
                    leftPlatform.GetComponent<Renderer>().material = new Material(Shader.Find("Sprites/Default"));
                    leftPlatform.transform.localScale = new Vector3(0.28f, 0.015f, 0.38f);
                    leftPlatform.transform.position = GorillaLocomotion.Player.Instance.leftControllerTransform.position + new Vector3(0f, -0.06f, 0f);
                    leftPlatform.transform.rotation = GorillaLocomotion.Player.Instance.leftControllerTransform.rotation * Quaternion.Euler(0f, 0f, -90f);
                    leftPlatformEnabled = true;
                }
                leftPlatform.GetComponent<Renderer>().material.color = Plugin.Theme;
            }
            else
            {
                if (leftPlatformEnabled)
                {
                    leftPlatform.AddComponent<Rigidbody>();
                    UnityEngine.Collider.Destroy(leftPlatform.GetComponent<Collider>());
                    UnityEngine.Object.Destroy(leftPlatform, 5f);
                    leftPlatformEnabled = false;
                    return;
                }
            }
            if (ControllerInputPoller.instance.rightGrab)
            {
                if (!rightPlatformEnabled)
                {
                    rightPlatform = GameObject.CreatePrimitive((PrimitiveType)3);
                    rightPlatform.name = "SSGUIPF";
                    rightPlatform.GetComponent<Renderer>().material = new Material(Shader.Find("Sprites/Default"));
                    rightPlatform.transform.localScale = new Vector3(0.28f, 0.015f, 0.38f);
                    rightPlatform.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position + new Vector3(0f, -0.06f, 0f);
                    rightPlatform.transform.rotation = GorillaLocomotion.Player.Instance.rightControllerTransform.rotation * Quaternion.Euler(0f, 0f, -90f);
                    rightPlatformEnabled = true;
                }
                rightPlatform.GetComponent<Renderer>().material.color = Plugin.Theme;
            }
            else
            {
                if (rightPlatformEnabled)
                {
                    rightPlatform.AddComponent<Rigidbody>();
                    Collider.Destroy(rightPlatform.GetComponent<Collider>());
                    GameObject.Destroy(rightPlatform, 5f);
                    rightPlatformEnabled = false;
                    return;
                }
            }
        }

        public static GameObject leftPlatform;

        public static bool leftPlatformEnabled = false;

        public static GameObject rightPlatform;

        public static bool rightPlatformEnabled = false;

        public bool platformLeft;

        public bool platformRight;

        public static void AutoWalk()
        {
            Vector2 joy = Plugin.DH == "L" ? SteamVR_Actions.gorillaTag_RightJoystick2DAxis.axis : SteamVR_Actions.gorillaTag_LeftJoystick2DAxis.axis;

            if (Mathf.Abs(joy.y) > 0.05f || Mathf.Abs(joy.x) > 0.05f)
            {
                GorillaTagger.Instance.leftHandTransform.position = GorillaTagger.Instance.bodyCollider.transform.position
                    + GorillaTagger.Instance.bodyCollider.transform.forward * (Mathf.Sin(Time.time * ModsVar.animSpeed) * (joy.y * ModsVar.armLength))
                    + GorillaTagger.Instance.bodyCollider.transform.right * ((Mathf.Sin(Time.time * ModsVar.animSpeed) * (joy.x * ModsVar.armLength)) - 0.2f)
                    + new Vector3(0f, -0.3f + (Mathf.Cos(Time.time * ModsVar.animSpeed) * 0.2f), 0f);

                GorillaTagger.Instance.rightHandTransform.position = GorillaTagger.Instance.bodyCollider.transform.position
                    + GorillaTagger.Instance.bodyCollider.transform.forward * (-Mathf.Sin(Time.time * ModsVar.animSpeed) * (joy.y * ModsVar.armLength))
                    + GorillaTagger.Instance.bodyCollider.transform.right * ((-Mathf.Sin(Time.time * ModsVar.animSpeed) * (joy.x * ModsVar.armLength)) + 0.2f)
                    + new Vector3(0f, -0.3f + (Mathf.Cos(Time.time * ModsVar.animSpeed) * -0.2f), 0f);
            }
            else
            {
                GorillaTagger.Instance.leftHandTransform.position = GorillaTagger.Instance.bodyCollider.transform.position
                    + GorillaTagger.Instance.bodyCollider.transform.forward * (0.2f * ModsVar.armLength)
                    + GorillaTagger.Instance.bodyCollider.transform.right * (-0.2f * ModsVar.armLength)
                    + new Vector3(0f, -0.3f, 0f);

                GorillaTagger.Instance.rightHandTransform.position = GorillaTagger.Instance.bodyCollider.transform.position
                    + GorillaTagger.Instance.bodyCollider.transform.forward * (0.2f * ModsVar.armLength)
                    + GorillaTagger.Instance.bodyCollider.transform.right * (0.2f * ModsVar.armLength)
                    + new Vector3(0f, -0.3f, 0f);
            }
        }

        public static void ForceTagFreeze()
        {
            GorillaLocomotion.Player.Instance.disableMovement = true;
        }

        public static void NoTagFreeze()
        {
            GorillaLocomotion.Player.Instance.disableMovement = false;
        }

        public static void StickLongArms()
        {
            GorillaLocomotion.Player.Instance.leftControllerTransform.transform.position = GorillaTagger.Instance.leftHandTransform.position + (GorillaTagger.Instance.leftHandTransform.forward * (1.25f - 0.917f));
            GorillaLocomotion.Player.Instance.rightControllerTransform.transform.position = GorillaTagger.Instance.rightHandTransform.position + (GorillaTagger.Instance.rightHandTransform.forward * (1.25f - 0.917f));
        }

        public static void EnableSteamLongArms()
        {
            GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
        }

        public static void DisableSteamLongArms()
        {
            GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        public static void MultipliedLongArms()
        {
            GorillaLocomotion.Player.Instance.leftControllerTransform.transform.position = GorillaTagger.Instance.headCollider.transform.position - (GorillaTagger.Instance.headCollider.transform.position - GorillaTagger.Instance.leftHandTransform.position) * 1.25f;
            GorillaLocomotion.Player.Instance.rightControllerTransform.transform.position = GorillaTagger.Instance.headCollider.transform.position - (GorillaTagger.Instance.headCollider.transform.position - GorillaTagger.Instance.rightHandTransform.position) * 1.25f;
        }

        public static void VerticalLongArms()
        {
            Vector3 lefty = GorillaTagger.Instance.headCollider.transform.position - GorillaTagger.Instance.leftHandTransform.position;
            lefty.y *= 1.25f;
            Vector3 righty = GorillaTagger.Instance.headCollider.transform.position - GorillaTagger.Instance.rightHandTransform.position;
            righty.y *= 1.25f;
            GorillaLocomotion.Player.Instance.leftControllerTransform.transform.position = GorillaTagger.Instance.headCollider.transform.position - lefty;
            GorillaLocomotion.Player.Instance.rightControllerTransform.transform.position = GorillaTagger.Instance.headCollider.transform.position - righty;
        }

        public static void HorizontalLongArms()
        {
            Vector3 lefty = GorillaTagger.Instance.headCollider.transform.position - GorillaTagger.Instance.leftHandTransform.position;
            lefty.x *= 1.25f;
            lefty.z *= 1.25f;
            Vector3 righty = GorillaTagger.Instance.headCollider.transform.position - GorillaTagger.Instance.rightHandTransform.position;
            righty.x *= 1.25f;
            righty.z *= 1.25f;
            GorillaLocomotion.Player.Instance.leftControllerTransform.transform.position = GorillaTagger.Instance.headCollider.transform.position - lefty;
            GorillaLocomotion.Player.Instance.rightControllerTransform.transform.position = GorillaTagger.Instance.headCollider.transform.position - righty;
        }

        public static void speedboost()
        {
            GorillaLocomotion.Player.Instance.jumpMultiplier = Plugin.jmulti;
        }
    }
}
