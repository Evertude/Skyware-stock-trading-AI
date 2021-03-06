﻿using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Items.BossBags
{
    public class IlluminantBag : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Treasure Bag";
			item.width = 20;
            item.height = 20;
            item.toolTip = "Right Click to open";
            item.rare = -2;

            item.maxStack = 30;

			item.expert = true;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
		{
			     player.QuickSpawnItem(mod.ItemType("CrystalShield")); 
			string[] lootTable = { "SylphBow", "FairystarStaff", "FaeSaber", };
			int loot = Main.rand.Next(lootTable.Length);
                  player.QuickSpawnItem(mod.ItemType("IlluminatedCrystal"), Main.rand.Next(32, 44));
			player.QuickSpawnItem(mod.ItemType(lootTable[loot]));
        }
    }
}
