using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace SpiritMod.NPCs
{
    public class CElemental : ModNPC
    {
        public override void SetDefaults()
        {
            npc.name = "Crimtane Elemental";
            npc.displayName = "Crimtane Elemental";
            npc.width =30;
            npc.height = 32;
            npc.damage = 29;
            npc.defense = 11;
            npc.lifeMax = 45;
            npc.HitSound = SoundID.NPCHit7;
			npc.DeathSound = SoundID.NPCDeath6;
            npc.value = 1290f;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.knockBackResist = .5f;
            npc.aiStyle = 91;
            aiType = NPCID.GraniteFlyer;
            Main.npcFrameCount[npc.type] = 10;

        }
        public override float CanSpawn(NPCSpawnInfo spawnInfo)
        {
            int x = spawnInfo.spawnTileX;
            int y = spawnInfo.spawnTileY;
            int tile = (int)Main.tile[x, y].type;
            return (tile == 203) && spawnInfo.spawnTileY > Main.rockLayer ? 0.1f : 0f;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i < 10; i++) ;
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, 825);
                Gore.NewGore(npc.position, npc.velocity, 826);
                Gore.NewGore(npc.position, npc.velocity, 827);
            }
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.15f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int frame = (int)npc.frameCounter;
            npc.frame.Y = frame * frameHeight;
        }
        public override void AI()
        {
             Lighting.AddLight((int)((npc.position.X + (float)(npc.width / 2)) / 16f), (int)((npc.position.Y + (float)(npc.height / 2)) / 16f), .27f, 0.1f, 0.06f);


            npc.spriteDirection = npc.direction;

        }
    }
}
