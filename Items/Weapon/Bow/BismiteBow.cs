using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace SpiritMod.Items.Weapon.Bow
{
    public class BismiteBow : ModItem
    {

        public override void SetDefaults()
        {
            item.name = "Bismite Bow";
            item.damage = 9;
            item.noMelee = true;
            item.ranged = true;
            item.width = 26;
            item.height = 62;
            item.toolTip = "Shoots two arrows upon use";
            item.useTime = 18;
			item.useAnimation = 22;
            item.useStyle = 5;
            item.shoot = 3;
            item.useAmmo = AmmoID.Arrow;
            item.knockBack = 3;
            item.useTurn = true;
            item.value = Terraria.Item.sellPrice(0, 0, 20, 0);
            item.rare = 2;
            item.UseSound = SoundID.Item5;          
            item.autoReuse = false;
            item.shootSpeed = 6.5f;
            item.crit = 8;
			item.reuseDelay = 20;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "BismiteCrystal", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}