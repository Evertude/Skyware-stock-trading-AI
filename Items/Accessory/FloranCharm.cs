using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Items.Accessory
{
    public class FloranCharm : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Floran Hunting Charm";
            item.width = 24;
            item.height = 24;
            item.toolTip = "Increases critical strike chance by 5% and maximum life by 10";
            item.value = Item.buyPrice(0, 0, 20, 0);
            item.rare = 2;
            item.defense = 2;

            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.magicCrit += 5;
            player.meleeCrit += 5;
            player.thrownCrit += 5;
            player.rangedCrit += 5;

            player.statLifeMax2 += 15;
        }
        public override void AddRecipes()
        {
            ModRecipe modRecipe = new ModRecipe(mod);
            modRecipe.AddIngredient(null, "FloranBar", 15);
            modRecipe.AddTile(TileID.Anvils);
            modRecipe.SetResult(this);
            modRecipe.AddRecipe();
        }
    }
}
