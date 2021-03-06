﻿using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Items.DonatorItems.Folv
{
    public class FolvStaff1 : ModItem
    {
        public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
        {
            equips.Add(EquipType.Balloon);
            return true;
        }

        public override void SetDefaults()
        {
            item.name = "Folv's Staff";
            item.width = 30;
            item.height = 30;
            item.toolTip = "Grants 2% increased magic damage and +10 maximum mana \n ~Donator Item~";
            item.rare = 1;
            item.value = 5000;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statManaMax2 += 10;
            player.magicDamage *= 1.02f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 40);
            recipe.AddIngredient(ItemID.GoldBar, 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();


            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(ItemID.Wood, 40);
            recipe2.AddIngredient(ItemID.PlatinumBar, 2);
            recipe2.AddTile(TileID.Anvils);
            recipe2.SetResult(this);
            recipe2.AddRecipe();
        }
    }
}
