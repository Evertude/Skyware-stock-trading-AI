using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace SpiritMod.NPCs
{
    public class CogSpider : ModNPC
    {
        public override void SetDefaults()
        {
            npc.name = "Cog Spider";
            npc.displayName = "Cog Spider";
            npc.width = 44;
            npc.height = 30;
            npc.damage = 23;
            npc.defense = 12;
            npc.lifeMax = 60;
            npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath6;
            npc.value = 460f;
            npc.knockBackResist = .25f;
            npc.aiStyle = 1;
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.BlueSlime];
            aiType = NPCID.BlueSlime;
            animationType = NPCID.BlueSlime;
        }
        public override float CanSpawn(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.spawnTileY < Main.rockLayer && NPC.downedBoss3 && !Main.dayTime && !spawnInfo.playerSafe && !spawnInfo.invasion && !spawnInfo.sky && !Main.eclipse ? 0.03f : 0f;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 226, hitDirection, -1f, 0, default(Color), 1f);
            }
            if (npc.life <= 0)
            {
                for (int k = 0; k < 10; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 226, hitDirection, -1f, 0, default(Color), 1f);
                }
            }
        }
        public override void NPCLoot()
        {
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("StarEnergy"));
            }
        }
        public override void AI()
        {
            npc.spriteDirection = npc.direction;
            {
                if (Main.rand.Next(150) == 6) //Fires desert feathers like a shotgun
                {
                    Vector2 direction = Main.player[npc.target].Center - npc.Center;
                    direction.Normalize();
                    direction.X *= 4f;
                    direction.Y *= 4f;

                    int amountOfProjectiles = Main.rand.Next(1, 1);
                    for (int i = 0; i < amountOfProjectiles; ++i)
                    {
                        float A = (float)Main.rand.Next(-150, 150) * 0.01f;
                        float B = (float)Main.rand.Next(-150, 150) * 0.01f;
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X + A, direction.Y + B, mod.ProjectileType("Starshock"), 15, 1, Main.myPlayer, 0, 0);
                    }
                }
            }
        }
    }
}
