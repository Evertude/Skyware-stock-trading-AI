using Terraria;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.NPCs.Boss.IlluminantMaster
{
    public class IlluminantMaster : ModNPC
    {
		int timer = 0;
		int teleportTimer = 0;
        bool text = false;
        public override void SetDefaults()
        {
            npc.name = "Illuminant Master";
            npc.displayName = "Illuminant Master";
            npc.width = 130;
            npc.height = 154;
            npc.damage = 2;
			npc.noTileCollide = true;
			bossBag = mod.ItemType("IlluminantBag");
            npc.defense = 34;
			npc.boss = true;
            npc.lifeMax = 32000;
            npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.noGravity = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Illuminant_Master_v1 (3)");
            npc.value = 60f;
            npc.knockBackResist = 0f;
            Main.npcFrameCount[npc.type] = 7;
			npc.aiStyle = -1;
     
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 62, hitDirection, -1f, 0, default(Color), 1f);
            }
            if (npc.life <= 0)
            {
                npc.position.X = npc.position.X + (float)(npc.width / 2);
                npc.position.Y = npc.position.Y + (float)(npc.height / 2);
                npc.width = 130;
                npc.height = 150;
                npc.position.X = npc.position.X - (float)(npc.width / 2);
                npc.position.Y = npc.position.Y - (float)(npc.height / 2);
                for (int num621 = 0; num621 < 200; num621++)
                {
                    int num622 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 62, 0f, 0f, 100, default(Color), 2f);
                    Main.dust[num622].velocity *= 3f;
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[num622].scale = 0.5f;
                        Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
                    }
                }
                for (int num623 = 0; num623 < 400; num623++)
                {
                    int num624 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 62, 0f, 0f, 100, default(Color), 3f);
                    Main.dust[num624].noGravity = true;
                    Main.dust[num624].velocity *= 5f;
                    num624 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 62, 0f, 0f, 100, default(Color), 2f);
                    Main.dust[num624].velocity *= 2f;
                }
            }
        }
        public override void NPCLoot()
		{
			if (Main.expertMode)
			{
				npc.DropBossBags();
			}
			else
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("IlluminatedCrystal"), Main.rand.Next(32,44));
			string[] lootTable = { "SylphBow", "FairystarStaff", "FaeSaber", "GastropodStaff"};
			int loot = Main.rand.Next(lootTable.Length);
			 Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType(lootTable[loot]));
                
			}
			MyWorld.downedIlluminantMaster = true;
		}
		public override void AI()
        {
			npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            if (!player.active || player.dead || Main.dayTime)
            {
                npc.active = false;
                npc.TargetClosest(false);
                npc.velocity.Y = -100;
				timer = 0;
            }
			
			timer++;
			teleportTimer++;
			
			if (timer == 150) //First Teleport
			{
				for (int i = 0; i < 50; ++i) //Create dust before teleport
					{
					int dust = Dust.NewDust(npc.position, npc.width, npc.height, 62);      
					Main.dust[dust].scale = 1.5f;
					}
				Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("FaeDetonator"), 35, 1, Main.myPlayer, 0, 0); //Make projectilllllelelelelele
				npc.position.X = player.position.X + 500f; //Teleport in a corner of the screen
				npc.position.Y = player.position.Y + 500f; //Moves to you
				Vector2 direction = Main.player[npc.target].Center - npc.Center;
				direction.Normalize();
				npc.velocity.Y = direction.Y * 9f;
				npc.velocity.X = direction.X * 9f;
				
				for (int i = 0; i < 50; ++i) //Create dust after teleport
					{
					int dust = Dust.NewDust(npc.position, npc.width, npc.height, 62);      
					Main.dust[dust].scale = 1.5f;
					}
			}

			if (timer == 300) //Second Teleport
			{
				for (int i = 0; i < 50; ++i) //Create dust before teleport
					{
					int dust = Dust.NewDust(npc.position, npc.width, npc.height, 62);      
					Main.dust[dust].scale = 1.5f;
					}
				Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("FaeDetonator"), 35, 1, Main.myPlayer, 0, 0); //Make projectilllllelelelelele
				npc.position.X = player.position.X - 500f; //Teleport in a corner of the screen
				npc.position.Y = player.position.Y + 500f;
				Vector2 direction = Main.player[npc.target].Center - npc.Center;//Moves to you
				direction.Normalize();
				npc.velocity.Y = direction.Y * 9f;
				npc.velocity.X = direction.X * 9f;
				
				for (int i = 0; i < 50; ++i) //Create dust after teleport
					{
					int dust = Dust.NewDust(npc.position, npc.width, npc.height, 62);      
					Main.dust[dust].scale = 1.5f;
					}
			}

			if (timer == 450) //Third Teleport
			{
				for (int i = 0; i < 50; ++i) //Create dust before teleport
					{
					int dust = Dust.NewDust(npc.position, npc.width, npc.height, 62);      
					Main.dust[dust].scale = 1.5f;
					}
				Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("FaeDetonator"), 35, 1, Main.myPlayer, 0, 0); //Make projectilllllelelelelele
				npc.position.X = player.position.X + 500f; //Teleport in a corner of the screen
				npc.position.Y = player.position.Y - 500f;
				Vector2 direction = Main.player[npc.target].Center - npc.Center;//Moves to you
				direction.Normalize();
				npc.velocity.Y = direction.Y * 9f;
				npc.velocity.X = direction.X * 9f;
				
				for (int i = 0; i < 50; ++i) //Create dust after teleport
					{
					int dust = Dust.NewDust(npc.position, npc.width, npc.height, 62);      
					Main.dust[dust].scale = 1.5f;
					}
			}

			if (timer == 600) //Fourth Teleport
			{
				for (int i = 0; i < 50; ++i) //Create dust before teleport
					{
					int dust = Dust.NewDust(npc.position, npc.width, npc.height, 62);      
					Main.dust[dust].scale = 1.5f;
					}
				Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("FaeDetonator"), 35, 1, Main.myPlayer, 0, 0); //Make projectilllllelelelelele
				npc.position.X = player.position.X - 500f; //Teleport in a corner of the screen
				npc.position.Y = player.position.Y - 500f;
				Vector2 direction = Main.player[npc.target].Center - npc.Center; //Moves to you
				direction.Normalize();
				npc.velocity.Y = direction.Y * 9f;
				npc.velocity.X = direction.X * 9f;
				
				for (int i = 0; i < 50; ++i) //Create dust after teleport
					{
					int dust = Dust.NewDust(npc.position, npc.width, npc.height, 62);      
					Main.dust[dust].scale = 1.5f;
					}
			}

				
				
				if (teleportTimer >= 80 && timer >= 600) //Phase 2 boiiiiii
					{
						for (int i = 0; i < 50; ++i)
						{
						int dust = Dust.NewDust(npc.position, npc.width, npc.height, 62);      
						Main.dust[dust].scale = 1.5f;
						}
						Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("FaeDetonator"), 35, 1, Main.myPlayer, 0, 0); //DETONATE ME
						npc.velocity.X = 0f;
						npc.velocity.Y = 0f;
						int A = Main.rand.Next(-250, 250) * 3;
						int B = Main.rand.Next(-100, 100) - 400;
						npc.position.X = player.Center.X + A;
						npc.position.Y = player.Center.Y + B;
						teleportTimer = 0;
						for (int i = 0; i < 5; ++i)
						{
							Vector2 direction = Main.player[npc.target].Center - npc.Center;
							direction.Normalize();
							float sX = direction.X * 15f;
							float sY = direction.Y * 15f;
							sX += (float)Main.rand.Next(-60, 61) * 0.08f;
							sY += (float)Main.rand.Next(-60, 61) * 0.08f;
							Projectile.NewProjectile(npc.Center.X, npc.Center.Y, sX, sY, mod.ProjectileType("CrystalSpike"), 50, 1, Main.myPlayer, 0, 0);
						}
					}
					
				if (timer == 1200)
				{
					timer = 0;
				}
            if (npc.life <= (npc.lifeMax / 2) && !text)
            {
                Main.NewText("You will be cleansed...", 200, 80, 160, true);
                text = true;
            }
            if (npc.life <= (npc.lifeMax / 2) && (Main.rand.Next(200) == 1))
            {
                Vector2 direction = Main.player[npc.target].Center - npc.Center;
                direction.Normalize();
                direction.X *= 8f;
                direction.Y *= 8f;

                int amountOfProjectiles = Main.rand.Next(10, 15);
                for (int i = 0; i < amountOfProjectiles; ++i)
                {
                    float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                    float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                    int p = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X + A, direction.Y + B, ProjectileID.DD2DrakinShot, 50, 1, Main.myPlayer, 0, 0);
                    Main.projectile[p].friendly = false;
                    Main.projectile[p].hostile = true;
                }
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (npc.velocity != Vector2.Zero)
            {
                Texture2D texture = Main.npcTexture[npc.type];
                Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);

                for (int i = 1; i < npc.oldPos.Length; ++i)
                {
                    Vector2 vector2_2 = npc.oldPos[i];
                    Microsoft.Xna.Framework.Color color2 = Color.White * npc.Opacity;
                    color2.R = (byte)(0.5 * (double)color2.R * (double)(10 - i) / 20.0);
                    color2.G = (byte)(0.5 * (double)color2.G * (double)(10 - i) / 20.0);
                    color2.B = (byte)(0.5 * (double)color2.B * (double)(10 - i) / 20.0);
                    color2.A = (byte)(0.5 * (double)color2.A * (double)(10 - i) / 20.0);
                    Main.spriteBatch.Draw(Main.npcTexture[npc.type], new Vector2(npc.oldPos[i].X - Main.screenPosition.X + (npc.width / 2),
                        npc.oldPos[i].Y - Main.screenPosition.Y + npc.height / 2), new Rectangle?(npc.frame), color2, npc.oldRot[i], origin, npc.scale, SpriteEffects.None, 0.0f);
                }
            }
            return true;
        }
    }
}