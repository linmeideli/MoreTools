using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using InnerNet;
using MoreTools.Tools;
using Reactor.Networking.Rpc;

namespace MoreTools.Patches
{
    [HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.OnPlayerJoined))]
    class OnPlayerJoinedPatch
    {
        public static void Postfix(AmongUsClient __instance, [HarmonyArgument(0)] ClientData client)
        {
            MoreToolsPlugin.Logger.LogInfo($"{client.PlayerName}(ClientID:{client.Id})が参加");
            if (AmongUsClient.Instance.AmHost && client.FriendCode == "")
            {
                AmongUsClient.Instance.KickPlayer(client.Id, false);
                Tools.Tools.SendInGame(client.PlayerName + " 因无好友代码被踢出");
                MoreToolsPlugin.Logger.LogInfo($"フレンドコードがないプレイヤーを{client?.PlayerName}をキックしました");
            }
            if (DestroyableSingleton<FriendsListManager>.Instance.IsPlayerBlockedUsername(client.FriendCode) && AmongUsClient.Instance.AmHost)
            {
                AmongUsClient.Instance.KickPlayer(client.Id, true);
                MoreToolsPlugin.Logger.LogInfo($"ブロック済みのプレイヤー{client?.PlayerName}({client.FriendCode})をBANしました。");
            }
            BanManager.CheckBanPlayer(client);
        }
    }
}
