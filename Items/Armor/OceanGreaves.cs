using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Items.Armor
{
    public class OceanGreaves : ModItem
    {
        public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
        {
            equips.Add(EquipType.Legs);
            return true;
        }

        public override void SetDefaults()
        {
            item.name = "Diver's Greaves";
            item.width = 22;
            item.height = 18;
            AddTooltip("Increases magic and minion damage by 6%, reduces mana cost by 5% and maximum mana by 10 \n Increases maximum number of minions by 1");
            item.value = Terraria.Item.sellPrice(0, 0, 31, 0);
            item.rare = 3;
            item.defense = 4;
        }

    public override void UpdateEquip(Player player)
    {
            player.minionDamage += 0.06f;
            player.magicDamage += 0.06f;
            player.statManaMax2 += 10;
            player.maxMinions += 1;
            player.manaCost -= 0.05f;
    }

    public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Coral, 6);
            recipe.AddIngredient(null, "PearlFragment", 11);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }       
    }
}