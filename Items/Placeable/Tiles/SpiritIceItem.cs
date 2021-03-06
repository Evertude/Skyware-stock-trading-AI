using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Items.Placeable.Tiles
{
	public class SpiritIceItem : ModItem
	{
		public override void SetDefaults()
		{
			item.name = "Spirit Ice";
			item.width = 16;
			item.height = 14;

			item.maxStack = 999;

            item.useStyle = 1;
			item.useTime = 10;
            item.useAnimation = 15;

            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;

            item.createTile = mod.TileType("SpiritIce");
		}
	}
}