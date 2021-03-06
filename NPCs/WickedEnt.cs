using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.NPCs
{
    public class WickedEnt : ModNPC
    {
        public override void SetDefaults()
        {
            npc.name = "Wicked Ent";
            npc.displayName = "Wicked Ent";
            npc.width = 28;
            npc.height = 52;
            npc.damage = 25;
            npc.defense = 7;
            npc.lifeMax = 75;
            npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath6;
            npc.value = 60f;
            npc.knockBackResist = .20f;
            npc.aiStyle = 3;
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.Zombie];
            aiType = NPCID.FaceMonster;
            animationType = NPCID.Zombie;
        }

        public override float CanSpawn(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.spawnTileY > Main.rockLayer && spawnInfo.player.ZoneJungle ? 0.2f : 0f;
        }
		public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i < 10; i++) ;
            if (npc.life <= 0)
            {
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Ent1"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Reach2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Reach2"), 1f);
            }
        }
        public override void AI()
        {
            if (Main.rand.Next(8) == 1)
            {
                int dust = Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, 44, npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
            }
        }
        public override void NPCLoot()
        {
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.JungleSpores, Main.rand.Next(1) + 3);
            }
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.RichMahogany, Main.rand.Next(4) + 3);
            }
        }
    }
}
