using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Items.Accessory
{
    public class FierySoul : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Lava Soul";
            item.width = 34;
            item.height = 30;
            item.toolTip = "Getting hurt releases embers";
            item.toolTip2 = "Minions have a chance to burn enemies";
            item.rare = 5;
            item.defense = 2;
            item.value = Terraria.Item.sellPrice(0, 3, 0, 0);
            item.accessory = true;

            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void UpdateEquip(Player player)
        {
            ((MyPlayer)player.GetModPlayer(mod, "MyPlayer")).Fierysoul = true;
        }
    }
}
