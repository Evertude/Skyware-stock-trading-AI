using System;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Items.Weapon.Summon
{
	public class BioStaff : ModItem
	{
		public override void SetDefaults()
		{
			item.name = "Bio Staff";
            item.width = 26;
            item.height = 28;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 5;

            item.crit = 4;
            item.mana = 7;
            item.damage = 32;
            item.knockBack = 0;

            item.useStyle = 1;
            item.useTime = 30;
            item.useAnimation = 30;
            
            item.summon = true;
            item.noMelee = true;

            item.shoot = mod.ProjectileType("CarnivorousPlantMinion");
            item.buffType = mod.BuffType("CarnivorousPlantMinionBuff");
            item.buffTime = 3600;

            item.useSound = 44;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position = Main.MouseWorld;
            speedX = speedY = 0;
            return true;
        }
    }
}