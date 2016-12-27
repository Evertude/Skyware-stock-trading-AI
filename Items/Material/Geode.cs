using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Items.Material
{
    public class Geode : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Geode";
            item.width = 24;
            item.height = 28;
            item.toolTip = "'Shinnnnnnnnnyyyyy'";
            item.value = 100;
            item.rare = 4;

            item.maxStack = 999;            
        }

        public override void AddRecipes() 
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Ruby);
            recipe.AddIngredient(ItemID.Emerald);
            recipe.AddIngredient(ItemID.Sapphire);
			recipe.AddIngredient(ItemID.SoulofLight);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}