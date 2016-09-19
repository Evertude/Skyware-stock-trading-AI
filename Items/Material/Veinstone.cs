using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Items.Material
{
    public class Veinstone : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Veinstone";
            item.width = 24;
            item.height = 28;
            item.value = 100;
            item.rare = 4;

            item.maxStack = 999;
        }
    }
}