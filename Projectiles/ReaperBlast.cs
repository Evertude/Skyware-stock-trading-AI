﻿using System;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Projectiles
{
	class ReaperBlast : ModProjectile
	{

		public override void SetDefaults()
		{
            projectile.name = "Hellfire Blast";
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.penetrate = 2;
            projectile.tileCollide = false;
            projectile.timeLeft = 300;
            projectile.height = 8;
            projectile.width = 8;
            projectile.alpha = 255;
            aiType = ProjectileID.Bullet;
            projectile.extraUpdates = 1;
        }

		public override void AI()
		{
			float rotationSpeed = (float)Math.PI / 15;
            projectile.rotation += rotationSpeed;
            {
                {
                    int dust = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 109, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
                    int dust2 = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 109, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust2].noGravity = true;
                    Main.dust[dust2].velocity *= 0f;
                    Main.dust[dust2].velocity *= 0f;
                    Main.dust[dust2].scale = 1.2f;
                    Main.dust[dust].scale = 1.2f;
                }
            }
		}
	}
}
