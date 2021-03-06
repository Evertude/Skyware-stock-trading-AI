﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.NPCs.Boss.Atlas
{
	public class Atlas : ModNPC
	{
		int[] arms = new int[2];
		int timer = 0;
		bool secondStage = false;
		bool thirdStage = false;
		bool lastStage = false;
		int collideTimer = 0;
		
		public override void SetDefaults()
		{
			npc.name = "Atlas";
			npc.width = 250;
			npc.height = 400;
			bossBag = mod.ItemType("AtlasBag");
			npc.damage = 100;
			npc.lifeMax = 45000;
			npc.defense = 15;
			npc.knockBackResist = 0f;
			npc.boss = true;
			npc.noGravity = true;
			npc.alpha = 255;
			npc.HitSound = SoundID.NPCHit7;
			npc.DeathSound = SoundID.NPCDeath5;
			music = MusicID.Boss4;
		}

		private int Counter;
		public override void AI()
		{
			bool expertMode = Main.expertMode; //expert mode bool
			Player player = Main.player[npc.target]; //player target
			bool aiChange = (double)npc.life <= (double)npc.lifeMax * 0.75; //ai change to phase 2
			bool aiChange2 = (double)npc.life <= (double)npc.lifeMax * 0.5; //ai change to phase 3
			bool aiChange3 = (double)npc.life <= (double)npc.lifeMax * 0.25; //ai change to phase 4
			bool phaseChange = (double)npc.life <= (double)npc.lifeMax * 0.1; //aggression increase
			player.AddBuff(mod.BuffType("UnstableAffliction"), 2); //buff that causes gravity shit
			int defenseBuff = (int)(65f * (1f - (float)npc.life / (float)npc.lifeMax));
			npc.defense = npc.defDefense + defenseBuff;
			if (npc.ai[0] == 0f)
			{
				npc.dontTakeDamage = true;
				arms[0] = NPC.NewNPC((int)npc.Center.X - 80 - Main.rand.Next(80, 160), (int)npc.position.Y, mod.NPCType("AtlasArmLeft"), npc.whoAmI, 0, 0, 0, -1);
				arms[1] = NPC.NewNPC((int)npc.Center.X + 80 + Main.rand.Next(80, 160), (int)npc.position.Y, mod.NPCType("AtlasArmRight"), npc.whoAmI, 0, 0, 0, 1);
				npc.ai[0] = 1f;
			}
			else if (npc.ai[0] == 1f)
			{
				npc.ai[1] += 1f;
				if (npc.ai[1] >= 210f)
				{
					npc.alpha -= 4;
					if (npc.alpha <= 0)
					{
						npc.ai[0] = 2f;
						npc.ai[1] = 0f;
						npc.alpha = 0;
						npc.velocity.Y = 14f;
						npc.dontTakeDamage = false;
						npc.netUpdate = true;
					}
				}
			}
			else if (npc.ai[0] == 2f)
			{
				if (npc.alpha == 0)
				{
					Vector2 dist = player.Center - npc.Center;
					Vector2 direction = player.Center - npc.Center;
					npc.netUpdate = true;
					npc.TargetClosest(true);
					if (!player.active || player.dead)
					{
						npc.TargetClosest(false);
						npc.velocity.Y = -100f;
					}
					#region Dashing mechanics
					//dash if player is too far away
					if (Math.Sqrt((dist.X * dist.X) + (dist.Y * dist.Y)) > 455)
					{
						direction.Normalize();
						npc.velocity *= 0.98f;
						if (Math.Sqrt((npc.velocity.X * npc.velocity.X) + (npc.velocity.Y * npc.velocity.Y)) >= 7f)
						{
							int dust = Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, 1, npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
							Main.dust[dust].noGravity = true;
							dust = Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, 1, npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
							Main.dust[dust].noGravity = true;
						}
						if (Math.Sqrt((npc.velocity.X * npc.velocity.X) + (npc.velocity.Y * npc.velocity.Y)) < 2f)
						{
							if (Main.rand.Next(25) == 1)
							{
								direction.X = direction.X * Main.rand.Next(15, 19);
								direction.Y = direction.Y * Main.rand.Next(15, 19);
								npc.velocity.X = direction.X;
								npc.velocity.Y = direction.Y;
							}
						}
					}
					#endregion
					#region Flying Movement
					if (Math.Sqrt((dist.X * dist.X) + (dist.Y * dist.Y)) < 325)
					{
						float speed = expertMode ? 15f : 12f; //made more aggressive.  expert mode is more.  dusking base value is 7
						float acceleration = expertMode ? 0.15f : 0.12f; //made more aggressive.  expert mode is more.  dusking base value is 0.09
						Vector2 vector2 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
						float xDir = player.position.X + (float)(player.width / 2) - vector2.X;
						float yDir = (float)(player.position.Y + (player.height / 2) - 120) - vector2.Y;
						float length = (float)Math.Sqrt(xDir * xDir + yDir * yDir);
						if (length > 400f)
						{
							++speed;
							acceleration += 0.05F;
							if (length > 600f)
							{
								++speed;
								acceleration += 0.05F;
								if (length > 800f)
								{
									++speed;
									acceleration += 0.05F;
								}
							}
						}
						float num10 = speed / length;
						xDir = xDir * num10;
						yDir = yDir * num10;
						if (npc.velocity.X < xDir)
						{
							npc.velocity.X = npc.velocity.X + acceleration;
							if (npc.velocity.X < 0 && xDir > 0)
							{
								npc.velocity.X = npc.velocity.X + acceleration;
							}
						}
						else if (npc.velocity.X > xDir)
						{
							npc.velocity.X = npc.velocity.X - acceleration;
							if (npc.velocity.X > 0 && xDir < 0)
							{
								npc.velocity.X = npc.velocity.X - acceleration;
							}
						}
						if (npc.velocity.Y < yDir)
						{
							npc.velocity.Y = npc.velocity.Y + acceleration;
							if (npc.velocity.Y < 0 && yDir > 0)
							{
								npc.velocity.Y = npc.velocity.Y + acceleration;
							}
						}
						else if (npc.velocity.Y > yDir)
						{
							npc.velocity.Y = npc.velocity.Y - acceleration;
							if (npc.velocity.Y > 0 && yDir < 0)
							{
								npc.velocity.Y = npc.velocity.Y - acceleration;
							}
						}
					}
					#endregion
					timer += phaseChange ? 2 : 1; //if below 20% life fire more often
					int shootThings = expertMode ? 200 : 250; //fire more often in expert mode
					if (timer > shootThings)
					{
						direction.Normalize();
						direction.X *= 8f;
						direction.Y *= 8f;
						int amountOfProjectiles = Main.rand.Next(5, 7);
						int damageAmount = expertMode ? 42 : 68; //always account for expert damage values
						Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 92);
						for (int num621 = 0; num621 < 30; num621++)
						{
							int num622 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 226, 0f, 0f, 100, default(Color), 2f);
						}
						for (int i = 0; i < amountOfProjectiles; ++i)
						{
							float A = (float)Main.rand.Next(-250, 250) * 0.01f;
							float B = (float)Main.rand.Next(-250, 250) * 0.01f;
							Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X + A, direction.Y + B, mod.ProjectileType("PrismaticBoltHostile"), damageAmount, 1, npc.target, 0, 0);
							timer = 0;
						}
					}
					if (aiChange)
					{
						if (secondStage == false)
						{
							Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 93);
							for (int I = 0; I < 5; I++)
							{
								//cos = y, sin = x
								int GeyserEye = NPC.NewNPC((int)(npc.Center.X + (Math.Sin(I * 72) * 250)), (int)(npc.Center.Y + (Math.Cos(I * 72) * 250)), mod.NPCType("CobbledEye"), npc.whoAmI, 0, 0, 0, -1);
								NPC Eye = Main.npc[GeyserEye];
								Eye.ai[0] = I * 72;
								Eye.ai[3] = I * 72;
							}
							secondStage = true;
						}
					}
					if (aiChange2)
					{
						if (thirdStage == false)
						{
							Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 93);
							for (int I = 0; I < 10; I++)
							{
								//cos = y, sin = x
								int GeyserEye = NPC.NewNPC((int)(npc.Center.X + (Math.Sin(I * 36) * 400)), (int)(npc.Center.Y + (Math.Cos(I * 36) * 400)), mod.NPCType("CobbledEye2"), npc.whoAmI, 0, 0, 0, -1);
								NPC Eye = Main.npc[GeyserEye];
								Eye.ai[0] = I * 36;
								Eye.ai[3] = I * 36;
							}
							thirdStage = true;
						}
					}
					if (aiChange3)
					{
						if (lastStage == false)
						{
							Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 93);
							for (int I = 0; I < 41; I++)
							{
								//cos = y, sin = x
								int GeyserEye = NPC.NewNPC((int)(npc.Center.X + (Math.Sin(I * 18) * 1200)), (int)(npc.Center.Y + (Math.Cos(I * 18) * 1200)), mod.NPCType("CobbledEye3"), npc.whoAmI, 0, 0, 0, -1);
								NPC Eye = Main.npc[GeyserEye];
								Eye.ai[0] = I * 18;
								Eye.ai[3] = I * 18;
							}
							lastStage = true;
						}
					}
				}
			}
			collideTimer++;
			if (collideTimer == 500)
			{
				npc.noTileCollide = true;
			}
			npc.TargetClosest(true);
			if (!player.active || player.dead) 
			{
				npc.TargetClosest(false);
				npc.velocity.Y = -100f;
				timer = 0;
			}
			Counter++;
			if (Counter > 400)
			{
				SpiritMod.shittyModTime = 120;
				Counter = 0;
			}
		}
		
		public override void HitEffect(int hitDirection, double damage)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(npc.position, npc.width, npc.height, 1, hitDirection, -1f, 0, default(Color), 1f);
			}
			if (npc.life <= 0)
			{
				npc.position.X = npc.position.X + (float)(npc.width / 2);
				npc.position.Y = npc.position.Y + (float)(npc.height / 2);
				npc.width = 300;
				npc.height = 500;
				npc.position.X = npc.position.X - (float)(npc.width / 2);
				npc.position.Y = npc.position.Y - (float)(npc.height / 2);
				for (int num621 = 0; num621 < 200; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 1, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 400; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 1, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 1, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
		
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * 0.55f * bossLifeScale);
			npc.damage = (int)(npc.damage * 0.65f);
		}
		
		public override void NPCLoot()
		{
			if (Main.expertMode)
			{
				npc.DropBossBags();
			}
			else
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ArcaneGeyser"), Main.rand.Next(32, 44));
				string[] lootTable = 
				{
					"KingRock",
					"Mountain",
					"TitanboundBulwark",
					"CragboundStaff",
					"QuakeFist",
					"Earthshatter"
				};
				int loot = Main.rand.Next(lootTable.Length);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType(lootTable[loot]));
			}
		}
		
		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			if (npc.ai[0] < 2f)
			{
				return false;
			}
			return base.CanHitPlayer(target, ref cooldownSlot);
		}
	}
}