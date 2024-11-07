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
    class Visual
    {
        public static void CasualESP()
        {
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                VRRig vrrig = RigUtils.GetVRRigFromPlayer(player);
                if (vrrig != GorillaTagger.Instance.offlineVRRig)
                {
                    UnityEngine.Color thecolor = vrrig.playerColor;

                    GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    box.transform.position = vrrig.transform.position;
                    UnityEngine.Object.Destroy(box.GetComponent<BoxCollider>());
                    box.transform.localScale = new Vector3(0.5f, 0.5f, 0f);
                    box.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                    box.GetComponent<Renderer>().enabled = false;

                    GameObject outl = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    outl.transform.position = vrrig.transform.position + (box.transform.up * 0.25f);
                    UnityEngine.Object.Destroy(outl.GetComponent<BoxCollider>());
                    outl.transform.localScale = new Vector3(0.5f, 0.05f, 0f);
                    outl.transform.rotation = box.transform.rotation;
                    outl.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                    outl.GetComponent<Renderer>().material.color = thecolor;
                    UnityEngine.Object.Destroy(outl, Time.deltaTime);

                    outl = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    outl.transform.position = vrrig.transform.position + (box.transform.up * -0.25f);
                    UnityEngine.Object.Destroy(outl.GetComponent<BoxCollider>());
                    outl.transform.localScale = new Vector3(0.55f, 0.05f, 0f);
                    outl.transform.rotation = box.transform.rotation;
                    outl.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                    outl.GetComponent<Renderer>().material.color = thecolor;
                    UnityEngine.Object.Destroy(outl, Time.deltaTime);

                    outl = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    outl.transform.position = vrrig.transform.position + (box.transform.right * 0.25f);
                    UnityEngine.Object.Destroy(outl.GetComponent<BoxCollider>());
                    outl.transform.localScale = new Vector3(0.05f, 0.55f, 0f);
                    outl.transform.rotation = box.transform.rotation;
                    outl.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                    outl.GetComponent<Renderer>().material.color = thecolor;
                    UnityEngine.Object.Destroy(outl, Time.deltaTime);

                    outl = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    outl.transform.position = vrrig.transform.position + (box.transform.right * -0.25f);
                    UnityEngine.Object.Destroy(outl.GetComponent<BoxCollider>());
                    outl.transform.localScale = new Vector3(0.05f, 0.55f, 0f);
                    outl.transform.rotation = box.transform.rotation;
                    outl.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                    outl.GetComponent<Renderer>().material.color = thecolor;
                    UnityEngine.Object.Destroy(outl, Time.deltaTime);

                    UnityEngine.Object.Destroy(box);
                }
            }
        }

        public static void TagESP()
        {
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                VRRig vrrig = RigUtils.GetVRRigFromPlayer(player);
                if (vrrig != GorillaTagger.Instance.offlineVRRig && RigUtils.isThisPlayerTagged(player))
                {
                    Color thecolor = Plugin.Theme;
                    GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    box.transform.position = vrrig.transform.position;
                    UnityEngine.Object.Destroy(box.GetComponent<BoxCollider>());
                    box.transform.localScale = new Vector3(0.5f, 0.5f, 0f);
                    box.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                    box.GetComponent<Renderer>().enabled = false;
                    GameObject outl = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    outl.transform.position = vrrig.transform.position + (box.transform.up * 0.25f);
                    UnityEngine.Object.Destroy(outl.GetComponent<BoxCollider>());
                    outl.transform.localScale = new Vector3(0.5f, 0.05f, 0f);
                    outl.transform.rotation = box.transform.rotation;
                    outl.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                    outl.GetComponent<Renderer>().material.color = thecolor;
                    UnityEngine.Object.Destroy(outl, Time.deltaTime);
                    outl = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    outl.transform.position = vrrig.transform.position + (box.transform.up * -0.25f);
                    UnityEngine.Object.Destroy(outl.GetComponent<BoxCollider>());
                    outl.transform.localScale = new Vector3(0.55f, 0.05f, 0f);
                    outl.transform.rotation = box.transform.rotation;
                    outl.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                    outl.GetComponent<Renderer>().material.color = thecolor;
                    UnityEngine.Object.Destroy(outl, Time.deltaTime);
                    outl = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    outl.transform.position = vrrig.transform.position + (box.transform.right * 0.25f);
                    UnityEngine.Object.Destroy(outl.GetComponent<BoxCollider>());
                    outl.transform.localScale = new Vector3(0.05f, 0.55f, 0f);
                    outl.transform.rotation = box.transform.rotation;
                    outl.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                    outl.GetComponent<Renderer>().material.color = thecolor;
                    UnityEngine.Object.Destroy(outl, Time.deltaTime);
                    outl = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    outl.transform.position = vrrig.transform.position + (box.transform.right * -0.25f);
                    UnityEngine.Object.Destroy(outl.GetComponent<BoxCollider>());
                    outl.transform.localScale = new Vector3(0.05f, 0.55f, 0f);
                    outl.transform.rotation = box.transform.rotation;
                    outl.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                    outl.GetComponent<Renderer>().material.color = thecolor;
                    UnityEngine.Object.Destroy(outl, Time.deltaTime);
                    UnityEngine.Object.Destroy(box);
                }
            }
        }

        public static void untagESP()
        {
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                VRRig vrrig = RigUtils.GetVRRigFromPlayer(player);
                if (vrrig != GorillaTagger.Instance.offlineVRRig && !RigUtils.isThisPlayerTagged(player))
                {
                    Color thecolor = Plugin.Theme;
                    GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    box.transform.position = vrrig.transform.position;
                    UnityEngine.Object.Destroy(box.GetComponent<BoxCollider>());
                    box.transform.localScale = new Vector3(0.5f, 0.5f, 0f);
                    box.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                    box.GetComponent<Renderer>().enabled = false;
                    GameObject outl = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    outl.transform.position = vrrig.transform.position + (box.transform.up * 0.25f);
                    UnityEngine.Object.Destroy(outl.GetComponent<BoxCollider>());
                    outl.transform.localScale = new Vector3(0.5f, 0.05f, 0f);
                    outl.transform.rotation = box.transform.rotation;
                    outl.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                    outl.GetComponent<Renderer>().material.color = thecolor;
                    UnityEngine.Object.Destroy(outl, Time.deltaTime);
                    outl = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    outl.transform.position = vrrig.transform.position + (box.transform.up * -0.25f);
                    UnityEngine.Object.Destroy(outl.GetComponent<BoxCollider>());
                    outl.transform.localScale = new Vector3(0.55f, 0.05f, 0f);
                    outl.transform.rotation = box.transform.rotation;
                    outl.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                    outl.GetComponent<Renderer>().material.color = thecolor;
                    UnityEngine.Object.Destroy(outl, Time.deltaTime);
                    outl = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    outl.transform.position = vrrig.transform.position + (box.transform.right * 0.25f);
                    UnityEngine.Object.Destroy(outl.GetComponent<BoxCollider>());
                    outl.transform.localScale = new Vector3(0.05f, 0.55f, 0f);
                    outl.transform.rotation = box.transform.rotation;
                    outl.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                    outl.GetComponent<Renderer>().material.color = thecolor;
                    UnityEngine.Object.Destroy(outl, Time.deltaTime);
                    outl = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    outl.transform.position = vrrig.transform.position + (box.transform.right * -0.25f);
                    UnityEngine.Object.Destroy(outl.GetComponent<BoxCollider>());
                    outl.transform.localScale = new Vector3(0.05f, 0.55f, 0f);
                    outl.transform.rotation = box.transform.rotation;
                    outl.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                    outl.GetComponent<Renderer>().material.color = thecolor;
                    UnityEngine.Object.Destroy(outl, Time.deltaTime);
                    UnityEngine.Object.Destroy(box);
                }
            }
        }
        public static void CasualTracers()
        {
            float lineWidth = 0.01f;
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                UnityEngine.Color thecolor = vrrig.playerColor;
                if (vrrig != GorillaTagger.Instance.offlineVRRig)
                {
                    GameObject line = new GameObject("Line");
                    LineRenderer liner = line.AddComponent<LineRenderer>();
                    liner.startColor = thecolor; liner.endColor = thecolor; liner.startWidth = lineWidth; liner.endWidth = lineWidth; liner.positionCount = 2; liner.useWorldSpace = true;
                    liner.SetPosition(0, ModsVar.LeftHandTracers == true ? GorillaTagger.Instance.rightHandTransform.position : GorillaTagger.Instance.leftHandTransform.position);
                    liner.SetPosition(1, vrrig.transform.position);
                    liner.material.shader = Shader.Find("GUI/Text Shader");
                    UnityEngine.Object.Destroy(line, Time.deltaTime);
                }
            }
        }
        public static void TaggedTracers()
        {
            float lineWidth = 0.01f;
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                UnityEngine.Color thecolor = Plugin.Theme;
                if (vrrig != GorillaTagger.Instance.offlineVRRig && RigUtils.isThisPlayerTagged(RigUtils.GetPlayerFromVRRig(vrrig)))
                {
                    GameObject line = new GameObject("Line");
                    LineRenderer liner = line.AddComponent<LineRenderer>();
                    liner.startColor = thecolor; liner.endColor = thecolor; liner.startWidth = lineWidth; liner.endWidth = lineWidth; liner.positionCount = 2; liner.useWorldSpace = true;
                    liner.SetPosition(0, ModsVar.LeftHandTracers == true ? GorillaTagger.Instance.rightHandTransform.position : GorillaTagger.Instance.leftHandTransform.position);
                    liner.SetPosition(1, vrrig.transform.position);
                    liner.material.shader = Shader.Find("GUI/Text Shader");
                    UnityEngine.Object.Destroy(line, Time.deltaTime);
                }
            }
        }

        public static void UnTaggedTracers()
        {
            float lineWidth = 0.01f;
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                UnityEngine.Color thecolor = Plugin.Theme;
                if (vrrig != GorillaTagger.Instance.offlineVRRig && !RigUtils.isThisPlayerTagged(RigUtils.GetPlayerFromVRRig(vrrig)))
                {
                    GameObject line = new GameObject("Line");
                    LineRenderer liner = line.AddComponent<LineRenderer>();
                    liner.startColor = thecolor; liner.endColor = thecolor; liner.startWidth = lineWidth; liner.endWidth = lineWidth; liner.positionCount = 2; liner.useWorldSpace = true;
                    liner.SetPosition(0, ModsVar.LeftHandTracers == true ? GorillaTagger.Instance.rightHandTransform.position : GorillaTagger.Instance.leftHandTransform.position);
                    liner.SetPosition(1, vrrig.transform.position);
                    liner.material.shader = Shader.Find("GUI/Text Shader");
                    UnityEngine.Object.Destroy(line, Time.deltaTime);
                }
            }
        }
    }
}
