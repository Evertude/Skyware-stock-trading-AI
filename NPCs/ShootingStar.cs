using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.NPCs
{
    public class ShootingStar : ModNPC
    {
        public override void SetDefaults()
        {
            npc.name = "Shooting Star";
            npc.displayName = "Shooting Star";
            npc.width = 24;
            npc.height = 30;
            npc.damage = 50;
            npc.defense = 20;
            npc.lifeMax = 400;
            npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath6;
            npc.value = 60f;
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.aiStyle = 56;
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.ChaosBall];
            aiType = NPCID.DungeonSpirit;
            animationType = NPCID.ChaosBall;
        }

        public override float CanSpawn(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.sky && Main.hardMode ? 0.1f : 0f;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i < 10; i++) ;
        }
        public override void NPCLoot()
		{
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("StarPiece"));
			}
		}
    }
}
