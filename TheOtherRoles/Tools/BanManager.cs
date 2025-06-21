using System;
using System.IO;
using System.Text.RegularExpressions;
using HarmonyLib;
using MoreTools.Tools;
using MoreTools.Attributes;
using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;

namespace MoreTools.Tools
{
    public static class BanManager
    {

        [PluginModuleInitializer]
        public static void CheckBanPlayer(InnerNet.ClientData player)
        {
            if (!AmongUsClient.Instance.AmHost) return;
            if (CheckBanList(player))
            {
                AmongUsClient.Instance.KickPlayer(player.Id, true);
                Tools.SendInGame(player.PlayerName + " 已被封禁，因其之前就被封禁过");
                MoreToolsPlugin.Logger.LogInfo($"{player.PlayerName}已被封禁，因其之前就被封禁过");
                return;
            }
        }
        public static bool CheckBanList(InnerNet.ClientData player)
        {
            _ = HttpBanList.LoadHttpBanList();
            if (player == null || player?.FriendCode == "") return false;
            try
            {
                foreach (string bp in HttpBanList.httpBanList) 
                {
                    if (player.FriendCode == bp) return true;
                }
            }
            catch (Exception ex)
            {
                MoreToolsPlugin.Logger.LogError(ex + "CheckBanList报错");
            }
            return false;
        }
    }
    [HarmonyPatch(typeof(BanMenu), nameof(BanMenu.Select))]
    class BanMenuSelectPatch
    {
        public static void Postfix(BanMenu __instance, int clientId)
        {
            InnerNet.ClientData recentClient = AmongUsClient.Instance.GetRecentClient(clientId);
            if (recentClient == null) return;
            if (!BanManager.CheckBanList(recentClient)) __instance.BanButton.GetComponent<ButtonRolloverHandler>().SetEnabledColors();
        }
    }
    public static class HttpBanList
    {
        public static List<string> httpBanList = new List<string>();
        public static async Task LoadHttpBanList()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://raw.githubusercontent.com/linmeideli/MoreTools/main/banlist.txt");
                response.EnsureSuccessStatusCode();
                string httpBanList = await response.Content.ReadAsStringAsync();

                foreach (string line in httpBanList.Split("\n", StringSplitOptions.RemoveEmptyEntries))
                {
                    HttpBanList.httpBanList.Add(line);
                    MoreToolsPlugin.Logger.LogInfo("BAN名单：\n" + line);
                }
            }
        }
    }
}
