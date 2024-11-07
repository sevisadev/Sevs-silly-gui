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


namespace SevsSillyGui.Resources
{
    class ModsVar
    {
        public static bool buttonForMods;
        public static bool LeftHandTracers = true;
        public static float armLength = 0.6f;
        public static float animSpeed = 9f;
        public static float flySpeed = 10f;
        public static bool riggyrigrigadig;
        public static float laggyRigDelay = 0.5f;
        public  static Color procolor;
        public static string protype = "SnowballLeft";
        public static Transform prohand;
        public static readonly string[] ExternalProjectiles =
        {
            "SnowballLeft", "WaterBalloonLeft", "LavaRockLeft",
            "BucketGiftFunctional", "ScienceCandyLeft", "FishFoodLeft"
        };
        public static readonly string[] InternalProjectiles =
        {
            "LMACE. LEFT.", "LMAEX. LEFT.", "LMAGD. LEFT.",
            "LMAHQ. LEFT.", "LMAIE. RIGHT.", "LMAIO. LEFT."
        };
        public static Dictionary<string, SnowballThrowable> projectileDictionary;
    }
}
