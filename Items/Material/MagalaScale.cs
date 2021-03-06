using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Items.Material
{
    public class MagalaScale : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Gore Magala Scale";
            item.width = 30;
            item.height = 24;
            item.value = 2100;
            item.rare = 6;
            item.toolTip = "Maybe you'll get a plate next time.";


            item.maxStack = 999;
        }
        
        public override void AddRecipes() 
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofNight, 2);
            recipe.AddIngredient(ItemID.HallowedBar);
            recipe.AddIngredient(ItemID.DefenderMedal);
            recipe.AddTile(TileID.AdamantiteForge);
            recipe.SetResult(this, 6);
            recipe.AddRecipe();
        }
    }
}