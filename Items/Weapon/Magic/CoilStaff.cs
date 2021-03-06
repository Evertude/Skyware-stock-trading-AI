using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
namespace SpiritMod.Items.Weapon.Magic
{
	public class CoilStaff : ModItem
	{
		public override void SetDefaults()
		{
			item.name = "Coil Mine Staff";
			item.width = 48;
			item.height = 50;			
			item.value = Item.buyPrice(0, 0, 30, 0);
			item.rare = 2;
			item.damage = 17;
			item.useStyle = 5;
			Item.staff[item.type] = true;
			item.useTime = 25;
			item.useAnimation = 25;
			item.mana = 11;
            item.toolTip = "Shoots out a detonating coil mine \n Only two mines can exist at once \n Occasionally burns foes";
            item.knockBack = 3;
			item.magic = true;
			item.noMelee = true;
			item.shoot = mod.ProjectileType("CoilMine");
			item.shootSpeed = 10f;
		}

        public override void AddRecipes()
        {
            ModRecipe modRecipe = new ModRecipe(mod);
            modRecipe.AddIngredient(null, "TechDrive", 15);
            modRecipe.AddTile(TileID.Anvils);
            modRecipe.SetResult(this);
            modRecipe.AddRecipe();
        }
    }
}
