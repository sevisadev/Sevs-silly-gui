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
    class Safty
    {
        public static void AntiReport()
        {
            float threshold = 0.5f;
            try
            {
                foreach (GorillaPlayerScoreboardLine line in GorillaScoreboardTotalUpdater.allScoreboardLines)
                {
                    if (line.linePlayer == NetworkSystem.Instance.LocalPlayer)
                    {
                        Transform report = line.reportButton.gameObject.transform;
                        foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                        {
                            if (vrrig != GorillaTagger.Instance.offlineVRRig)
                            {
                                float D1 = Vector3.Distance(vrrig.rightHandTransform.position, report.position);
                                float D2 = Vector3.Distance(vrrig.leftHandTransform.position, report.position);

                                if (D1 < threshold || D2 < threshold)
                                {
                                    if (PhotonNetwork.CurrentRoom.IsVisible && !PhotonNetwork.CurrentRoom.CustomProperties.ToString().Contains("MODDED"))
                                    {
                                        PhotonNetwork.Disconnect();
                                        RPCS.RPCProtection();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }

        public static void NoFinger()
        {
            ((ControllerInputPoller)ControllerInputPoller.instance).leftControllerGripFloat = 0f;
            ((ControllerInputPoller)ControllerInputPoller.instance).rightControllerGripFloat = 0f;
            ((ControllerInputPoller)ControllerInputPoller.instance).leftControllerIndexFloat = 0f;
            ((ControllerInputPoller)ControllerInputPoller.instance).rightControllerIndexFloat = 0f;
            ((ControllerInputPoller)ControllerInputPoller.instance).leftControllerPrimaryButton = false;
            ((ControllerInputPoller)ControllerInputPoller.instance).leftControllerSecondaryButton = false;
            ((ControllerInputPoller)ControllerInputPoller.instance).rightControllerPrimaryButton = false;
            ((ControllerInputPoller)ControllerInputPoller.instance).rightControllerSecondaryButton = false;
            ((ControllerInputPoller)ControllerInputPoller.instance).leftControllerPrimaryButtonTouch = false;
            ((ControllerInputPoller)ControllerInputPoller.instance).leftControllerSecondaryButtonTouch = false;
            ((ControllerInputPoller)ControllerInputPoller.instance).rightControllerPrimaryButtonTouch = false;
            ((ControllerInputPoller)ControllerInputPoller.instance).rightControllerSecondaryButtonTouch = false;
        }

        public static bool offsethaschanged;
        private static Vector3 lastrighthandoffset = Vector3.zero;
        private static Vector3 lastlefthandoffset = Vector3.zero;
        public static void FakeOculusMenu()
        {
            if (ModsVar.buttonForMods)
            {
                NoFinger();
                GorillaLocomotion.Player.Instance.leftControllerTransform.rotation = ((Component)Camera.main).transform.rotation * Quaternion.Euler(0f, 100f, 0f);
                GorillaLocomotion.Player.Instance.rightControllerTransform.rotation = ((Component)Camera.main).transform.rotation * Quaternion.Euler(0f, -100f, 0f);
                if (!offsethaschanged)
                {
                    lastlefthandoffset = GorillaLocomotion.Player.Instance.leftHandOffset;
                    lastrighthandoffset = GorillaLocomotion.Player.Instance.rightHandOffset;
                    offsethaschanged = true;
                }
                GorillaLocomotion.Player.Instance.leftHandOffset = new Vector3(-0.02f, -0.052f, -0.056f);
                GorillaLocomotion.Player.Instance.rightHandOffset = new Vector3(0.02f, -0.052f, -0.056f);
            }
            else if (offsethaschanged)
            {
                GorillaLocomotion.Player.Instance.leftHandOffset = lastlefthandoffset;
                GorillaLocomotion.Player.Instance.rightHandOffset = lastrighthandoffset;
                offsethaschanged = false;
            }
            GorillaLocomotion.Player.Instance.inOverlay = ModsVar.buttonForMods;
        }
        public static void SpoofColor()
        {
            Color[] colors = new Color[]
            {
                Color.cyan,
                Color.yellow,
                Color.blue,
                Color.gray,
                Color.black,
                Color.white,
                Color.magenta,
                Color.yellow,
                Color.green,
                new Color(1f, 0.5f, 1f, 255f),
                new Color(0f, 0.5f, 0f, 255f),
                new Color32(113, 0, 198, 255),
                new Color32(170, 198, 170, 255),
                new Color32(170, 170, 170, 255),
                new Color32(227, 170, 85, 255),
                new Color32(0, 226, 255, 255)
            };
            ChangeColor(colors[UnityEngine.Random.Range(0, colors.Length - 1)]);
        }
        public static void ChangeIdentity()
        {
            SpoofName();
            SpoofColor();
        }
        public static void SpoofName()
        {
            string[] names = new string[]
            {
                "II",
                "PBBV",
                "YOURMOM",
                "MINIGAMES",
                "SKIBIDI",
                "SIGMA",
                "FROSTY",
                "FRISH",
                "LITTLETIMMY",
                "SILLYBILLY",
                "TIMMY",
                "RIZZ",
                "RIXX",
                "JMANCURLY",
                "VMT",
                "ELLIOT",
                "POLAR",
                "3CLIPCE",
                "GORILLAVR",
                "GORILLAVRGT",
                "GORILLAGTVR",
                "GORILLAGT",
                "DUCKY",
                "EDDIE",
                "EDDY",
                "RAKZZ",
                "CASEOH",
                "SKETCH",
                "WATERMELON",
                "CRAZY",
                "MONK",
                "MONKE",
                "MONKI",
                "MONKEY",
                "MONKIY",
                "GORILL",
                "GOORILA",
                "GORILLA",
                "REDBERRY",
                "FOX",
                "RUFUS",
                "TTT",
                "TTTPIG",
                "PPPTIG",
                "K9",
                "BTC"
            };

            ChangeName(names[UnityEngine.Random.Range(0, names.Length - 1)]);
        }
        public static bool lastinlobbyagain;
        public static void ChangeIdentityOnDisconnect()
        {
            if (!PhotonNetwork.InRoom && lastinlobbyagain)
            {
                ChangeIdentity();
            }
            lastinlobbyagain = PhotonNetwork.InRoom;
        }
        public static void ChangeName(string PlayerName)
        {
            try
            {
                GorillaComputer.instance.currentName = PlayerName;
                PhotonNetwork.LocalPlayer.NickName = PlayerName;
                GorillaComputer.instance.offlineVRRigNametagText.text = PlayerName;
                GorillaComputer.instance.savedName = PlayerName;
                PlayerPrefs.SetString("playerName", PlayerName);
                PlayerPrefs.Save();
            }
            catch (Exception exception)
            {
                UnityEngine.Debug.LogError(string.Format("iiMenu <b>NAME ERROR</b> {1} - {0}", exception.Message, exception.StackTrace));
            }
        }
        public static void ChangeColor(Color color)
        {
            PlayerPrefs.SetFloat("redValue", Mathf.Clamp(color.r, 0f, 1f));
            PlayerPrefs.SetFloat("greenValue", Mathf.Clamp(color.g, 0f, 1f));
            PlayerPrefs.SetFloat("blueValue", Mathf.Clamp(color.b, 0f, 1f));

            GorillaTagger.Instance.UpdateColor(color.r, color.g, color.b);
            PlayerPrefs.Save();

            if (PhotonNetwork.InRoom && GorillaComputer.instance.friendJoinCollider.playerIDsCurrentlyTouching.Contains(PhotonNetwork.LocalPlayer.UserId))
            {
                GorillaTagger.Instance.myVRRig.SendRPC("RPC_InitializeNoobMaterial", RpcTarget.All, new object[] { color.r, color.g, color.b });
                RPCS.RPCProtection();
            }
        }
    }
}
