using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections.Generic;

namespace SevsSillyGui.Resources
{
    public class FileLoader
    {
        public static AudioClip LoadSoundFromURL(string resourcePath, string fileName)
        {
            if (!Directory.Exists("SevsSillyGui"))
            {
                Directory.CreateDirectory("SevsSillyGui");
            }
            if (!File.Exists("SevsSillyGui/" + fileName))
            {
                UnityEngine.Debug.Log("Downloading " + fileName);
                WebClient stream = new WebClient();
                stream.DownloadFile(resourcePath, "SevsSillyGui/" + fileName);
            }

            return LoadSoundFromFile(fileName);
        }

        public static Dictionary<string, AudioClip> audioFilePool = new Dictionary<string, AudioClip> { };
        public static AudioClip LoadSoundFromFile(string fileName)
        {
            AudioClip sound = null;

            if (!audioFilePool.ContainsKey(fileName))
            {
                if (!Directory.Exists("SevsSillyGui"))
                {
                    Directory.CreateDirectory("SevsSillyGui");
                }
                string filePath = System.IO.Path.Combine(System.Reflection.Assembly.GetExecutingAssembly().Location, "SevsSillyGui/" + fileName);
                filePath = filePath.Split("BepInEx\\")[0] + "SevsSillyGui/" + fileName;
                filePath = filePath.Replace("\\", "/");

                UnityWebRequest actualrequest = UnityWebRequestMultimedia.GetAudioClip("file://" + filePath, GetAudioType(GetFileExtension(fileName)));
                UnityWebRequestAsyncOperation newvar = actualrequest.SendWebRequest();
                while (!newvar.isDone) { }

                AudioClip actualclip = DownloadHandlerAudioClip.GetContent(actualrequest);
                sound = Task.FromResult(actualclip).Result;

                audioFilePool.Add(fileName, sound);
            }
            else
            {
                sound = audioFilePool[fileName];
            }

            return sound;
        }

        public static AudioClip LoadSoundboard(string fileName)
        {
            AudioClip sound = null;

            if (!audioFilePool.ContainsKey(fileName))
            {
                if (!Directory.Exists("SevsSillyGui/Sounds"))
                {
                    Directory.CreateDirectory("SevsSillyGui/Sounds");
                }
                string filePath = System.IO.Path.Combine(System.Reflection.Assembly.GetExecutingAssembly().Location, "SevsSillyGui/Sounds/" + fileName);
                filePath = filePath.Split("BepInEx\\")[0] + "SevsSillyGui/Sounds/" + fileName;
                filePath = filePath.Replace("\\", "/");

                UnityWebRequest actualrequest = UnityWebRequestMultimedia.GetAudioClip("file://" + filePath, GetAudioType(GetFileExtension(fileName)));
                UnityWebRequestAsyncOperation newvar = actualrequest.SendWebRequest();
                while (!newvar.isDone) { }

                AudioClip actualclip = DownloadHandlerAudioClip.GetContent(actualrequest);
                sound = Task.FromResult(actualclip).Result;

                audioFilePool.Add(fileName, sound);
            }
            else
            {
                sound = audioFilePool[fileName];
            }

            return sound;
        }

        public static string GetFileExtension(string fileName)
        {
            return fileName.ToLower().Split(".")[fileName.Split(".").Length - 1];
        }

        public static AudioType GetAudioType(string extension)
        {
            switch (extension.ToLower())
            {
                case "mp3":
                    return AudioType.MPEG;
                case "wav":
                    return AudioType.WAV;
                case "ogg":
                    return AudioType.OGGVORBIS;
                case "aiff":
                    return AudioType.AIFF;
            }
            return AudioType.WAV;
        }
    }
}