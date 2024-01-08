using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownGame.Models;
using TopDownGame.Models.Weapons;

namespace TopDownGame.Managers
{
    public static class DamageNumbersManager
    {
        public static List<DamageNumbers> DamageNumbers { get; } = new();
        public static List<Explosion> explosions { get; } = new();

        public static void AddExplosion(Explosion explosion)
        {
            explosions.Add(explosion);
        }

        public static void Update()
        {
            var copyDamage = DamageNumbers.ToList();

            foreach (var DamageNumber in copyDamage)
            {
                DamageNumber.Update();
            }

            for (int i = 0; i <  explosions.Count; i++)
            {
                var explosion = explosions[i];
                explosion.Update();
            }

        }
        public static void Draw()
        {
            var copyDamage = DamageNumbers.ToList();
            foreach (var DamageNumber in copyDamage)
            {
                DamageNumber.Draw();
            }
            for (int i = 0; i < explosions.Count; i++)
            {
                var explosion = explosions[i];
                explosion.Draw();
            }
        }

    }
}
