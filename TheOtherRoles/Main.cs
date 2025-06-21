global using Il2CppInterop.Runtime;
global using Il2CppInterop.Runtime.Attributes;
global using Il2CppInterop.Runtime.InteropTypes;
global using Il2CppInterop.Runtime.InteropTypes.Arrays;
global using Il2CppInterop.Runtime.Injection;

using BepInEx;
using BepInEx.Configuration;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Hazel;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using Il2CppSystem.Security.Cryptography;
using Il2CppSystem.Text;
using AmongUs.Data;
using System.Threading.Tasks;
using MoreTools.Tools;

namespace MoreTools
{
    [BepInPlugin(Id, "MoreTools", VersionString)]
    [BepInProcess("Among Us.exe")]
    
    public class MoreToolsPlugin : BasePlugin
    {
        public const string Id = "xtreme.elm.moretools";
        public const string VersionString = "1.0.0";
        public static string AmongUsVersion = "2024.10.29";

        public static Version Version = Version.Parse(VersionString);
        internal static BepInEx.Logging.ManualLogSource Logger;
         
        public Harmony Harmony { get; } = new Harmony(Id);
        public static MoreToolsPlugin Instance;

        public override void Load() {

            Logger = Log;
            Instance = this;
            Harmony.PatchAll();

            MoreToolsPlugin.Logger.LogInfo("===============MoreTools Loading completed!===============");
        }
    }
}
