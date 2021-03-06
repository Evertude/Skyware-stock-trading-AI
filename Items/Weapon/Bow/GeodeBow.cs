using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using SpiritMod.Projectiles;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace SpiritMod.Items.Weapon.Bow
{
    public class GeodeBow : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Geode Bownanza";
            item.damage = 31;
            item.ranged = true;
            item.toolTip = "Critical hits inflict a multitude of debuffs";
            item.width = 36;
            item.height = 36;
            item.useTime = 26;
            item.useAnimation = 50;            
            item.useStyle = 5;
			item.shoot = 3;
			item.shootSpeed = 10f;
            item.knockBack = 7;
            item.value = Terraria.Item.sellPrice(0, 0, 80, 0);
            item.rare = 5;
            item.UseSound = SoundID.Item5;   
            item.autoReuse = false;
			item.useAmmo = AmmoID.Arrow;
            item.crit = 2;
            item.useTurn = true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int p = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
            Main.projectile[p].GetModInfo<SpiritProjectileInfo>(mod).shotFromGeodeBow = true;
            return false;
        }

        public override void AddRecipes()  
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Geode", 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
