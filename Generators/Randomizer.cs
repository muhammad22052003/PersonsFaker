using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameParser.Generators
{
    public class Randomizer
    {
        public static char GetRandomChar(char template, int seed)
        {
            Random random = new Random(seed);

            if (template >= 'ʹ' && template <= '˿')
            {
                return (char)random.Next('ʹ', '˿' + 1);
            }
            else if (template >= 'A' && template <= 'Z')
            {
                return (char)random.Next('A', 'Z' + 1);
            }
            else if (template >= '0' && template <= '9')
            {
                return (char)random.Next('0', '9' + 1);

            }
            else if (template >= 'А' && template <= 'Я')
            {
                return (char)random.Next('А', 'Я' + 1);

            }
            else if (template >= 'а' && template <= 'я')
            {
                return (char)random.Next('а', 'я' + 1);
            }
            else
            {
                return (char)random.Next('a', 'z' + 1);
            }
        }

        public static List<int> GetUniqueRandomNumbers(int maxValue, int seed, int count = 10)
        {
            if(count > maxValue) { throw new Exception(); }

            Random random = new Random(seed);

            List<int> numbers = new List<int>();

            for (int i = 0; i < count; )
            {
                int number = random.Next(maxValue);

                if (numbers.Contains(number)) { continue; }
                i++;

                numbers.Add(number);
            }

            return numbers;
        }

        public static bool GetRandomChanceFromDrop(double chance, int seed)
        {
            Random random = new Random(seed);
            double drop = chance - Math.Floor(chance);
            int randomNum = 0;

            if (drop != 0)
            {
                randomNum = random.Next(0, (int)Math.Ceiling(1.0 / drop * 10));
            }
            else { return false; }

            if (randomNum < 10 && randomNum >= 0)
            {
                return true;
            }

            return false;
        }
    }
}
