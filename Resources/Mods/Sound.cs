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
    class Sound
    {
        public static void RandomSoundSpam()
        {
            if (Plugin.DH == "R" ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab)
            {
                int soundId = UnityEngine.Random.Range(0, 259);
                if (PhotonNetwork.InRoom)
                {
                    GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlayHandTap", RpcTarget.All, new object[] {
                        soundId,
                        false,
                        999999f
                    });
                    RPCS.RPCProtection();
                }
                else
                {
                    GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(soundId, false, 999999f);
                }
            }
        }

        public static void BassSoundSpam()
        {
            if (Plugin.DH == "R" ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab)
            {
                if (PhotonNetwork.InRoom)
                {
                    GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlayHandTap", RpcTarget.All, new object[]{
                        68,
                        false,
                        999999f
                    });
                    RPCS.RPCProtection();
                }
                else
                {
                    GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(68, false, 999999f);
                }
            }
        }

        public static void MetalSoundSpam()
        {
            if (Plugin.DH == "R" ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab)
            {
                if (PhotonNetwork.InRoom)
                {
                    GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlayHandTap", RpcTarget.All, new object[]{
                        18,
                        false,
                        999999f
                    });
                }
                else
                {
                    GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(18, false, 999999f);
                }
            }
        }

        public static void WolfSoundSpam()
        {
            if (Plugin.DH == "R" ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab)
            {
                if (PhotonNetwork.InRoom)
                {
                    GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlayHandTap", RpcTarget.All, new object[]{
                        195,
                        false,
                        999999f
                    });
                    RPCS.RPCProtection();
                }
                else
                {
                    GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(195, false, 999999f);
                }
            }
        }

        public static void CatSoundSpam()
        {
            if (Plugin.DH == "R" ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab)
            {
                if (PhotonNetwork.InRoom)
                {
                    GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlayHandTap", RpcTarget.All, new object[]{
                        236,
                        false,
                        999999f
                    });
                    RPCS.RPCProtection();
                }
                else
                {
                    GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(236, false, 999999f);
                }
            }
        }

        public static void TurkeySoundSpam()
        {
            if (Plugin.DH == "R" ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab)
            {
                if (PhotonNetwork.InRoom)
                {
                    GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlayHandTap", RpcTarget.All, new object[]{
                        83,
                        false,
                        999999f
                    });
                    RPCS.RPCProtection();
                }
                else
                {
                    GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(83, false, 999999f);
                }
            }
        }

        public static void FrogSoundSpam()
        {
            if (Plugin.DH == "R" ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab)
            {
                if (PhotonNetwork.InRoom)
                {
                    GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlayHandTap", RpcTarget.All, new object[]{
                        91,
                        false,
                        999999f
                    });
                    RPCS.RPCProtection();
                }
                else
                {
                    GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(91, false, 999999f);
                }
            }
        }

        public static void BeeSoundSpam()
        {
            if (Plugin.DH == "R" ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab)
            {
                if (PhotonNetwork.InRoom)
                {
                    GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlayHandTap", RpcTarget.All, new object[]{
                        191,
                        false,
                        999999f
                    });
                    RPCS.RPCProtection();
                }
                else
                {
                    GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(191, false, 999999f);
                }
            }
        }

        public static void EarrapeSoundSpam()
        {
            if (Plugin.DH == "R" ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab)
            {
                if (PhotonNetwork.InRoom)
                {
                    GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlayHandTap", RpcTarget.All, new object[]{
                        215,
                        false,
                        999999f
                    });
                    RPCS.RPCProtection();
                }
                else
                {
                    GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(215, false, 999999f);
                }
            }
        }

        public static void DingSoundSpam()
        {
            if (Plugin.DH == "R" ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab)
            {
                if (PhotonNetwork.InRoom)
                {
                    GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlayHandTap", RpcTarget.All, new object[]{
                        244,
                        false,
                        999999f
                    });
                    RPCS.RPCProtection();
                }
                else
                {
                    GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(244, false, 999999f);
                }
            }
        }

        public static void CrystalSoundSpam()
        {
            if (Plugin.DH == "R" ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab)
            {
                int[] sounds = new int[]
                {
                    UnityEngine.Random.Range(40,54),
                    UnityEngine.Random.Range(214,221)
                };
                int soundId = sounds[UnityEngine.Random.Range(0, 1)];
                if (PhotonNetwork.InRoom)
                {
                    GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlayHandTap", RpcTarget.All, new object[]{
                        soundId,
                        false,
                        999999f
                    });
                    RPCS.RPCProtection();
                }
                else
                {
                    GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(soundId, false, 999999f);
                }
                RPCS.RPCProtection();
            }
        }

        public static void BigCrystalSoundSpam()
        {
            if (Plugin.DH == "R" ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab)
            {
                if (PhotonNetwork.InRoom)
                {
                    GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlayHandTap", RpcTarget.All, new object[]{
                        213,
                        false,
                        999999f
                    });
                    RPCS.RPCProtection();
                }
                else
                {
                    GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(213, false, 999999f);
                }
            }
        }

        public static void PanSoundSpam()
        {
            if (Plugin.DH == "R" ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab)
            {
                if (PhotonNetwork.InRoom)
                {
                    GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlayHandTap", RpcTarget.All, new object[]{
                        248,
                        false,
                        999999f
                    });
                    RPCS.RPCProtection();
                }
                else
                {
                    GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(248, false, 999999f);
                }
            }
        }

        public static void AK47SoundSpam()
        {
            if (Plugin.DH == "R" ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab)
            {
                if (PhotonNetwork.InRoom)
                {
                    GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlayHandTap", RpcTarget.All, new object[]{
                        203,
                        false,
                        999999f
                    });
                    RPCS.RPCProtection();
                }
                else
                {
                    GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(203, false, 999999f);
                }
            }
        }

        public static void SqueakSoundSpam()
        {
            if (Plugin.DH == "R" ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab)
            {
                if (PhotonNetwork.InRoom)
                {
                    GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlayHandTap", RpcTarget.All, new object[]{
                        75 + (Time.frameCount % 2),
                        false,
                        999999f
                    });
                    RPCS.RPCProtection();
                }
                else
                {
                    GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(75 + (Time.frameCount % 2), false, 999999f);
                }
            }
        }
    }
}
