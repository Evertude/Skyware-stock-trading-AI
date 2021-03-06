﻿using System;
using System.Collections.Generic;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Items.Accessory
{
    public class InfernalShield : ModItem
    {
        public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
        {
            equips.Add(EquipType.Shield);
            return true;
        }

        public override void SetDefaults()
        {
            item.name = "Infernal Shield";
            item.width = 24;
            item.height = 28;
            item.rare = 5;
            item.value = 80000;
            item.toolTip = "Double tap a direction to dash in flames \n Reduces damage taken by 5%";
            item.damage = 36;
            item.defense = 3;
            item.melee = true;
            item.accessory = true;

            item.knockBack = 5f;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<MyPlayer>(mod).infernalShield = true;
            player.endurance += 0.05f;
        }
    }
}
