using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace SpiritMod.Projectiles.Magic
{
	public class BlizzardSpike : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.name = "Icicle";
			projectile.width = 12;
			projectile.height = 30;
			projectile.timeLeft = 80;
			projectile.hostile = false;
            projectile.magic = true;
			projectile.friendly = true;
			projectile.ignoreWater = true;
			projectile.extraUpdates = 1;
			Main.projFrames[projectile.type] = 5;
		}
		public override void AI()
		{
            {
                if (Main.rand.Next(6) == 0)
                {
                    {
                        int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 67);
                        int dust1 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 67);
                    }
                }
            }
			projectile.frameCounter++;
			if (projectile.frameCounter >= 6)
			{
				projectile.frame++;
				projectile.frameCounter = 0;
				if (projectile.frame >= 5)
				{
					projectile.frame = 0;
				}
			}
		}
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 67);
		}
	}
}
