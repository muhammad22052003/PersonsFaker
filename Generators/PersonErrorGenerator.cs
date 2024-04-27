using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Task_5.Delegates;
using Task_5.Models;
using Person = Task_5.Models.Person;

namespace Task_5.Generators
{
    public class PersonErrorGenerator
    {
        public PersonErrorGenerator()
        {
            _actions.Add(DeleteCharAction);
            _actions.Add(AddCharAction);
            _actions.Add(ReplaceCharAction);
        }

        /// Used to store global index data
        public struct GlobalIndexData
        {
            public Models.Person Human { get; set; }

            public PropertyInfo Property { get; set; }

            public int localeIndex { get; set; }
        }

        public List<List<string>> GenerateErrors(List<List<string>> persons ,double errorsValue, int seed)
        {
            Random random = new Random(seed);

            int errorsCount = (int)Math.Floor(errorsValue);

            if(Randomizer.GetRandomChanceFromDrop(errorsValue, seed))
            {
                errorsCount++;
            }


            for (int i = 0; i < persons.Count; i++)
            {
                int errors = errorsCount;

                for (int j = 2; 0 != errors; errors--)
                {
                    int chance = random.Next(j, persons[i].Count);

                    if (chance <= errorsCount)
                    {
                        var action = GetRandomAction(random.Next());

                        persons[i][chance] = action(persons[i][chance], random.Next());
                    }
                }
            }

            return persons;
        }

        public RandomAction GetRandomAction(int seed)
        {
            Random random = new Random(seed);

            return _actions[random.Next(0, _actions.Count)];
        }

        public string DeleteCharAction(string value, int seed)
        {
            if(value.Length == 1)
            {
                return value;
            }

            Random random = new Random(seed);

            int index = random.Next(value.Length);

            string result = value.Remove(index, 1);

            return result;
        }

        public string AddCharAction(string value, int seed)
        {
            Random random = new Random(seed);

            int index = random.Next(value.Length);

            string result = value.Insert(index, Randomizer
                                 .GetRandomChar(value[index], seed).ToString());

            return result;
        }

        public string ReplaceCharAction(string value, int seed)
        {
            Random random = new Random(seed);

            int index = random.Next(value.Length);

            StringBuilder result = new StringBuilder(value);

            if (value.Length <= 1) { return value; }

            if(value.Length - 1 == index)
            {
                char sym = result[index];
                result[index] = result[index - 1];
                result[index - 1] = sym;
            }
            else
            {
                char sym = result[index];
                result[index] = result[index + 1];
                result[index + 1] = sym;
            }

            return result.ToString();
        }

        private List<RandomAction> _actions { get; set; } = new List<RandomAction>();
    }
}
