using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Items.Weapon.Magic
{
	public class GeodeStave : ModItem
	{
		public override void SetDefaults()
		{
            
			item.name = "Geode Staff";
			item.damage = 32;
			item.magic = true;
			item.mana = 15;
            item.toolTip = "Shoots a ball of energy that inflicts a multitude of debuffs";
            item.toolTip2 = "The energy may split into a fiery blaze or a cursed inferno";
			item.width = 22;
			item.height = 34;
			item.useTime = 36;
			item.useAnimation = 36;
			item.useStyle = 5;
			Item.staff[item.type] = true; 
			item.noMelee = true; 
			item.knockBack = 3;
            item.useTurn = true;
            item.value = Terraria.Item.sellPrice(0, 0, 80, 0);
            item.rare = 5;
			item.UseSound = SoundID.Item20;
			item.autoReuse = false;
			item.shoot = mod.ProjectileType("GeodeStaveProjectile");
			item.shootSpeed = 12;
        }
        public override void AddRecipes()  
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Geode", 14);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();

        }

	}
}
