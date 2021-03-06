using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Buffs
{
    public class Wraith : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffName[this.Type] = "Wraith";
            Main.buffTip[Type] = "You are invincible and speedy";

            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed += 0.55f;
            player.longInvince = true;
            player.jumpSpeedBoost += 3f;
            {

                Dust.NewDust(player.position, player.width, player.height, 109);
                Dust.NewDust(player.position, player.width, player.height, 109);
            }
        }
    }
}
