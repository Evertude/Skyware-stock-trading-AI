using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpiritMod.Projectiles.Boss
{
	public class IceBeam : ModProjectile
	{
        int target;
        public override void SetDefaults()
		{
			projectile.hostile = true;
			projectile.magic = true;
			projectile.width = 6;
			projectile.height = 6;
            projectile.timeLeft = 180;
            projectile.alpha = 255;
            projectile.friendly = false;
			projectile.name = "Ice Beam";
			projectile.aiStyle = 1;
			projectile.tileCollide = false;
			
		}
        public override bool PreAI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + 1.57F;

            if (projectile.ai[0] == 0 && Main.netMode != 1)
            {
                target = -1;
                float distance = 2000f;
                for (int k = 0; k < 255; k++)
                {
                    if (Main.player[k].active && !Main.player[k].dead)
                    {
                        Vector2 center = Main.player[k].Center;
                        float currentDistance = Vector2.Distance(center, projectile.Center);
                        if (currentDistance < distance || target == -1)
                        {
                            distance = currentDistance;
                            target = k;
                        }
                    }
                }
                if (target != -1)
                {
                    projectile.ai[0] = 1;
                    projectile.netUpdate = true;
                }
            }
            else
            {
                Player targetPlayer = Main.player[this.target];
                if (!targetPlayer.active || targetPlayer.dead)
                {
                    this.target = -1;
                    projectile.ai[0] = 0;
                    projectile.netUpdate = true;
                }
                else
                {
                    float currentRot = projectile.velocity.ToRotation();
                    Vector2 direction = targetPlayer.Center - projectile.Center;
                    float targetAngle = direction.ToRotation();
                    if (direction == Vector2.Zero)
                    {
                        targetAngle = currentRot;
                    }

                    float desiredRot = currentRot.AngleLerp(targetAngle, 0.1f);
                    projectile.velocity = new Vector2(projectile.velocity.Length(), 0f).RotatedBy(desiredRot, default(Vector2));
                }
            }

            if (projectile.timeLeft <= 60)
            {
                projectile.alpha -= 4;
            }
            {
                int dust = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 135, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
                Main.dust[dust].scale = 1.5f;
            }
            return false;
        }
       
	}
}