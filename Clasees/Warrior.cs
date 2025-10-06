using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pr9
{
    /// <summary>
    /// Базовый класс Воина
    /// </summary>
    public abstract class Warrior
    {
        public double Health { get; protected set; }

        public Warrior()
        {
            Health = 100; 
        }

        /// <summary>
        /// Метод для получения урона
        /// </summary>
        /// <param name="damage">Величина получаемого урона</param>
        public abstract void TakeDamage(double damage);
    }

    /// <summary>
    /// Воин в легких доспехах
    /// </summary>
    public class LightArmorWarrior : Warrior
    {
        private const double ARMOR_COEFFICIENT = 0.7; // Коэффициент защиты 30%

        public override void TakeDamage(double damage)
        {
            if (Health > 0)
            {
                double actualDamage = damage * ARMOR_COEFFICIENT;
                Health -= actualDamage;
                if (Health < 0) Health = 0;
            }
        }

        public override string ToString()
        {
            return $"Воин в легких доспехах (Здоровье: {Health:F1})";
        }
    }

    /// <summary>
    /// Воин в тяжелых доспехах
    /// </summary>
    public class HeavyArmorWarrior : Warrior
    {
        private const double ARMOR_COEFFICIENT = 0.4; // Коэффициент защиты 60%

        public override void TakeDamage(double damage)
        {
            if (Health > 0)
            {
                double actualDamage = damage * ARMOR_COEFFICIENT;
                Health -= actualDamage;
                if (Health < 0) Health = 0;
            }
        }

        public override string ToString()
        {
            return $"Воин в тяжелых доспехах (Здоровье: {Health:F1})";
        }
    }
}