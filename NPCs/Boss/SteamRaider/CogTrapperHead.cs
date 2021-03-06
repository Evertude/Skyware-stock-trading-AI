﻿using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SpiritMod.Projectiles;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;
using SpiritMod.Tiles;
using SpiritMod;

namespace SpiritMod.NPCs.Boss.SteamRaider
{
	public class CogTrapperHead : ModNPC
	{
		public bool flies = true;
		public bool directional = false;
		public float speed = 6.5f;
		public float turnSpeed = 0.125f;
		public bool tail = false;
		public int minLength = 15;
		public int maxLength = 16;
		
		public override void SetDefaults()
		{
			npc.name = "Cog Trapper";
			npc.displayName = "Cog Trapper";
			npc.damage = 32; //150
			npc.npcSlots = 1f;
			npc.width = 26; //324
			npc.height = 26; //216
			npc.defense = 5;
			npc.lifeMax = 1500; //250000
			npc.aiStyle = 6; //new
			Main.npcFrameCount[npc.type] = 1; //new
            aiType = -1; //new
            animationType = 10; //new
			npc.knockBackResist = 0f;
			npc.value = Item.buyPrice(0, 0, 5, 0);
			npc.alpha = 255;
			npc.behindTiles = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath14;
			npc.netAlways = true;
			for (int k = 0; k < npc.buffImmune.Length; k++)
			{
				npc.buffImmune[k] = true;
			}
		}
		
		public override void AI()
		{
			Player player = Main.player[npc.target];
			bool expertMode = Main.expertMode;
			Lighting.AddLight((int)((npc.position.X + (float)(npc.width / 2)) / 16f), (int)((npc.position.Y + (float)(npc.height / 2)) / 16f), 0f, 0.0375f, 0.125f);
			if (npc.ai[3] > 0f)
			{
				npc.realLife = (int)npc.ai[3];
			}
			if (npc.target < 0 || npc.target == 255 || player.dead)
			{
				npc.TargetClosest(true);
			}
			npc.velocity.Length();
			if (npc.alpha != 0)
			{
				for (int num934 = 0; num934 < 2; num934++)
				{
					int num935 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 226, 0f, 0f, 100, default(Color), 1f);
					Main.dust[num935].noGravity = true;
					Main.dust[num935].noLight = true;
				}
			}
			npc.alpha -= 12;
			if (npc.alpha < 0)
			{
				npc.alpha = 0;
			}
			if (Main.netMode != 1)
            {
	            if (!tail && npc.ai[0] == 0f)
				{
	            	int Previous = npc.whoAmI;
					for (int num36 = 0; num36 < maxLength; num36++)
	                {
	                    int lol = 0;
	                    if (num36 >= 0 && num36 < minLength)
	                    {
	                        lol = NPC.NewNPC((int)npc.position.X + (npc.width / 2), (int)npc.position.Y + (npc.height / 2), mod.NPCType("CogTrapperBody"), npc.whoAmI);
	                    }
	                    else
	                    {
	                        lol = NPC.NewNPC((int)npc.position.X + (npc.width / 2), (int)npc.position.Y + (npc.height / 2), mod.NPCType("CogTrapperTail"), npc.whoAmI);
	                    }
	                    Main.npc[lol].realLife = npc.whoAmI;
	                    Main.npc[lol].ai[2] = (float)npc.whoAmI;
	                    Main.npc[lol].ai[1] = (float)Previous;
	                    Main.npc[Previous].ai[0] = (float)lol;
						npc.netUpdate = true;
						Previous = lol;
					}
					tail = true;
	            }
                if (!npc.active && Main.netMode == 2)
				{
					NetMessage.SendData(28, -1, -1, "", npc.whoAmI, -1f, 0f, 0f, 0, 0, 0);
				}
            }
			int num180 = (int)(npc.position.X / 16f) - 1;
			int num181 = (int)((npc.position.X + (float)npc.width) / 16f) + 2;
			int num182 = (int)(npc.position.Y / 16f) - 1;
			int num183 = (int)((npc.position.Y + (float)npc.height) / 16f) + 2;
			if (num180 < 0)
			{
				num180 = 0;
			}
			if (num181 > Main.maxTilesX)
			{
				num181 = Main.maxTilesX;
			}
			if (num182 < 0)
			{
				num182 = 0;
			}
			if (num183 > Main.maxTilesY)
			{
				num183 = Main.maxTilesY;
			}
			bool flag94 = flies;
			npc.localAI[1] = 0f;
			if (directional)
			{
				if (npc.velocity.X < 0f)
				{
					npc.spriteDirection = 1;
				}
				else if (npc.velocity.X > 0f)
				{
					npc.spriteDirection = -1;
				}
			}
			if (player.dead)
			{
				npc.TargetClosest(false);
				flag94 = false;
				npc.velocity.Y = npc.velocity.Y + 10f;
				if ((double)npc.position.Y > Main.worldSurface * 16.0)
				{
					npc.velocity.Y = npc.velocity.Y + 10f;
				}
				if ((double)npc.position.Y > Main.rockLayer * 16.0)
				{
					for (int num957 = 0; num957 < 200; num957++)
					{
						if (Main.npc[num957].aiStyle == npc.aiStyle)
						{
							Main.npc[num957].active = false;
						}
					}
				}
			}
			float num188 = speed;
			float num189 = turnSpeed;
			Vector2 vector18 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
			float num191 = player.position.X + (float)(player.width / 2);
			float num192 = player.position.Y + (float)(player.height / 2);
			int num42 = -1;
			int num43 = (int)(player.Center.X / 16f);
			int num44 = (int)(player.Center.Y / 16f);
			for (int num45 = num43 - 2; num45 <= num43 + 2; num45++)
			{
				for (int num46 = num44; num46 <= num44 + 15; num46++)
				{
					if (WorldGen.SolidTile2(num45, num46))
					{
						num42 = num46;
						break;
					}
				}
				if (num42 > 0)
				{
					break;
				}
			}
			if (num42 > 0)
			{
				npc.defense = 5;
				num42 *= 16;
				float num47 = (float)(num42 - 200); //was 800
				if (player.position.Y > num47)
				{
					num192 = num47;
					if (Math.Abs(npc.Center.X - player.Center.X) < 125f) //was 500
					{
						if (npc.velocity.X > 0f)
						{
							num191 = player.Center.X + 150f; //was 600
						}
						else
						{
							num191 = player.Center.X - 150f; //was 600
						}
					}
				}
			}
			else
			{
				npc.defense = 0;
				num188 = expertMode ? 10.83f : 8.66f; //added 2.5
				num189 = expertMode ? 0.208f : 0.166f; //added 0.05
			}
			float num48 = num188 * 1.3f;
			float num49 = num188 * 0.7f;
			float num50 = npc.velocity.Length();
			if (num50 > 0f)
			{
				if (num50 > num48)
				{
					npc.velocity.Normalize();
					npc.velocity *= num48;
				}
				else if (num50 < num49)
				{
					npc.velocity.Normalize();
					npc.velocity *= num49;
				}
			}
			if (num42 > 0)
			{
				for (int num51 = 0; num51 < 200; num51++)
				{
					if (Main.npc[num51].active && Main.npc[num51].type == npc.type && num51 != npc.whoAmI)
					{
						Vector2 vector3 = Main.npc[num51].Center - npc.Center;
						if (vector3.Length() < 400f)
						{
							vector3.Normalize();
							vector3 *= 1000f;
							num191 -= vector3.X;
							num192 -= vector3.Y;
						}
					}
				}
			}
			else
			{
				for (int num52 = 0; num52 < 200; num52++)
				{
					if (Main.npc[num52].active && Main.npc[num52].type == npc.type && num52 != npc.whoAmI)
					{
						Vector2 vector4 = Main.npc[num52].Center - npc.Center;
						if (vector4.Length() < 60f)
						{
							vector4.Normalize();
							vector4 *= 200f;
							num191 -= vector4.X;
							num192 -= vector4.Y;
						}
					}
				}
			}
			num191 = (float)((int)(num191 / 16f) * 16);
			num192 = (float)((int)(num192 / 16f) * 16);
			vector18.X = (float)((int)(vector18.X / 16f) * 16);
			vector18.Y = (float)((int)(vector18.Y / 16f) * 16);
			num191 -= vector18.X;
			num192 -= vector18.Y;
			float num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
			if (npc.ai[1] > 0f && npc.ai[1] < (float)Main.npc.Length)
			{
				try
				{
					vector18 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
					num191 = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - vector18.X;
					num192 = Main.npc[(int)npc.ai[1]].position.Y + (float)(Main.npc[(int)npc.ai[1]].height / 2) - vector18.Y;
				}
				catch
				{
				}
				npc.rotation = (float)System.Math.Atan2((double)num192, (double)num191) + 1.57f;
				num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
				int num194 = npc.width;
				num193 = (num193 - (float)num194) / num193;
				num191 *= num193;
				num192 *= num193;
				npc.velocity = Vector2.Zero;
				npc.position.X = npc.position.X + num191;
				npc.position.Y = npc.position.Y + num192;
				if (directional)
				{
					if (num191 < 0f)
					{
						npc.spriteDirection = 1;
					}
					if (num191 > 0f)
					{
						npc.spriteDirection = -1;
					}
				}
			}
			else
			{
				num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
				float num196 = System.Math.Abs(num191);
				float num197 = System.Math.Abs(num192);
				float num198 = num188 / num193;
				num191 *= num198;
				num192 *= num198;
				bool flag21 = false;
				if (!flag21)
				{
					if ((npc.velocity.X > 0f && num191 > 0f) || (npc.velocity.X < 0f && num191 < 0f) || (npc.velocity.Y > 0f && num192 > 0f) || (npc.velocity.Y < 0f && num192 < 0f))
					{
						if (npc.velocity.X < num191)
						{
							npc.velocity.X = npc.velocity.X + num189;
						}
						else
						{
							if (npc.velocity.X > num191)
							{
								npc.velocity.X = npc.velocity.X - num189;
							}
						}
						if (npc.velocity.Y < num192)
						{
							npc.velocity.Y = npc.velocity.Y + num189;
						}
						else
						{
							if (npc.velocity.Y > num192)
							{
								npc.velocity.Y = npc.velocity.Y - num189;
							}
						}
						if ((double)System.Math.Abs(num192) < (double)num188 * 0.2 && ((npc.velocity.X > 0f && num191 < 0f) || (npc.velocity.X < 0f && num191 > 0f)))
						{
							if (npc.velocity.Y > 0f)
							{
								npc.velocity.Y = npc.velocity.Y + num189 * 2f;
							}
							else
							{
								npc.velocity.Y = npc.velocity.Y - num189 * 2f;
							}
						}
						if ((double)System.Math.Abs(num191) < (double)num188 * 0.2 && ((npc.velocity.Y > 0f && num192 < 0f) || (npc.velocity.Y < 0f && num192 > 0f)))
						{
							if (npc.velocity.X > 0f)
							{
								npc.velocity.X = npc.velocity.X + num189 * 2f; //changed from 2
							}
							else
							{
								npc.velocity.X = npc.velocity.X - num189 * 2f; //changed from 2
							}
						}
					}
					else
					{
						if (num196 > num197)
						{
							if (npc.velocity.X < num191)
							{
								npc.velocity.X = npc.velocity.X + num189 * 1.1f; //changed from 1.1
							}
							else if (npc.velocity.X > num191)
							{
								npc.velocity.X = npc.velocity.X - num189 * 1.1f; //changed from 1.1
							}
							if ((double)(System.Math.Abs(npc.velocity.X) + System.Math.Abs(npc.velocity.Y)) < (double)num188 * 0.5)
							{
								if (npc.velocity.Y > 0f)
								{
									npc.velocity.Y = npc.velocity.Y + num189;
								}
								else
								{
									npc.velocity.Y = npc.velocity.Y - num189;
								}
							}
						}
						else
						{
							if (npc.velocity.Y < num192)
							{
								npc.velocity.Y = npc.velocity.Y + num189 * 1.1f;
							}
							else if (npc.velocity.Y > num192)
							{
								npc.velocity.Y = npc.velocity.Y - num189 * 1.1f;
							}
							if ((double)(System.Math.Abs(npc.velocity.X) + System.Math.Abs(npc.velocity.Y)) < (double)num188 * 0.5)
							{
								if (npc.velocity.X > 0f)
								{
									npc.velocity.X = npc.velocity.X + num189;
								}
								else
								{
									npc.velocity.X = npc.velocity.X - num189;
								}
							}
						}
					}
				}
			}
			npc.rotation = (float)System.Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) + 1.57f;
		}
		
		public override void HitEffect(int hitDirection, double damage)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(npc.position, npc.width, npc.height, 226, hitDirection, -1f, 0, default(Color), 1f);
			}
			if (npc.life <= 0)
			{
				npc.position.X = npc.position.X + (float)(npc.width / 2);
				npc.position.Y = npc.position.Y + (float)(npc.height / 2);
				npc.width = 20;
				npc.height = 20;
				npc.position.X = npc.position.X - (float)(npc.width / 2);
				npc.position.Y = npc.position.Y - (float)(npc.height / 2);
				for (int num621 = 0; num621 < 5; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 226, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 10; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 226, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 180, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
		
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);
		}
	}
}