using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Items.Accessory
{
    public class FrigidWind : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Frigid Wind";
            item.width = 34;
            item.height = 30;
            item.toolTip = "Greatly increases jump height \n Leave a trail of chilly embers as you walk";
            item.value = 100000;
            item.rare = 5;
            item.defense = 2;
            item.accessory = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.jumpSpeedBoost += 6f;
            ((MyPlayer)player.GetModPlayer(mod, "MyPlayer")).icytrail = true;
        }
    }
}
