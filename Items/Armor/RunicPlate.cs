using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Items.Armor
{
    public class RunicPlate : ModItem
    {
        public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
        {
            equips.Add(EquipType.Body);
            return true;
        }

        public override void SetDefaults()
        {
            item.name = "Runic Plate";
            item.width = 34;
            item.height = 30;
            AddTooltip("Increases magic critical strike chance by 8% and movement speed by 10%");
            item.value = 50000;
            item.rare = 5;
            item.defense = 14;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 1.10f;
            player.magicCrit += 8;
        }
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Rune", 16);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}