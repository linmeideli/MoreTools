using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using InnerNet;
using MoreTools.Tools;

namespace MoreTools.Patches
{
    [HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.OnPlayerJoined))]
    class OnPlayerJoinedPatch
    {
        public static void Postfix(AmongUsClient __instance, [HarmonyArgument(0)] ClientData client)
        {
            MoreToolsPlugin.Logger.LogInfo($"{client.PlayerName}(客户端ID：:{client.Id})加入了房间");
            if (AmongUsClient.Instance.AmHost && client.FriendCode == "")
            {
                AmongUsClient.Instance.KickPlayer(client.Id, false);
                Tools.Tools.SendInGame(client.PlayerName + " 因无好友代码被踢出");
                MoreToolsPlugin.Logger.LogInfo($"踢了一个没有好友代码的 {client?.PlayerName} 玩家");
            }
            if (DestroyableSingleton<FriendsListManager>.Instance.IsPlayerBlockedUsername(client.FriendCode) && AmongUsClient.Instance.AmHost)
            {
                AmongUsClient.Instance.KickPlayer(client.Id, true);
                MoreToolsPlugin.Logger.LogInfo($"被封禁的玩家 {client?.PlayerName}({client.FriendCode}) 已被Ban");
            }
            BanManager.CheckBanPlayer(client);
        }
    }
}
