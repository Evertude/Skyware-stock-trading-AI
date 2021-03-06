using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Projectiles
{
	public class JellyBeam : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Electric Beam";
            projectile.width = 2;
            projectile.height = 2;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.melee = true;
            projectile.ranged = true;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
            projectile.alpha = 255;
            projectile.timeLeft = 500;
            projectile.light = 0;
            projectile.extraUpdates = 100;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 9;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;

        }
        public override void AI()
        {
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] > 9f)
            {
                for (int num447 = 0; num447 < 2; num447++)
                {
                    Vector2 vector33 = projectile.position;
                    vector33 -= projectile.velocity * ((float)num447 * 0.25f);
                    projectile.alpha = 255;
                    int num448 = Dust.NewDust(vector33, 1, 1, 242, 0f, 0f, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 0.25f);
                    Main.dust[num448].noGravity = true;
                    Main.dust[num448].position = vector33;
                    Main.dust[num448].scale = (float)Main.rand.Next(70, 110) * 0.013f;
                    Main.dust[num448].velocity *= 0.2f;
                }
                return;
            }
        }
 
        public override void Kill(int timeLeft)    
        {
            {
                for (int num621 = 0; num621 < 5; num621++)
                {
                    int num622 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 242, 0f, 0f, 100, default(Color), 2f);
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(5) == 2)

            {
                target.AddBuff(mod.BuffType("ElectrifiedV2"), 180);
            }
        }

            //public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
            //{
            //    Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            //    for (int k = 0; k < projectile.oldPos.Length; k++)
            //    {
            //        Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
            //        Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
            //        spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            //    }
            //    return true;
            //}
        }
}