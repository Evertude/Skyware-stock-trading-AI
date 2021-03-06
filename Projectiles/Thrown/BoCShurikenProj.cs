using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Projectiles.Thrown
{
	public class BoCShurikenProj : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Dusk Essence";
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 180;
            projectile.height = 16;
            projectile.width = 16;
            aiType = ProjectileID.Shuriken;
            projectile.extraUpdates = 1;
            base.projectile.ignoreWater = true;
            base.projectile.tileCollide = false;
            base.projectile.thrown = true;


        }

        public override void AI()
        {
            int num = (int)base.projectile.velocity.X * 10;
            int num2 = (int)base.projectile.velocity.Y + 1;
            base.projectile.frameCounter++;
            if (base.projectile.frameCounter > 120)
            {
                base.projectile.frameCounter = 0;
                if (base.projectile.frame == 5)
                {
                    base.projectile.frame = 0;
                }
                else
                {
                    base.projectile.frame++;
                }
            }
            int num3 = 80 - num;
            int num4 = 12 - num2;
            int num5 = 16;
            base.projectile.localAI[1] += 0.0104719754f * (float)num4;
            base.projectile.localAI[1] %= 6.28318548f;
            Vector2 center = Main.projectile[(int)base.projectile.localAI[0]].Center;
            center.X -= (float)num5;
            base.projectile.rotation = (float)Math.Atan2((double)center.Y, (double)center.X) - 2f;
            base.projectile.Center = center + (float)num3 * new Vector2((float)Math.Cos((double)base.projectile.localAI[1]), (float)Math.Sin((double)base.projectile.localAI[1]));
        }
        private static Vector2 GetVelocity(Projectile projectile)
        {
            float num = 400f;
            Vector2 velocity = projectile.velocity;
            Vector2 vector = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
            vector.Normalize();
            Vector2 vector2 = vector * ((float)Main.rand.Next(10, 41) * 0.1f);
            if (Main.rand.Next(3) == 0)
            {
                vector2 *= 2f;
            }
            Vector2 vector3 = velocity * 0.25f + vector2;
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].CanBeChasedBy(projectile, false))
                {
                    float num2 = Main.npc[i].position.X + (float)(Main.npc[i].width / 2);
                    float num3 = Main.npc[i].position.Y + (float)(Main.npc[i].height / 2);
                    float num4 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num2) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num3);
                    if ((double)num4 < (double)num && Collision.CanHit(projectile.position, projectile.width, projectile.height, Main.npc[i].position, Main.npc[i].width, Main.npc[i].height))
                    {
                        num = num4;
                        vector3.X = num2;
                        vector3.Y = num3;
                        Vector2 vector4 = vector3 - projectile.Center;
                        vector4.Normalize();
                        vector3 = vector4 * 8f;
                    }
                }
            }
            return vector3 * 0.8f;
        }
        public override void Kill(int timeLeft)
        {
            if (Main.rand.Next(0, 4) == 0)
            {
                Terraria.Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, mod.ItemType("BoCShuriken"), 1, false, 0, false, false);
            }
            for (int i = 0; i < 5; i++)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 5);
            }
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y);
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