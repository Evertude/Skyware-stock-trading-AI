using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Items.Material
{
    public class Chitin : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Chitin";
            item.toolTip = "'Surprisingly beautiful beetle scales'";
            item.width = 24;
            item.height = 28;
            item.value = 20;
            item.rare = 1;

            item.maxStack = 999;
        }
    }
}