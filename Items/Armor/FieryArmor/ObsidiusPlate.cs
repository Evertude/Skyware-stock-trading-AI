using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Items.Armor.FieryArmor
{
    public class ObsidiusPlate : ModItem
    {
        public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
        {
            equips.Add(EquipType.Body);
            return true;
        }

        public override void SetDefaults()
        {
            item.name = "Fiery Chestplate";
            item.width = 30;
            item.height = 20;
            AddTooltip("Increases ranged and throwing damage by 6%");
            item.value = Terraria.Item.sellPrice(0, 0, 35, 0);
            item.rare = 3;
            item.defense = 8;
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedDamage += 0.06f;
            player.thrownDamage += 0.06f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "CarvedRock", 21);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
