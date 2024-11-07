using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BepInEx;
using Photon.Pun;
using Utilla;
using System;
using Photon.Realtime;
using SevsSillyGui.Resources;
using ExitGames.Client.Photon;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using TagEffects;
using Valve.VR;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UnityEngine.Networking;
using OVR;
using Photon.Voice.Unity;
using PlayFab.ClientModels;
using SevsSillyGui.Patches;
using UnityEngine.InputSystem;
using SevsSillyGui.Resources.Mods;
using BepInEx;
using ExitGames.Client.Photon;
using GorillaLocomotion.Gameplay;
using GorillaNetworking;
using GorillaTagScripts;
using GorillaTagScripts.ObstacleCourse;
using HarmonyLib;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.XR;

namespace SevsSillyGui.Startup
{
    class Boards
    {
        public static List<TMPro.TextMeshPro> udTMP = new List<TMPro.TextMeshPro> { };
        public static bool used;
        public static void BoardsL(Material mat)
        {
            try
            {
                bool found = false;
                int indexOfThatThing = 0;
                for (int i = 0; i < GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom").transform.childCount; i++)
                {
                    GameObject v = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom").transform.GetChild(i).gameObject;
                    if (v.name.Contains("forestatlas"))
                    {
                        indexOfThatThing++;
                        if (indexOfThatThing == 2)
                        {
                            found = true;
                            if (!used)
                            {
                                Plugin.DefaultBC = v.GetComponent<Renderer>().material;
                                used = true;
                            }
                            v.GetComponent<Renderer>().material = mat;
                        }
                    }
                }

                bool found2 = false;
                indexOfThatThing = 0;
                for (int i = 0; i < GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest").transform.childCount; i++)
                {
                    GameObject v = GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest").transform.GetChild(i).gameObject;
                    if (v.name.Contains("forestatlas"))
                    {
                        indexOfThatThing++;
                        if (indexOfThatThing == 4)
                        {
                            UnityEngine.Debug.Log("Board found");
                            found2 = true;
                            v.GetComponent<Renderer>().material = mat;
                        }
                    }
                }
                if (found && found2)
                {
                    GameObject vr = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomBoundaryStones/BoundaryStoneSet_Forest/wallmonitorforestbg");
                    if (vr != null)
                    {
                        vr.GetComponent<Renderer>().material = mat;
                    }

                    foreach (GorillaNetworkJoinTrigger v in (List<GorillaNetworkJoinTrigger>)typeof(PhotonNetworkController).GetField("allJoinTriggers", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(PhotonNetworkController.Instance))
                    {
                        try
                        {
                            JoinTriggerUI ui = (JoinTriggerUI)typeof(GorillaNetworkJoinTrigger).GetField("ui", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(v);
                            JoinTriggerUITemplate temp = (JoinTriggerUITemplate)typeof(JoinTriggerUI).GetField("template", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ui);
                            temp.ScreenBG_AbandonPartyAndSoloJoin = mat;
                            temp.ScreenBG_AlreadyInRoom = mat;
                            temp.ScreenBG_ChangingGameModeSoloJoin = mat;
                            temp.ScreenBG_Error = mat;
                            temp.ScreenBG_InPrivateRoom = mat;
                            temp.ScreenBG_LeaveRoomAndGroupJoin = mat;
                            temp.ScreenBG_LeaveRoomAndSoloJoin = mat;
                            temp.ScreenBG_NotConnectedSoloJoin = mat;

                            TextMeshPro text = (TextMeshPro)Traverse.Create(ui).Field("screenText").GetValue();
                            if (!udTMP.Contains(text))
                            {
                                udTMP.Add(text);
                            }
                        }
                        catch { }
                    }
                    PhotonNetworkController.Instance.UpdateTriggerScreens();

                    string[] objectsWithTMPro = new string[]
                    {
                                    "Environment Objects/LocalObjects_Prefab/TreeRoom/CodeOfConduct",
                                    "Environment Objects/LocalObjects_Prefab/TreeRoom/COC Text",
                                    "Environment Objects/LocalObjects_Prefab/TreeRoom/Data",
                                    "Environment Objects/LocalObjects_Prefab/TreeRoom/FunctionSelect"
                    };
                    foreach (string lol in objectsWithTMPro)
                    {
                        GameObject obj = GameObject.Find(lol);
                        if (obj != null)
                        {
                            TextMeshPro text = obj.GetComponent<TextMeshPro>();
                            if (!udTMP.Contains(text))
                            {
                                udTMP.Add(text);
                            }
                        }
                        else
                        {
                            UnityEngine.Debug.Log("Could not find " + lol);
                        }
                    }

                    Transform targettransform = GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/ForestScoreboardAnchor/GorillaScoreBoard").transform;
                    for (int i = 0; i < targettransform.transform.childCount; i++)
                    {
                        GameObject v = targettransform.GetChild(i).gameObject;
                        if ((v.name.Contains("Board Text") || v.name.Contains("Scoreboard_OfflineText")) && v.activeSelf)
                        {
                            TextMeshPro text = v.GetComponent<TextMeshPro>();
                            if (!udTMP.Contains(text))
                            {
                                udTMP.Add(text);
                            }
                        }
                    }
                }
                GameObject computerMonitor = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/GorillaComputerObject/ComputerUI/monitor/monitorScreen");
                if (computerMonitor != null)
                {
                    computerMonitor.GetComponent<Renderer>().material = mat;
                }
            }
            catch { }
        }
    }
}
