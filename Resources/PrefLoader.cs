using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SevsSillyGui;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SevsSillyGui.Resources
{
    public class PrefLoader
    {
        public static void Load()
        {
            if (PlayerPrefs.HasKey("CONT"))
            {
                Plugin.MC = PlayerPrefs.GetInt("CONT") == 1 ? "Old" : "New";
                Plugin.NewControls = PlayerPrefs.GetInt("CONT") == 1 ? false : true;
            }

            if (PlayerPrefs.HasKey("DH"))
            {
                Plugin.DH = PlayerPrefs.GetString("DH");
            }


            if (PlayerPrefs.HasKey("TOON"))
            {
                Plugin.TOON = PlayerPrefs.GetFloat("TOON");
            }

            if (PlayerPrefs.HasKey("TOTW"))
            {
                Plugin.TOTW = PlayerPrefs.GetFloat("TOTW");
            }

            if (PlayerPrefs.HasKey("TOTH"))
            {
                Plugin.TOTH = PlayerPrefs.GetFloat("TOTH");
            }

            if (PlayerPrefs.HasKey("TTON"))
            {
                Plugin.TTON = PlayerPrefs.GetFloat("TTON");
            }

            if (PlayerPrefs.HasKey("TTTW"))
            {
                Plugin.TTTW = PlayerPrefs.GetFloat("TTTW");
            }

            if (PlayerPrefs.HasKey("TTTH"))
            {
                Plugin.TTTH = PlayerPrefs.GetFloat("TTTH");
            }

            if (PlayerPrefs.HasKey("bgav"))
            {
                Plugin.bgav = PlayerPrefs.GetFloat("bgav");
            }

            if (PlayerPrefs.HasKey("laggyRigDelay"))
            {
                ModsVar.laggyRigDelay = PlayerPrefs.GetFloat("laggyRigDelay");
            }

            if (PlayerPrefs.HasKey("animSpeed"))
            {
                ModsVar.animSpeed = PlayerPrefs.GetFloat("animSpeed");
            }

            if (PlayerPrefs.HasKey("armLength"))
            {
                ModsVar.armLength = PlayerPrefs.GetFloat("armLength");
            }

            if (PlayerPrefs.HasKey("LeftHandTracers"))
            {
                ModsVar.LeftHandTracers = PlayerPrefs.GetInt("LeftHandTracers") == 1 ? false : true;
            }

            if (PlayerPrefs.HasKey("PR"))
            {
                Plugin.PR = PlayerPrefs.GetFloat("PR");
            }

            if (PlayerPrefs.HasKey("PG"))
            {
                Plugin.PG = PlayerPrefs.GetFloat("PG");
            }

            if (PlayerPrefs.HasKey("PB"))
            {
                Plugin.PB = PlayerPrefs.GetFloat("PB");
            }

            if (PlayerPrefs.HasKey("ProHandLeft"))
            {
                Plugin.ProHandLeft = PlayerPrefs.GetInt("ProHandLeft") == 1 ? false : true;
            }

            if (PlayerPrefs.HasKey("ProTypesS"))
            {
                Plugin.ProTypeNum = PlayerPrefs.GetInt("ProTypesS");
                ModsVar.protype = ModsVar.ExternalProjectiles[PlayerPrefs.GetInt("ProTypesS")];
            }

            if (PlayerPrefs.HasKey("AUTOCLEARRPCS"))
            {
                Plugin.AUTOCLEARRPCS = PlayerPrefs.GetInt("AUTOCLEARRPCS") == 1 ? false : true;
            }

            if (PlayerPrefs.HasKey("flySpeed"))
            {
                ModsVar.flySpeed = PlayerPrefs.GetFloat("flySpeed");
            }

            if (PlayerPrefs.HasKey("FallGravity"))
            {
                Plugin.FallGravity = PlayerPrefs.GetInt("FallGravity") == 1 ? false : true;
            }

            if (PlayerPrefs.HasKey("MenuFall"))
            {
                Plugin.MenuFall = PlayerPrefs.GetInt("MenuFall") == 1 ? false : true;
            }

            if (PlayerPrefs.HasKey("CustomBoards"))
            {
                Plugin.CustomBoards = PlayerPrefs.GetInt("CustomBoards") == 1 ? false : true;
            }

            if (PlayerPrefs.HasKey("ProRainbow"))
            {
                Plugin.ProRainbow = PlayerPrefs.GetInt("ProRainbow") == 1 ? false : true;
            }

            if (PlayerPrefs.HasKey("spc"))
            {
                Plugin.spc = PlayerPrefs.GetInt("spc");
                Plugin.jmulti = Plugin.jmultiamounts[Plugin.spc];
            }

            if (PlayerPrefs.HasKey("RGBMENU"))
            {
                Plugin.RGBMENU = PlayerPrefs.GetInt("RGBMENU") == 1 ? false : true;
            }

            if (PlayerPrefs.HasKey("ChaseSpeed"))
            {
                Plugin.ChaseSpeed = PlayerPrefs.GetFloat("ChaseSpeed");
            }
        }
    }
}
