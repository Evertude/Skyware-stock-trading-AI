﻿using System;
using System.Collections.Generic;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Items.Accessory
{
    public class HellsGaze : ModItem
    {

        public override void SetDefaults()
        {
            item.name = "Fiery Lash";
            item.width = 24;
            item.toolTip = "Nearby enemies are engulfed by fire";
            item.toolTip2 = "Increases critical strike chance by 6% \n You emit a fiery glow";
            item.height = 28;
            item.rare = 4;
            item.value = 80000;
            item.expert = true;
            item.melee = true;
            item.accessory = true;

            item.knockBack = 9f;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<MyPlayer>(mod).HellGaze = true;
            player.meleeCrit += 6;
            player.rangedCrit += 6;
            player.magicCrit += 6;
            player.thrownCrit += 6;
        }
    }
}
