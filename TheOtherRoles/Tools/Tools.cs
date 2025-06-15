using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AmongUs.GameOptions;
using HarmonyLib;
using Hazel;
using InnerNet;
using Reactor.Utilities.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Object = Il2CppSystem.Object;
using MoreTools;
using Epic.OnlineServices;

namespace MoreTools.Tools
{
    public class Tools
    {
        public static void SendInGame(string text, bool isAlways = false)
        {
            if (DestroyableSingleton<HudManager>._instance) DestroyableSingleton<HudManager>.Instance.Notifier.AddDisconnectMessage(text);
        }
    }
}
