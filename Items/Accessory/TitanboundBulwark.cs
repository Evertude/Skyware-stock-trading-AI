using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System;
using System.Collections.Generic;

namespace SpiritMod.Items.Accessory
{
	public class TitanboundBulwark : ModItem
	{
		public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
        {
            equips.Add(EquipType.Shield);
            return true;
        }
		public override void SetDefaults()
		{
			item.name = "Titanbound Bulwark";
			item.toolTip = "As your health decreases, your mana regeneration increases \n Reduces damage taken by 10% \n Increases life regeneration";
			item.width = 18;
			item.height = 18;
			item.value = Item.buyPrice(0,51, 0, 0);
			item.rare = 9;
			item.accessory = true;
			item.defense = 2;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			float manaBoost = (float)(player.statLifeMax2 - player.statLife) / (float)player.statLifeMax2 * 50f;
			player.manaRegen += (int)manaBoost;
            player.endurance += .1f;
            player.lifeRegen += 3;
		}
	}
}
