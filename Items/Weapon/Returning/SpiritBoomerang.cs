using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Items.Weapon.Returning
{
	public class SpiritBoomerang : ModItem
	{
        public override void SetDefaults()
        {
            item.name = "Spirit Boomerang";
            item.damage = 54;
            item.melee = true;
            item.width = 40;
            item.height = 40;
            item.toolTip = "'Shoots out three boomerangs that inflict Soul Burn!'";
            item.useTime = 30;
            item.useAnimation = 30;
            item.noUseGraphic = true;
            item.useStyle = 1;
            item.knockBack = 3;
            item.value = Terraria.Item.sellPrice(0, 1, 20, 0);
            item.rare = 5;
            item.shootSpeed = 9f;
            item.shoot = mod.ProjectileType("SpiritBoomerang");
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int I = 0; I < 3; I++)
            {
                Projectile.NewProjectile(position.X - 8, position.Y + 8, speedX + ((float)Main.rand.Next(-250, 250) / 100), speedY + ((float)Main.rand.Next(-250, 250) / 100), type, damage, knockBack, player.whoAmI, 0f, 0f);
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "SpiritBar", 16);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}