using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Items.Accessory
{
	public class TitaniumRing : ModItem
	{
		public override void SetDefaults()
		{
			item.name = "Titanium Band";
			item.width = 18;
			item.height = 18;
            item.toolTip = "Every hit has a chance to give you shadow dodge";
            item.value = Item.buyPrice(0, 10, 0, 0);
			item.rare = 9;

			item.accessory = true;

			item.defense = 2;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetModPlayer<MyPlayer>(mod).TiteRing = true;
		}
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TitaniumBar, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}
