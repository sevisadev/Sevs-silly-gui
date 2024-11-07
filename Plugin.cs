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
using SevsSillyGui.Startup;

namespace SevsSillyGui
{
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin("com.sev.gorillatag.SevsSillyGui", "Sevs Silly Gui", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {

        public static float bgav;
        string Checkerrrrrrrr = "yes";
        public static int Section = 1;
        public static bool IsShowingM = true;
        TextMeshPro MainText;
        TextMeshPro ModText;
        TextMeshPro SetText;
        public static List<string> Items = new List<string>();
        public static List<string> Names = new List<string>();
        public static List<bool> ItemsOn = new List<bool>();
        public static List<int> ItemArea = new List<int>();
        public static List<bool> ItemOnTime = new List<bool>();
        public static List<Action> ItemDO = new List<Action>();
        public static List<Action> ItemDOn = new List<Action>();
        public static List<bool> OffMe = new List<bool>();
        public static int Page = 0;
        float deltaTime = 0.0f;
        private float fps = 0.0f;
        bool LP;
        bool RP;
        public static string DH = "R";
        public static TextMeshPro LeadText;
        public static Color Theme;
        bool KeyI;
        bool KeyP;
        AudioSource Audio;
        AudioSource sbpb;
        public static Color ColorOne;
        public static Color ColorTwo;
        public float fadeDuration = 2f;
        float times;
        bool KeyO;
        public static int MySpace;
        public static AudioClip Error;
        public static List<string> AdminIds = new List<string> { "950F966CB2712FC6", "F7A2BD044F36668A", "2EF6C096A14D09F0" };
        public static bool IsAdmin;

        public static float TOON = 0f;
        public static float TOTW = 0f;
        public static float TOTH = 0f;
        public static float TTON = 0f;
        public static float TTTW = 0f;
        public static float TTTH = 0f;

        public static string SelectedId;
        public static bool IsSelecting;

        public static Material ThemeMat;

        public static List<int> DontSave = new List<int> { 1, 999, 2, 3, 9 };

        public static string MC = "New";
        public static bool NewControls = true;

        public static bool up;
        public static bool down;
        public static bool click;

        public static bool prohand;
        public static float PR = 0.8f;
        public static float PG = 0.8f;
        public static float PB = 0.8f;
        public static bool ProHandLeft = false;
        public static int ProTypeNum = 0;
        public static bool ProRainbow = true;

        public static bool Leftstepwater;
        public static bool Rightstepwater;

        public static float splashDelllllat;

        public static bool AUTOCLEARRPCS;
        public static bool CustomBoards = true;
        public static Material DefaultBC;

        public static bool RGBMENU;

        public static readonly string[] ProNames =
        {
            "Snowball", "Water balloon", "Lava rock",
            "Gifts", "Mentos", "Shit"
        };

        public static bool MenuFall = true;
        public static bool FallGravity = false;
        public static List<TextMeshPro> MenuDe = new List<TextMeshPro>();

        public static float jmulti = 1.1f;
        public static int spc = 1;
        public static float[] jmultiamounts = new float[] { 0.5f, 1.1f, 2f, 10f };
        public static string[] speedNames = new string[] { "Slow", "Normal", "Fast", "Super fast" };

        public static float ChaseSpeed = 3;


        public static void ANext(bool Sound = true)
        {
            Page++;
            ItemsOn[2] = false;
            if (LeadText.gameObject.activeSelf)
            {
                LeadText.gameObject.SetActive(false);
                ABack(false);
            }
            if (Sound)
            {
                GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(66, false, 1f);
                GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(66, true, 1f);
            }
            int IT = -1;
            foreach (var item in Items)
            {
                IT++;
            }
            if (Page > IT)
            {
                Page = 0;
            }
            if (ItemArea[Page] != Section)
            {
                ANext(false);
            }
            if (Names[Page] == "Admin mods" && !IsAdmin)
            {
                ANext(false);
            }
            if (!IsShowingM)
            {
                IsShowingM = true;
                Page = 0;
            }
        }

        public static void ABack(bool Sound = true)
        {
            Page--;
            ItemsOn[2] = false;
            if (LeadText.gameObject.activeSelf)
            {
                LeadText.gameObject.SetActive(false);
                ANext(false);
            }
            if (Sound)
            {
                GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(66, false, 1f);
                GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(66, true, 1f);
            }
            int IT = -1;
            foreach (var item in Items)
            {
                IT++;
            }
            if (Page < 0)
            {
                Page = IT;
            }
            if (ItemArea[Page] != Section)
            {
                ABack(false);
            }
            if (Names[Page] == "Admin mods" && !IsAdmin)
            {
                ABack(false);
            }
            if (!IsShowingM)
            {
                IsShowingM = true;
                Page = 0;
            }
        }

        void ASelect()
        {
            if (IsShowingM)
            {
                ItemsOn[Page] = !ItemsOn[Page];
                GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(67, false, 1f);
                GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(67, true, 1f);
            }
        }

        void Start()
        {
            PhotonNetwork.NetworkingClient.EventReceived += EventReceived;
            Utilla.Events.GameInitialized += OnGameInitialized;
        }



        void OnGameInitialized(object sender, EventArgs e)
        {
            ((Behaviour)GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/motdtext").gameObject.GetComponent<PlayFabTitleDataTextDisplay>()).enabled = false;
            PrefLoader.Load();
            DH = PlayerPrefs.GetString("DH");
            float HeghtUp = 0.075f;
            GameObject Cam = GameObject.Find("Player Objects/Player VR Controller/GorillaPlayer");
            Audio = Cam.AddComponent<AudioSource>();
            Audio.clip = FileLoader.LoadSoundFromURL("https://github.com/sevisadev/Sevs-silly-gui/raw/main/Resourses/LobbyMusic.mp3", "BackgroundAudio.mp3");
            Error = FileLoader.LoadSoundFromURL("https://github.com/sevisadev/Sevs-silly-gui/raw/main/Resourses/Microsoft%20Windows%20XP%20Error.mp3", "ErrorSound.mp3");
            Audio.volume = 0f;
            Audio.loop = true;
            Audio.bypassListenerEffects = true;
            Audio.bypassReverbZones = true;
            Audio.bypassEffects = true;
            Audio.Play();
            sbpb = GameObject.Find("Player Objects/Player VR Controller/GorillaPlayer/TurnParent/Main Camera/DebugCanvas").AddComponent<AudioSource>();
            sbpb.volume = 1f;
            sbpb.loop = false;
            sbpb.bypassListenerEffects = true;
            sbpb.bypassReverbZones = true;
            sbpb.bypassEffects = true;
            ThemeMat = new Material(Shader.Find("GorillaTag/UberShader"));
            GameObject debugCanvas = GameObject.Find("Player Objects/Player VR Controller/GorillaPlayer/TurnParent/Main Camera/DebugCanvas");
            if (debugCanvas != null)
            {
                debugCanvas.SetActive(true);

                // Get template TextMeshPro object
                GameObject textTemplate = GameObject.Find("Player Objects/Player VR Controller/GorillaPlayer/TurnParent/Main Camera/DebugCanvas/Text (TMP)");
                if (textTemplate != null)
                {
                    textTemplate.GetComponent<TextMeshPro>().text = "";  // Clear text on template
                    textTemplate.SetActive(false);

                    // Create new text objects for display
                    GameObject TextHost = Instantiate(textTemplate, debugCanvas.transform);
                    GameObject LeadHost = Instantiate(textTemplate, debugCanvas.transform);
                    GameObject ModHost = Instantiate(textTemplate, debugCanvas.transform);
                    GameObject SetHost = Instantiate(textTemplate, debugCanvas.transform);

                    MainText = TextHost.GetComponent<TextMeshPro>();
                    LeadText = LeadHost.GetComponent<TextMeshPro>();
                    ModText = ModHost.GetComponent<TextMeshPro>();
                    SetText = SetHost.GetComponent<TextMeshPro>();

                    if (MainText != null && LeadText != null)
                    {
                        // Set text properties and initial positions
                        TextHost.SetActive(true);
                        TextHost.transform.position = textTemplate.transform.position + new Vector3(0, HeghtUp, 0);
                        TextHost.transform.rotation = textTemplate.transform.rotation;
                        TextHost.transform.localScale = textTemplate.transform.localScale;

                        LeadHost.SetActive(true);
                        LeadHost.transform.position = textTemplate.transform.position + new Vector3(0, HeghtUp, 0);
                        LeadHost.transform.rotation = textTemplate.transform.rotation;
                        LeadHost.transform.localScale = textTemplate.transform.localScale;

                        ModHost.SetActive(false);
                        ModHost.transform.position = textTemplate.transform.position + new Vector3(0, HeghtUp, 0);
                        ModHost.transform.rotation = textTemplate.transform.rotation;
                        ModHost.transform.localScale = textTemplate.transform.localScale;

                        SetHost.SetActive(false);
                        SetHost.transform.position = textTemplate.transform.position + new Vector3(0, HeghtUp, 0);
                        SetHost.transform.rotation = textTemplate.transform.rotation;
                        SetHost.transform.localScale = textTemplate.transform.localScale;

                        MainText.color = Theme;

                        Buttons();


                        InvokeRepeating("Upfps", 0.0f, 1f);  // Start FPS update
                        InvokeRepeating("autoclear", 0.0f, 45f);
                        ANext(false);
                        MainText.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                        LeadText.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                        ModText.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                        SetText.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                        GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/motdtext").gameObject.GetComponent<TextMeshPro>().text = $"<color=white>Thank you for using Sevs Silly GUI. The version your on has {Names.Count - 8} mods. I hope you enjoy using my menu and have fun! If you are banned, it is not my fault.";
                        GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/motd (1)").gameObject.GetComponent<TextMeshPro>().text = "Sevs silly gui";
                    }
                }
            }
        }


        void Buttons()
        {
            // 999 = Admin mods
            // 1 = Main
            // 2 = Settings
            // 3 = Mods
            // 4 = OP mods
            // 5 = Safty mods
            // 6 = Movement mods
            // 7 = Visual mods
            // 8 = Room mods
            // 9 = Projectile mods
            // 10 = Rig mods
            // 11 = Fun mods
            // 12 = Advantage mods
            // 13 = sound mods



            // 2 = Settings choose
            // 100 = Menu Settings
            // 101 Movement settings
            // 102 Visual settings
            // 103 = Projectile settings
            // 104 = Rig settings
            // 105 = Safty settings

            // 400 = Soundboard

            // Defualt
            AddButt("!Disconnect!", false, 1, delegate { PhotonNetwork.Disconnect(); }, delegate { }, true);
            AddButt("JOIN DISCORD!!!", false, 1, delegate { Process.Start("https://discord.gg/Fjwnh4ygPZ"); }, delegate { }, true);
            AddButt("<color=blue>Player info</color>", false, 1, delegate { Stats(); }, delegate { });
            AddButt("Mods", false, 1, delegate {
                Section = 3;
                ANext(false);
            }, delegate { }, true);
            AddButt("Settings", false, 1, delegate {
                Section = 2;
                ANext(false);
            }, delegate { }, true);
            AddButt("!Minimize GUI!", false, 1, delegate {
                if (MenuFall)
                {
                    var mff = Instantiate(MainText.gameObject, MainText.gameObject.transform);
                    mff.transform.parent = null;
                    mff.transform.position = MainText.transform.position;
                    mff.transform.rotation = MainText.transform.rotation;
                    var rigmf = mff.AddComponent<Rigidbody>();
                    rigmf.velocity = GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity;
                    if (!FallGravity)
                    {
                        rigmf.useGravity = false;
                    }
                    MenuDe.Add(mff.GetComponent<TextMeshPro>());
                    GameObject.Destroy(mff, 15f);
                }
                IsShowingM = false;
            }, delegate { }, true, true);

            // Admin mods
            AddButt("Admin mods", false, 1, delegate {
                Section = 999;
                ANext(false);
            }, delegate { }, true);
            AddButt("Back", false, 999, delegate { Gotomain(); }, delegate { }, true, true);
            AddButt("User ESP", false, 999, delegate {
                Snitch();
            }, delegate { });
            AddButt("Load user ESP", false, 999, delegate {
                RaiseEvent("Snitch");
            }, delegate { }, true);
            AddButt("Freeze users", false, 999, delegate {
                RaiseEvent("Freeze");
            }, delegate { });
            AddButt("Fling users", false, 999, delegate {
                RaiseEvent("Fling");
            }, delegate { }, true);
            AddButt("Copy buttons", false, 999, delegate {
                int ItemOnSec = 0;
                string itemsg = "# Sevs silly gui mods\n";
                foreach (bool item in ItemsOn)
                {
                    if (ItemArea[ItemOnSec] != 999 && Items[ItemOnSec] != "Admin mods")
                    {
                        itemsg += Names[ItemOnSec] + "\n";
                    }
                    ItemOnSec++;
                }
                copy(itemsg);
            }, delegate {
            }, true);

            // Settings
            AddButt("Back", false, 2, delegate { Gotomain(); }, delegate { }, true, true);
            AddButt("Copy user id", false, 2, delegate {
                copy(PhotonNetwork.LocalPlayer.UserId);
            }, delegate { }, true);
            AddButt("Menu settings", false, 2, delegate {
                Section = 100;
                ANext(false);
            }, delegate { }, true);
            AddButt("Movement settings", false, 2, delegate {
                Section = 101;
                ANext(false);
            }, delegate { }, true);
            AddButt("Visual settings", false, 2, delegate {
                Section = 102;
                ANext(false);
            }, delegate { }, true);
            AddButt("Projectile settings", false, 2, delegate {
                Section = 103;
                ANext(false);
            }, delegate { }, true);
            AddButt("Rig settings", false, 2, delegate {
                Section = 104;
                ANext(false);
            }, delegate { }, true);
            AddButt("Safty settings", false, 2, delegate {
                Section = 105;
                ANext(false);
            }, delegate { }, true);

            // safty settings
            AddButt("Back", false, 105, delegate {
                Section = 2;
                ANext(false);
            }, delegate { }, true, true);
            AddButt($"<color=blue>Auto RPC clearing: {(AUTOCLEARRPCS == false ? "Off" : "On")}", false, 105, delegate {
                AUTOCLEARRPCS = !AUTOCLEARRPCS;
                Items[MySpace] = $"<color=blue><color=blue>Auto RPC clearing: {(AUTOCLEARRPCS == false ? "Off" : "On")}</color>";
                PlayerPrefs.SetInt("AUTOCLEARRPCS", AUTOCLEARRPCS == true ? 0 : 1);
            }, delegate { }, true);

            // Projectile settings
            AddButt("Back", false, 103, delegate {
                Section = 2;
                ANext(false);
            }, delegate { }, true, true);
            AddButt($"<color=red>Color R: {PR * 10}", false, 103, delegate {
                PR = PR + 0.1f;
                PR = (float)Math.Round((double)PR, 1);
                if (PR > 0.9f)
                {
                    PR = 0.0f;
                }
                Items[MySpace] = $"<color=red>Color R: {PR * 10}</color>";
                PlayerPrefs.SetFloat("PR", PR);
            }, delegate { }, true);
            AddButt($"<color=green>Color G: {PG * 10}", false, 103, delegate {
                PG = PG + 0.1f;
                PG = (float)Math.Round((double)PG, 1);
                if (PG > 0.9f)
                {
                    PG = 0.0f;
                }
                Items[MySpace] = $"<color=green>Color G: {PG * 10}</color>";
                PlayerPrefs.SetFloat("PG", PG);
            }, delegate { }, true);
            AddButt($"<color=blue>Color B: {PB * 10}", false, 103, delegate {
                PB = PB + 0.1f;
                PB = (float)Math.Round((double)PB, 1);
                if (PB > 0.9f)
                {
                    PB = 0.0f;
                }
                Items[MySpace] = $"<color=blue>Color B: {PB * 10}</color>";
                PlayerPrefs.SetFloat("PB", PB);
            }, delegate { }, true);
            AddButt($"<color=blue>Projectile hand: {(ProHandLeft == true ? "Right" : "Left")}", false, 103, delegate {
                ProHandLeft = !ProHandLeft;
                Items[MySpace] = $"<color=blue>Projectile hand: {(ProHandLeft == true ? "Right" : "Left")}</color>";
                PlayerPrefs.SetInt("ProHandLeft", ProHandLeft == true ? 0 : 1);
            }, delegate { }, true);
            AddButt($"<color=blue>Projectile type: {ProNames[ProTypeNum]} ", false, 103, delegate {
                ProTypeNum++;
                if (ProTypeNum > 5) ProTypeNum = 0;
                Items[MySpace] = $"<color=blue>Projectile type: {ProNames[ProTypeNum]}</color>";
                ModsVar.protype = ModsVar.ExternalProjectiles[ProTypeNum];
                PlayerPrefs.SetInt("ProTypesS", ProTypeNum);
            }, delegate { }, true);
            AddButt($"<color=blue>Rainbow projectiles: {(ProRainbow == false ? "Off" : "On")}", false, 103, delegate {
                ProRainbow = !ProRainbow;
                Items[MySpace] = $"<color=blue>Rainbow projectiles: {(ProRainbow == false ? "Off" : "On")}";
                PlayerPrefs.SetInt("ProRainbow", ProRainbow == true ? 0 : 1);
            }, delegate { }, true);

            // Menu settings
            AddButt("Back", false, 100, delegate {
                Section = 2;
                ANext(false);
            }, delegate { }, true, true);
            AddButt($"<color=blue>Dominant hand: {DH}", false, 100, delegate {
                DH = DH == "R" ? "L" : "R";
                Items[MySpace] = $"<color=blue>Dominant hand: {DH}</color>";
                PlayerPrefs.SetString("DH", DH);
            }, delegate { }, true);
            AddButt($"<color=blue>Menu controls: {MC}", false, 100, delegate {
                MC = MC == "New" ? "Old" : "New";
                NewControls = !NewControls;
                Items[MySpace] = $"<color=blue><color=blue>Menu controls: {MC}</color>";
                PlayerPrefs.SetInt("CONT", NewControls == true ? 0 : 1);
            }, delegate { }, true);
            AddButt($"<color=blue>Menu drop: {(MenuFall == false ? "Off" : "On")}", false, 100, delegate {
                MenuFall = !MenuFall;
                Items[MySpace] = $"<color=blue><color=blue>Menu gravity: {(MenuFall == false ? "Off" : "On")}</color>";
                PlayerPrefs.SetInt("MenuFall", MenuFall == true ? 0 : 1);
            }, delegate { }, true);
            AddButt($"<color=blue>Menu gravity: {(FallGravity == false ? "Off" : "On")}", false, 100, delegate {
                FallGravity = !FallGravity;
                Items[MySpace] = $"<color=blue><color=blue>Menu gravity: {(FallGravity == false ? "Off" : "On")}</color>";
                PlayerPrefs.SetInt("FallGravity", FallGravity == true ? 0 : 1);
            }, delegate { }, true);
            AddButt($"<color=blue>Custom boards: {(CustomBoards == false ? "Off" : "On")}", false, 100, delegate {
                CustomBoards = !CustomBoards;
                Items[MySpace] = $"<color=blue><color=blue>Custom boards: {(CustomBoards == false ? "Off" : "On")}</color>";
                PlayerPrefs.SetInt("CustomBoards", CustomBoards == true ? 0 : 1);
            }, delegate { }, true);
            AddButt($"<color=red>Color 1 R: {TOON * 10}", false, 100, delegate {
                TOON = TOON + 0.1f;
                TOON = (float)Math.Round((double)TOON, 1);
                if (TOON > 0.9f)
                {
                    TOON = 0.0f;
                }
                Items[MySpace] = $"<color=red>Color 1 R: {TOON * 10}</color>";
                PlayerPrefs.SetFloat("TOON", TOON);
            }, delegate { }, true);
            AddButt($"<color=green>Color 1 G: {TOTW * 10}", false, 100, delegate {
                TOTW = TOTW + 0.1f;
                TOTW = (float)Math.Round((double)TOTW, 1);
                if (TOTW > 0.9f)
                {
                    TOTW = 0.0f;
                }
                Items[MySpace] = $"<color=green>Color 1 G: {TOTW * 10}</color>";
                PlayerPrefs.SetFloat("TOTW", TOTW);
            }, delegate { }, true);
            AddButt($"<color=blue>Color 1 B: {TOTH * 10}", false, 100, delegate {
                TOTH = TOTH + 0.1f;
                TOTH = (float)Math.Round((double)TOTH, 1);
                if (TOTH > 0.9f)
                {
                    TOTH = 0.0f;
                }
                Items[MySpace] = $"<color=blue>Color 1 B: {TOTH * 10}</color>";
                PlayerPrefs.SetFloat("TOTH", TOTH);
            }, delegate { }, true);
            AddButt($"<color=red>Color 2 R: {TTON * 10}", false, 100, delegate {
                TTON = TTON + 0.1f;
                TTON = (float)Math.Round((double)TTON, 1);
                if (TTON > 0.9f)
                {
                    TTON = 0.0f;
                }
                Items[MySpace] = $"<color=red>Color 2 R: {TTON * 10}</color>";
                PlayerPrefs.SetFloat("TTON", TTON);
            }, delegate { }, true);
            AddButt($"<color=green>Color 2 G: {TTTW * 10}", false, 100, delegate {
                TTTW = TTTW + 0.1f;
                TTTW = (float)Math.Round((double)TTTW, 1);
                if (TTTW > 0.9f)
                {
                    TTTW = 0.0f;
                }
                Items[MySpace] = $"<color=green>Color 2 G: {TTTW * 10}</color>";
                PlayerPrefs.SetFloat("TTTW", TTTW);
            }, delegate { }, true);
            AddButt($"<color=blue>Color 2 B: {TTTH * 10}", false, 100, delegate {
                TTTH = TTTH + 0.1f;
                TTTH = (float)Math.Round((double)TTTH, 1);
                if (TTTH > 0.9f)
                {
                    TTTH = 0.0f;
                }
                Items[MySpace] = $"<color=blue>Color 2 B: {TTTH * 10}</color>";
                PlayerPrefs.SetFloat("TTTH", TTTH);
            }, delegate { }, true);
            AddButt($"<color=blue>RGB menu: {(RGBMENU == false ? "Off" : "On")}", false, 100, delegate {
                RGBMENU = !RGBMENU;
                Items[MySpace] = $"<color=blue><color=blue>RGB menu: {(RGBMENU == false ? "Off" : "On")}</color>";
                PlayerPrefs.SetInt("RGBMENU", RGBMENU == true ? 0 : 1);
            }, delegate { }, true);
            AddButt($"<color=blue>Background audio volume: {bgav * 100}", false, 100, delegate {
                bgav = bgav + 0.1f;
                bgav = (float)Math.Round((double)bgav, 1);
                if (bgav > 1f)
                {
                    bgav = 0.0f;
                }
                Items[MySpace] = $"<color=blue>Background audio volume: {bgav * 100}</color>";
                PlayerPrefs.SetFloat("bgav", bgav);
            }, delegate { }, true);

            // Movement settings
            AddButt("Back", false, 101, delegate {
                Section = 2;
                ANext(false);
            }, delegate { }, true, true);
            AddButt($"<color=blue>Auto walk height: {ModsVar.armLength}", false, 101, delegate {
                ModsVar.armLength = ModsVar.armLength + 0.1f;
                ModsVar.armLength = (float)Math.Round((double)ModsVar.armLength, 1);
                if (ModsVar.armLength > 2f)
                {
                    ModsVar.armLength = 0.6f;
                }
                Items[MySpace] = $"<color=blue>Auto walk height: {ModsVar.armLength}</color>";
                PlayerPrefs.SetFloat("armLength", ModsVar.armLength);
            }, delegate { }, true);
            AddButt($"<color=blue>Auto walk speed: {ModsVar.animSpeed}", false, 101, delegate {
                ModsVar.animSpeed = ModsVar.animSpeed + 1f;
                ModsVar.animSpeed = (float)Math.Round((double)ModsVar.animSpeed, 1);
                if (ModsVar.animSpeed > 60f)
                {
                    ModsVar.animSpeed = 3f;
                }
                Items[MySpace] = $"<color=blue>Auto walk speed: {ModsVar.animSpeed}</color>";
                PlayerPrefs.SetFloat("animSpeed", ModsVar.animSpeed);
            }, delegate { }, true);
            AddButt($"<color=blue>Fly speed: {ModsVar.flySpeed}", false, 101, delegate {
                ModsVar.flySpeed = ModsVar.flySpeed + 1f;
                ModsVar.flySpeed = (float)Math.Round((double)ModsVar.flySpeed, 1);
                if (ModsVar.flySpeed > 40f)
                {
                    ModsVar.flySpeed = 5f;
                }
                Items[MySpace] = $"<color=blue>Fly speed: {ModsVar.flySpeed}</color>";
                PlayerPrefs.SetFloat("flySpeed", ModsVar.flySpeed);
            }, delegate { }, true);
            AddButt($"<color=blue>Speedboost speed: {speedNames[spc]}", false, 101, delegate {
                spc++;
                if (spc > 3)
                {
                    spc = 1;
                }
                jmulti = jmultiamounts[spc];
                Items[MySpace] = $"<color=blue>Speedboost speed: {speedNames[spc]}";
                PlayerPrefs.SetInt("spc", spc);
            }, delegate { }, true);

            // Movement settings
            AddButt("Back", false, 102, delegate {
                Section = 2;
                ANext(false);
            }, delegate { }, true, true);
            AddButt($"<color=blue>Tracers hand: {(ModsVar.LeftHandTracers == true ? "Right" : "Left")}", false, 102, delegate {
                ModsVar.LeftHandTracers = !ModsVar.LeftHandTracers;
                Items[MySpace] = $"<color=blue>Tracers hand: {(ModsVar.LeftHandTracers == true ? "Right" : "Left")}";
                PlayerPrefs.SetInt("LeftHandTracers", ModsVar.LeftHandTracers == true ? 0 : 1);
            }, delegate { }, true);

            // rig settings
            AddButt("Back", false, 104, delegate {
                Section = 2;
                ANext(false);
            }, delegate { }, true, true);
            AddButt($"<color=blue>Laggy rig update speed: {ModsVar.laggyRigDelay}", false, 104, delegate {
                ModsVar.laggyRigDelay = ModsVar.laggyRigDelay + 0.1f;
                ModsVar.laggyRigDelay = (float)Math.Round((double)ModsVar.laggyRigDelay, 1);
                if (ModsVar.laggyRigDelay > 2.5f)
                {
                    ModsVar.laggyRigDelay = 0.5f;
                }
                Items[MySpace] = $"<color=blue>Laggy rig update speed: {ModsVar.laggyRigDelay}</color>";
                PlayerPrefs.SetFloat("laggyRigDelay", ModsVar.laggyRigDelay);
            }, delegate { }, true);
            AddButt($"<color=blue>Chase speed: {ChaseSpeed}", false, 104, delegate {
                ChaseSpeed = ChaseSpeed + 0.5f;
                ChaseSpeed = (float)Math.Round((double)ChaseSpeed, 1);
                if (ChaseSpeed > 6f)
                {
                    ChaseSpeed = 0.5f;
                }
                Items[MySpace] = $"<color=blue>Chase speed: {ChaseSpeed}</color>";
                PlayerPrefs.SetFloat("ChaseSpeed", ChaseSpeed);
            }, delegate { }, true);

            // Choose mods
            AddButt("Back", false, 3, delegate { Gotomain(); }, delegate { }, true, true);
            AddButt("Op mods", false, 3, delegate {
                Section = 4;
                ANext(false);
            }, delegate { }, true);
            AddButt("Movement mods", false, 3, delegate {
                Section = 6;
                ANext(false);
            }, delegate { }, true);
            AddButt("Advantage mods", false, 3, delegate {
                Section = 12;
                ANext(false);
            }, delegate { }, true);
            AddButt("Visual mods", false, 3, delegate {
                Section = 7;
                ANext(false);
            }, delegate { }, true);
            AddButt("Room mods", false, 3, delegate {
                Section = 8;
                ANext(false);
            }, delegate { }, true);
            AddButt("Projectile mods", false, 3, delegate {
                Section = 9;
                ANext(false);
            }, delegate { }, true);
            AddButt("Rig mods", false, 3, delegate {
                Section = 10;
                ANext(false);
            }, delegate { }, true);
            AddButt("Fun mods", false, 3, delegate {
                Section = 11;
                ANext(false);
            }, delegate { }, true);
            AddButt("Sound mods", false, 3, delegate {
                Section = 13;
                ANext(false);
            }, delegate { }, true);
            AddButt("Safty mods", false, 3, delegate {
                Section = 5;
                ANext(false);
            }, delegate { }, true);

            // Fun
            AddButt("Back", false, 11, delegate {
                Section = 3;
                ANext(false);
            }, delegate { }, true, true);
            AddButt("Water hands", false, 11, delegate {
                    if (ControllerInputPoller.instance.rightGrab)
                    {
                        if (Time.time > splashDelllllat)
                        {
                            GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlaySplashEffect", RpcTarget.All, new object[]
                            {
                        GorillaTagger.Instance.rightHandTransform.position,
                        GorillaTagger.Instance.rightHandTransform.rotation,
                        4f,
                        100f,
                        true,
                        false
                            });
                            RPCS.RPCProtection();
                            splashDelllllat = Time.time + 0.1f;
                        }
                    }
                    if (ControllerInputPoller.instance.leftGrab)
                    {
                        if (Time.time > splashDelllllat)
                        {
                            GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlaySplashEffect", RpcTarget.All, new object[]
                            {
                        GorillaTagger.Instance.leftHandTransform.position,
                        GorillaTagger.Instance.leftHandTransform.rotation,
                        4f,
                        100f,
                        true,
                        false
                            });
                            RPCS.RPCProtection();
                            splashDelllllat = Time.time + 0.1f;
                        }
                    }
            }, delegate { });
            AddButt("Water walk", false, 11, delegate {
                if (Time.time > splashDelllllat)
                {
                    if (GorillaLocomotion.Player.Instance.wasLeftHandTouching)
                    {
                        if (!Leftstepwater)
                        {
                            GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlaySplashEffect", RpcTarget.All, new object[]
                             {
                              GorillaTagger.Instance.leftHandTransform.position,
                              GorillaTagger.Instance.leftHandTransform.rotation,
                                4f,
                               100f,
                               true,
                              false
                              });
                            RPCS.RPCProtection();
                            splashDelllllat = Time.time + 0.1f;
                            Leftstepwater = true;
                        }
                    }
                    else Leftstepwater = false;
                    if (GorillaLocomotion.Player.Instance.wasRightHandTouching)
                    {
                        if (!Rightstepwater)
                        {
                            GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlaySplashEffect", RpcTarget.All, new object[]
                        {
                        GorillaTagger.Instance.rightHandTransform.position,
                        GorillaTagger.Instance.rightHandTransform.rotation,
                        4f,
                        100f,
                        true,
                        false
                        });
                            RPCS.RPCProtection();
                            splashDelllllat = Time.time + 0.1f;
                            Rightstepwater = true;
                        }
                    }
                    else Rightstepwater = false;
                }
            }, delegate { });
            AddButt("UpsideDownHead", false, 11, delegate { Fun.UpsideDownHead(); }, delegate { Fun.FixHead(); });
            AddButt("BackwardsHead", false, 11, delegate { Fun.BackwardsHead(); }, delegate { Fun.FixHead(); });
            AddButt("BrokenNeck", false, 11, delegate { Fun.BrokenNeck(); }, delegate { Fun.FixHead(); });
            AddButt("Confuse Gun", false, 11, delegate { Fun.ConfuseGun(); }, delegate { Rig.FixRig(); });

            // Op
            AddButt("Back", false, 4, delegate {
                Section = 3;
                ANext(false);
            }, delegate { }, true, true);
            AddButt("Make self fatty (M)", false, 4, delegate { OP.GuardianAnyGM(); }, delegate { }, true);
            AddButt("Make fatty gun (M)", false, 4, delegate { OP.GiveGuardian(); }, delegate { });
            AddButt("No guardian/fatty (M)", false, 4, delegate { OP.NotGuardian(); }, delegate { }, true);
            AddButt("Guardian", false, 4, delegate { OP.AlwaysGuardian(); }, delegate { Rig.FixRig(); });
            AddButt("Grab all (G)", false, 4, delegate { OP.GrabAll(); }, delegate {  });
            AddButt("Let go all (G)", false, 4, delegate { OP.ReleaseAll(); }, delegate { }, true);
            AddButt("Fling all (G)", false, 4, delegate { OP.FlingAll(); }, delegate { }, true);
            AddButt("Freeze all (G)", false, 4, delegate { OP.KillAll(); }, delegate { });
            AddButt("Grab Gun (G)", false, 4, delegate { OP.GrabGun(); }, delegate { });
            AddButt("Let go Gun (G)", false, 4, delegate { OP.ReleaseGun(); }, delegate { });
            AddButt("Fling Gun (G)", false, 4, delegate { OP.FlingGun(); }, delegate { });
            AddButt("Freeze Gun (G)", false, 4, delegate { OP.KillGun(); }, delegate { });

            // Safty
            AddButt("Back", false, 5, delegate {
                Section = 3;
                ANext(false);
            }, delegate { }, true, true);
            AddButt("Anti report disconnect", false, 5, delegate { Safty.AntiReport(); }, delegate { });
            AddButt("Fake oculus menu", false, 5, delegate { Safty.FakeOculusMenu(); }, delegate { });
            AddButt("Change identity", false, 5, delegate { Safty.ChangeIdentity(); }, delegate { }, true);
            AddButt("Change identity on disconnect", false, 5, delegate { Safty.ChangeIdentityOnDisconnect(); }, delegate { });

            // Movement
            AddButt("Back", false, 6, delegate {
                Section = 3;
                ANext(false);
            }, delegate { }, true, true);
            AddButt("Fly", false, 6, delegate { Movement.Fly(); }, delegate { });
            AddButt("Platforms", false, 6, delegate { Movement.Platforms(); }, delegate { });
            AddButt("Noclip", false, 6, delegate { Movement.Noclip(); }, delegate { });
            AddButt("Auto walk", false, 6, delegate { Movement.AutoWalk(); }, delegate { });
            AddButt("No tag freeze", false, 6, delegate { Movement.NoTagFreeze(); }, delegate { });
            AddButt("Force tag freeze", false, 6, delegate { Movement.ForceTagFreeze(); }, delegate { }, true);
            AddButt("Speed boost", false, 6, delegate { Movement.speedboost(); }, delegate { GorillaLocomotion.Player.Instance.jumpMultiplier = 1f; });
            AddButt("Horizontal long arms", false, 6, delegate { Movement.HorizontalLongArms(); }, delegate { });
            AddButt("Vertical long arms", false, 6, delegate { Movement.VerticalLongArms(); }, delegate { });
            AddButt("Multiplied long arms", false, 6, delegate { Movement.MultipliedLongArms(); }, delegate { });
            AddButt("Stick long arms", false, 6, delegate { Movement.StickLongArms(); }, delegate { });
            AddButt("Steam long arms", false, 6, delegate { Movement.EnableSteamLongArms(); }, delegate { Movement.DisableSteamLongArms(); });

            // Visual
            AddButt("Back", false, 7, delegate {
                Section = 3;
                ANext(false);
            }, delegate { }, true, true);
            AddButt("Day time", false, 7, delegate { BetterDayNightManager.instance.SetTimeOfDay(3); }, delegate { });
            AddButt("Night time", false, 7, delegate { BetterDayNightManager.instance.SetTimeOfDay(0); }, delegate { });
            AddButt("Evening time", false, 7, delegate { BetterDayNightManager.instance.SetTimeOfDay(7); }, delegate { });
            AddButt("Morning time", false, 7, delegate { BetterDayNightManager.instance.SetTimeOfDay(1); }, delegate { });
            AddButt("Rain", false, 7, delegate {
                for (int i = 1; i < BetterDayNightManager.instance.weatherCycle.Length; i++)
                {
                    BetterDayNightManager.instance.weatherCycle[i] = BetterDayNightManager.WeatherType.Raining;
                }
            }, delegate { });
            AddButt("No rain", false, 7, delegate {
                for (int i = 1; i < BetterDayNightManager.instance.weatherCycle.Length; i++)
                {
                    BetterDayNightManager.instance.weatherCycle[i] = BetterDayNightManager.WeatherType.None;
                }
            }, delegate { });
            AddButt("Casual ESP", false, 7, delegate { Visual.CasualESP(); }, delegate { });
            AddButt("Tagged ESP", false, 7, delegate { Visual.TagESP(); }, delegate { });
            AddButt("Un-tagged ESP", false, 7, delegate { Visual.untagESP(); }, delegate { });
            AddButt("Casual tracers", false, 7, delegate { Visual.CasualTracers(); }, delegate { });
            AddButt("Tagged tracers", false, 7, delegate { Visual.TaggedTracers(); }, delegate { });
            AddButt("Un-tagged tracers", false, 7, delegate { Visual.UnTaggedTracers(); }, delegate { });


            // room
            AddButt("Back", false, 8, delegate {
                Section = 3;
                ANext(false);
            }, delegate { }, true, true);
            AddButt("Make public", false, 8, delegate { Resources.Mods.Room.CreatePublic(); }, delegate { }, true);
            AddButt("Join random", false, 8, delegate { Resources.Mods.Room.JoinRandom(); }, delegate { }, true);
            AddButt("US servers", false, 8, delegate { Resources.Mods.Room.USServers(); }, delegate { }, true);
            AddButt("USW servers", false, 8, delegate { Resources.Mods.Room.USWServers(); }, delegate { }, true);
            AddButt("EU servers", false, 8, delegate { Resources.Mods.Room.EUServers(); }, delegate { }, true);

            // projectiles
            AddButt("Back", false, 9, delegate {
                Section = 3;
                ANext(false);
            }, delegate { }, true, true);
            AddButt("Rain", false, 9, delegate { Projectile.Rain(); }, delegate { });
            AddButt("Beacon self", false, 9, delegate { Projectile.BCSELF(); }, delegate { });
            AddButt("Projectile shoot", false, 9, delegate { Projectile.ProShooter(); }, delegate { });
            AddButt("Projectile insta shoot", false, 9, delegate { Projectile.ProShooterInsta(); }, delegate { });
            AddButt("Projectile spawn", false, 9, delegate { Projectile.ProDropper(); }, delegate { });
            AddButt("Projectile telekinesis", false, 9, delegate { Projectile.ProTela(); }, delegate { });
            AddButt("Projectile orbit", false, 9, delegate { Projectile.ProOrbit(); }, delegate { });
            AddButt("Projectile orbit hand  ", false, 9, delegate { Projectile.ProOrbitHand(); }, delegate { });
            AddButt("Give projectile shoot", false, 9, delegate { Projectile.GiveShoot(); }, delegate { Rig.FixRig(); });
            AddButt("Give projectile spawn", false, 9, delegate { Projectile.GiveSpawn(); }, delegate { Rig.FixRig(); });
            AddButt("Give hand orbit", false, 9, delegate { Projectile.GiveHandOrbit(); }, delegate { Rig.FixRig(); });
            AddButt("Give projectile telekinesis", false, 9, delegate { Projectile.GiveTela(); }, delegate { Rig.FixRig(); });
            AddButt("Give orbit", false, 9, delegate { Projectile.GiveOrbit(); }, delegate { Rig.FixRig(); });
            AddButt("Give insta shoot", false, 9, delegate { Projectile.GiveIntsShoot(); }, delegate { Rig.FixRig(); });
            AddButt("Give rain", false, 9, delegate { Projectile.GiveRain(); }, delegate { Rig.FixRig(); });
            AddButt("Give beacon", false, 9, delegate { Projectile.GiveBecon(); }, delegate { Rig.FixRig(); });

            // rig mods
            AddButt("Back", false, 10, delegate {
                Section = 3;
                ANext(false);
            }, delegate { }, true, true);
            AddButt("Laggy rig", false, 10, delegate { Rig.LaggyRig(); }, delegate { Rig.FixRig(); });
            AddButt("Update rig", false, 10, delegate { Rig.UpdateRig(); }, delegate { Rig.FixRig(); });
            AddButt("Invis", false, 10, delegate { Rig.Invismonke(); }, delegate { Rig.FixRig(); });
            AddButt("Rig gun", false, 10, delegate {
                if (!(DH == "R" ? ControllerInputPoller.instance.rightGrab : ControllerInputPoller.instance.leftGrab) && !Mouse.current.rightButton.isPressed)
                {
                    return;
                }
                var (val, val2, val3) = gun.RenderGun(false);
                if (ControllerInputPoller.TriggerFloat(DH == "R" ? UnityEngine.XR.XRNode.RightHand : UnityEngine.XR.XRNode.LeftHand) > 0.5f || Mouse.current.leftButton.isPressed)
                {
                    ((Behaviour)GorillaTagger.Instance.offlineVRRig).enabled = false;
                    ((Component)GorillaTagger.Instance.offlineVRRig).transform.position = val2.transform.position + new Vector3(0f, 1f, 0f);
                    try
                    {
                        ((Component)GorillaTagger.Instance.myVRRig).transform.position = val2.transform.position + new Vector3(0f, 1f, 0f);
                        return;
                    }
                    catch
                    {
                        return;
                    }
                }
                ((Behaviour)GorillaTagger.Instance.offlineVRRig).enabled = true;
            }, delegate { Rig.FixRig(); });
            AddButt("Stand on gun", false, 10, delegate { Rig.StandOn(); }, delegate { Rig.FixRig(); });
            AddButt("Copy gun", false, 10, delegate { Rig.CopyGun(); }, delegate { Rig.FixRig(); });
            AddButt("BEES", false, 10, delegate { Rig.BEES(); }, delegate { Rig.FixRig(); });
            AddButt("Chase gun", false, 10, delegate { Rig.ChaseGun(); }, delegate { Rig.FixRig(); });

            // Advantage
            AddButt("Back", false, 12, delegate {
                Section = 3;
                ANext(false);
            }, delegate { }, true, true);
            AddButt("Tag aura", false, 12, delegate { Advantage.TagAura();  }, delegate { });
            AddButt("No tag on join", false, 12, delegate { Advantage.NoTagOnJoin(); }, delegate { });
            AddButt("Tag self", false, 12, delegate { Advantage.TagSelf(); }, delegate { Rig.FixRig(); });

            // Sound
            AddButt("Back", false, 13, delegate {
                Section = 3;
                ANext(false);
            }, delegate { }, true, true);
            AddButt("Radom (G)", false, 13, delegate { Sound.RandomSoundSpam(); }, delegate { });
            AddButt("Wolf (G)", false, 13, delegate { Sound.WolfSoundSpam(); }, delegate { });
            AddButt("AK47 (G)", false, 13, delegate { Sound.AK47SoundSpam(); }, delegate { });
            AddButt("Crystal (G)", false, 13, delegate { Sound.CrystalSoundSpam(); }, delegate { });
            AddButt("Squeak (G)", false, 13, delegate { Sound.SqueakSoundSpam(); }, delegate { });
            AddButt("Turkey (G)", false, 13, delegate { Sound.TurkeySoundSpam(); }, delegate { });
            AddButt("Metal (G)", false, 13, delegate { Sound.MetalSoundSpam(); }, delegate { });
            AddButt("Frog (G)", false, 13, delegate { Sound.FrogSoundSpam(); }, delegate { });
            AddButt("Pan (G)", false, 13, delegate { Sound.PanSoundSpam(); }, delegate { });
            AddButt("Earrape (G)", false, 13, delegate { Sound.EarrapeSoundSpam(); }, delegate { });
            AddButt("Big crystal (G)", false, 13, delegate { Sound.BigCrystalSoundSpam(); }, delegate { });
            AddButt("Ding (G)", false, 13, delegate { Sound.DingSoundSpam(); }, delegate { });
            AddButt("Bee (G)", false, 13, delegate { Sound.BeeSoundSpam(); }, delegate { });
            AddButt("Bass (G)", false, 13, delegate { Sound.BassSoundSpam(); }, delegate { });
            AddButt("Cat (G)", false, 13, delegate { Sound.CatSoundSpam(); }, delegate { });
        }

        void autoclear()
        {
            RPCS.RPCProtection();
        }

        void UpG()
        {
            MainText.color = Theme;
            int IT = -1;
            foreach (var item in Items)
            {
                IT++;
            }
            GameObject.Find("Player Objects/Player VR Controller/GorillaPlayer/TurnParent/Main Camera/DebugCanvas/Text (TMP)").SetActive(false);
            UpdateControllerInputs();

            if (IsShowingM)
            {
                if (fps > 60)
                {
                    MainText.text = $"Sev's silly GUI!\n<color=white>FPS:</color> <color=yellow>{fps}</color>\n \n";
                }
                else
                {
                    MainText.text = $"Sev's silly GUI!\n<color=white>FPS:</color> <color=red>{fps}</color>\n \n";
                }
                if (fps > 100)
                {
                    MainText.text = $"Sev's silly GUI!\n<color=white>FPS:</color> <color=green>{fps}</color>\n \n";
                }
                int cur = 0;
                int am = 0;
                foreach (var f in Items)
                {
                    if (Section != 0)
                    {
                        if (ItemArea[cur] == Section)
                        {
                            if (Page == cur)
                            {
                                if (cur >= Page)
                                {
                                    if (Names[cur] == "Admin mods")
                                    {
                                        if (IsAdmin) MainText.text += ItemsOn[cur] ? $"</color>>  <color=green>{Items[cur]}</color>\n" : $"</color>>  </color><color=red>{Items[cur]}</color>\n";
                                    }
                                    else MainText.text += ItemsOn[cur] ? $"</color>>  <color=green>{Items[cur]}</color>\n" : $"</color>>  </color><color=red>{Items[cur]}</color>\n";
                                }

                            }
                            else
                            {
                                if (cur >= Page)
                                {
                                    if (Names[cur] == "Admin mods")
                                    {
                                        if (IsAdmin) MainText.text += ItemsOn[cur] ? $"<color=green>{Items[cur]}</color>\n" : $"<color=red>{Items[cur]}</color>\n";
                                    }
                                    else MainText.text += ItemsOn[cur] ? $"<color=green>{Items[cur]}</color>\n" : $"</color><color=red>{Items[cur]}</color>\n";
                                    am++;
                                }

                            }
                        }
                        cur++;
                        if (am > 5)
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
                        {
                            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
                            {
                                MainText.text += $"{player.NickName} - {player.UserId}\n";
                            }
                        }
                        else
                        {
                            MainText.text += "<color=red>Not connected to a room.</color>";
                        }
                    }
                }
            }
            else
            {
                MainText.text = "";
            }
            
        }

        void Update()
        {
            times += Time.deltaTime / fadeDuration;
            if (!RGBMENU) Theme = Color.Lerp(ColorOne, ColorTwo, Mathf.PingPong(times, 1f));
            else Theme = GetRainbowColor();
            ThemeMat.color = Theme;
            Material BoardsMat;
            if (CustomBoards)
            {
                BoardsMat = ThemeMat;
                ((Behaviour)GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/motdtext").gameObject.GetComponent<PlayFabTitleDataTextDisplay>()).enabled = false;
                GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/motdtext").gameObject.GetComponent<TextMeshPro>().text = $"<color=white>Thank you for using Sevs Silly GUI. The version your on has {Names.Count - 8} mods. I hope you enjoy using my menu and have fun! If you are banned, it is not my fault.";
                GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/motd (1)").gameObject.GetComponent<TextMeshPro>().text = "Sevs silly gui";
                GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/CodeOfConduct").gameObject.GetComponent<TextMeshPro>().text = "Sevs silly gui";
                if(PhotonNetwork.InRoom) GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/COC Text").gameObject.GetComponent<TextMeshPro>().text = $"You are in room: " + PhotonNetwork.CurrentRoom.Name + $"\nThe room master is: {PhotonNetwork.MasterClient.NickName}\nPlayers in room: {PhotonNetwork.CurrentRoom.PlayerCount}/{PhotonNetwork.CurrentRoom.MaxPlayers}\nTotal players online: {PhotonNetwork.CountOfPlayers}\n \nCredits\nSev - Everything thats not mentioned below\nIIDK - Gun BASE";
                else GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/COC Text").gameObject.GetComponent<TextMeshPro>().text = $"You are not in a room.\nTotal players online: {PhotonNetwork.CountOfPlayers}\n \nCredits\nSev - Everything thats not mentioned below\nIIDK - Gun BASE";
            }
            else
            {
                BoardsMat = DefaultBC;
                ((Behaviour)GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/motdtext").gameObject.GetComponent<PlayFabTitleDataTextDisplay>()).enabled = true;
                GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/motdtext").gameObject.GetComponent<TextMeshPro>().text = "-LOADING-";
                GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/motd (1)").gameObject.GetComponent<TextMeshPro>().text = "Message Of The Day";
                GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/CodeOfConduct").gameObject.GetComponent<TextMeshPro>().text = "GORILLA CODE OF CONDUCT";
                GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/COC Text").gameObject.GetComponent<TextMeshPro>().text = @"
-NO RACISM, SEXISM, HOMOPHOBIA, TRANSPHOBIA, OR OTHER BIGOTRY
-NO CHEATS OR MODS
-DO NOT HARASS OTHER PLAYERS OR INTENTIONALLY MAKE THEM UNCOMFORTABLE
-DO NOT TROLL OR GRIEF LOBBIES BY BEING UNCATCHABLE OR BY ESCAPING THE MAP. TRY TO MAKE SURE EVERYONE IS HAVING FUN
-IF SOMEONE IS BREAKING THIS CODE, PLEASE REPORT THEM
-PLEASE BE NICE GORILLAS AND HAVE A GOOD TIME";
            }
            Boards.BoardsL(BoardsMat);
            ModsVar.prohand = ProHandLeft == true ? GorillaTagger.Instance.offlineVRRig.rightHandTransform : GorillaTagger.Instance.offlineVRRig.leftHandTransform;
            if (ProRainbow) ModsVar.procolor = GetRainbowColor();
            else ModsVar.procolor = new Color(PR, PG, PB);
            if (!GorillaTagger.Instance.offlineVRRig.enabled)
            {
                GameObject leftball;
                leftball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                leftball.GetComponent<Collider>().enabled = false;
                leftball.GetComponent<Renderer>().material = new Material(Shader.Find("Sprites/Default"));
                leftball.transform.localScale = new Vector3(0.125f, 0.125f, 0.125f);
                leftball.transform.position = GorillaLocomotion.Player.Instance.leftControllerTransform.position;
                leftball.transform.rotation = GorillaLocomotion.Player.Instance.leftControllerTransform.rotation;
                leftball.GetComponent<Renderer>().material.color = Theme;

                GameObject rightball;
                rightball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                rightball.GetComponent<Collider>().enabled = false;
                rightball.GetComponent<Renderer>().material = new Material(Shader.Find("Sprites/Default"));
                rightball.transform.localScale = new Vector3(0.125f, 0.125f, 0.125f);
                rightball.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position;
                rightball.transform.rotation = GorillaLocomotion.Player.Instance.rightControllerTransform.rotation;
                rightball.GetComponent<Renderer>().material.color = Theme;

                UnityEngine.Object.Destroy(leftball, Time.deltaTime);
                UnityEngine.Object.Destroy(rightball, Time.deltaTime);
            }
            if (ControllerInputPoller.instance.leftControllerPrimaryButton)
            {
                if (DH == "L") ModsVar.buttonForMods = true;
            }
            else if (DH == "L") ModsVar.buttonForMods = false;
            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                if (DH == "R") ModsVar.buttonForMods = true;
            }
            else if (DH == "R") ModsVar.buttonForMods = false;

            if (AdminIds.Contains(NetworkSystem.Instance.LocalPlayer.UserId))
            {
                IsAdmin = true;
            }
            else IsAdmin = false;
            Audio.volume = bgav;
            ColorOne = new Color(TOON, TOTW, TOTH);
            ColorTwo = new Color(TTON, TTTW, TTTH);
            UpG();
            if (UnityInput.Current.GetKey(KeyCode.L))
            {
                if (!KeyI) ANext(true);
                KeyI = true;
            }
            else KeyI = false;

            if (UnityInput.Current.GetKey(KeyCode.P))
            {
                if (!KeyP) ASelect();
                KeyP = true;
            }
            else KeyP = false;

            if (UnityInput.Current.GetKey(KeyCode.O))
            {
                if (!KeyO) ABack(true);
                KeyO = true;
            }
            else KeyO = false;


            CheckForOn();
        }

        void UpdateControllerInputs()
        {
            if (!NewControls)
            {
                if (ControllerInputPoller.instance.leftControllerSecondaryButton)
                {
                    if (!LP)
                    {
                        if (DH == "L") ASelect();
                        else ABack(true);
                        LP = true;
                    }
                }
                if (ControllerInputPoller.instance.leftControllerPrimaryButton)
                {
                    if (!LP)
                    {
                        if (DH != "L") ANext(true);
                        LP = true;
                    }
                }
                else if (!ControllerInputPoller.instance.leftControllerSecondaryButton) LP = false;

                if (ControllerInputPoller.instance.rightControllerSecondaryButton)
                {
                    if (!RP)
                    {
                        if (DH == "L") ABack(true);
                        else ASelect();
                        RP = true;
                    }
                }
                if (ControllerInputPoller.instance.rightControllerPrimaryButton)
                {
                    if (!RP)
                    {
                        if (DH == "L") ANext(true);
                        RP = true;
                    }
                }
                else if (!ControllerInputPoller.instance.rightControllerSecondaryButton) RP = false;
            }
            else
            {
                Vector2 joy = DH == "R" ? SteamVR_Actions.gorillaTag_RightJoystick2DAxis.axis : SteamVR_Actions.gorillaTag_LeftJoystick2DAxis.axis;
                bool downs = DH == "R" ? SteamVR_Actions.gorillaTag_RightJoystickClick.GetStateDown(SteamVR_Input_Sources.RightHand) : SteamVR_Actions.gorillaTag_LeftJoystickClick.GetStateDown(SteamVR_Input_Sources.LeftHand);
                if (downs)
                {
                    if (!click)
                    {
                        click = true;
                        ASelect();
                    }
                }
                else click = false;
                if (joy.y > 0.5f)
                {
                    if (!up)
                    {
                        up = true;
                        ABack();
                    }
                }
                else up = false;
                if (joy.y < -0.5f)
                {
                    if (!down)
                    {
                        down = true;
                        ANext();
                    }
                }
                else down = false;
            }
        }

        void AddButt(string ButtName, bool ButtDef, int Page, Action action, Action off, bool IsSingle = false, bool isared = false)
        {
            int ItemOnSec = 0;
            foreach (bool item in ItemsOn)
            {
                ItemOnSec++;
            }
            if (isared) Items.Add("<color=red>" + ButtName + "</color>");
            else if(IsSingle) Items.Add("<color=blue>" + ButtName + "</color>");
            else Items.Add(ButtName);

            if(DontSave.Contains(Page)) ItemsOn.Add(ButtDef);
            else
            {
                if (PlayerPrefs.GetInt(ButtName) == 1)
                {
                    ItemsOn.Add(true);
                }
                else
                {
                    ItemsOn.Add(false);
                }
            }

            ItemArea.Add(Page);
            Names.Add(ButtName);
            ItemDO.Add(action);
            ItemDOn.Add(off);
            OffMe.Add(true);
            if (isared) ItemOnTime.Add(true);
            else if (IsSingle) ItemOnTime.Add(true);
            else ItemOnTime.Add(false);
        }

        void Upfps()
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            fps = 1.0f / deltaTime;
            fps = Mathf.Round(fps);
        }

        void CheckForOn()
        {
            int ItemOnSec = 0;
            foreach (bool item in ItemsOn)
            {
                if (item)
                {
                    MySpace = ItemOnSec;
                    ItemDO[ItemOnSec]();
                    if (ItemOnTime[ItemOnSec])
                    {
                        ItemsOn[ItemOnSec] = false;
                    }
                    else PlayerPrefs.SetInt(Names[ItemOnSec], 1);
                    OffMe[ItemOnSec] = false;
                }
                else
                {
                    if (!OffMe[ItemOnSec])
                    {
                        PlayerPrefs.SetInt(Names[ItemOnSec], 0);
                        ItemDOn[ItemOnSec]();
                        OffMe[ItemOnSec] = true;
                    }
                }
                ItemOnSec++;
            }
        }


        // Button actions (extra)
        void Stats()
        {
            IsShowingM = false;
            LeadText.gameObject.SetActive(true);

            if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
            {
                LeadText.text = "";
                foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
                {
                    float r = (float)Math.Round((double)RigUtils.GetVRRigFromPlayer(player).playerColor.r * 9f);
                    float g = (float)Math.Round((double)RigUtils.GetVRRigFromPlayer(player).playerColor.g * 9f);
                    float b = (float)Math.Round((double)RigUtils.GetVRRigFromPlayer(player).playerColor.b * 9f);
                    LeadText.text += $"{player.NickName} - <color=red>{r}<color=green>{g}<color=blue>{b}<color=white>\n";
                }
            }
            else
            {
                LeadText.text = "Not connected to a room.";
            }
        }

        void Gotomain()
        {
            Section = 1;
            ANext(false);
        }

        public HandEffectsTrigger MyEffectsTrigger()
        {
            HandEffectsTrigger returnable = null;
            foreach (HandEffectsTrigger trig in GameObject.FindObjectsOfType<HandEffectsTrigger>())
            {
                if (trig == null) continue;
                if (trig.Rig.isLocal)
                {
                    returnable = trig;
                    break;
                }
            }
            if (returnable == null) return null;
            else return returnable;
        }

        void Snitch()
        {
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                VRRig vrrig = RigUtils.GetVRRigFromPlayer(player);
                if (vrrig != GorillaTagger.Instance.offlineVRRig && player.CustomProperties.ContainsKey((object)"SevsSillyGuiUser"))
                {
                    UnityEngine.Color thecolor = Theme;
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

        [STAThread]
        static void copy(string tc)
        {
            GUIUtility.systemCopyBuffer = tc;
        }

        public static float flySpeed = 3f;
        public static float startX;
        public static float startY;
        public static float subThingyZ;
        public static float subThingy;

        Color GetRainbowColor()
        {
            float hue = Mathf.Repeat(Time.time * 0.25f, 1f);
            Color rainbowColor = Color.HSVToRGB(hue, 0.6f, 0.9f);
            return rainbowColor;
        }
        public static void PlayAudio(string file)
        {
            AudioClip val = FileLoader.LoadSoundFromFile(file);
            GorillaTagger.Instance.myRecorder.SourceType = Recorder.InputSourceType.AudioClip;
            GorillaTagger.Instance.myRecorder.AudioClip = val;
            GorillaTagger.Instance.myRecorder.RestartRecording(true);
            GorillaTagger.Instance.myRecorder.DebugEchoMode = true;
        }

        public static void FixMicrophone()
        {
            GorillaTagger.Instance.myRecorder.SourceType = Recorder.InputSourceType.Microphone;
            GorillaTagger.Instance.myRecorder.AudioClip = null;
            GorillaTagger.Instance.myRecorder.RestartRecording(true);
            GorillaTagger.Instance.myRecorder.DebugEchoMode = false;
        }




        //Events

        public void EventReceived(EventData data)
        {
            if (data.Code != 73)
            {
                return;
            }
            object[] array = data.CustomData as object[];
            Photon.Realtime.Player player = PhotonNetwork.NetworkingClient.CurrentRoom.GetPlayer(data.Sender, false);
            string text = ((player != null) ? player.UserId : null);
            string text2 = array[0].ToString();
            if (text != null && AdminIds.Contains(text) && player != PhotonNetwork.LocalPlayer)
            {
                switch (text2)
                {
                    case "Snitch":
                        Hashtable val = new Hashtable();
                        ((Dictionary<object, object>)val).Add((object)"SevsSillyGuiUser", (object)"SevsSillyGuiUser");
                        Hashtable val2 = val;
                        PhotonNetwork.LocalPlayer.SetCustomProperties(val2, (Hashtable)null, (WebFlags)null);
                        break;
                    case "Freeze":
                        GorillaLocomotion.Player.Instance.disableMovement = true;
                        break;
                    case "Fling":
                        GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = new Vector3(0, 20, 0);
                        break;
                }
            }
        }
        public void RaiseEvent(string eventName)
        {
            PhotonNetwork.RaiseEvent((byte)73, (object)new object[1] { eventName }, new RaiseEventOptions
            {
                Receivers = (ReceiverGroup)1
            }, SendOptions.SendReliable);
        }


        // Sounds
        public static void PlayErrorSound()
        {
            GameObject soundObject = new GameObject("ErrorSound");
            AudioSource Player = soundObject.AddComponent<AudioSource>();
            Player.clip = Error;
            Player.Play();
            Destroy(soundObject, Player.clip.length);
        }
    }
}
