using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Items.Weapon.Swung
{
    public class FaeSaber : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Fae Saber";     
            item.damage = 58;            
            item.melee = true;            
            item.width = 34;              
            item.height = 40;             
            item.toolTip = "Occasionally shoots out a crystalline bolt";  
            item.useTime = 32;           
            item.useAnimation = 32;     
            item.useStyle = 1;        
            item.knockBack = 4;
            item.value = Terraria.Item.sellPrice(0, 5, 0, 0);
            item.rare = 6;
            item.UseSound = SoundID.Item1;         
            item.shoot = mod.ProjectileType("Fae");
            item.shootSpeed = 7f;            
            item.crit = 6;                     
			 item.autoReuse = true;
        }
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
    {
		if (Main.rand.Next(10)> 8)
		{
			return false;
		}
		return true;
	}
    }
}
