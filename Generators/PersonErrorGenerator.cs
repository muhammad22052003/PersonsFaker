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

        public List<Person> GenerateErrors(List<Person> persons ,double errorsValue, int seed)
        {
            int allIndexesAmount = GetAllIndexes(persons); //  used to get the total number of errors
            int errorsCount = GetRandomErrorsCount(persons : persons,
                                                   seed: seed,
                                                   countAllIndexes: allIndexesAmount,
                                                   errorsValue: errorsValue);

            Random random = new Random(seed);   

            while (errorsCount != 0)
            {
                int secondSeed = random.Next();

                allIndexesAmount = GetAllIndexes(persons);

                int errorIndex = GetErrorRandomIndex(errorsCount, secondSeed, allIndexesAmount);

                GlobalIndexData indexData = GetGlobalIndexData(persons, errorIndex);

                RandomAction action = GetRandomAction(secondSeed);

                action(indexData, secondSeed);

                errorsCount--;
            }

            //  It is not necessary to obtain the data, the root data itself will change
            return persons;
        }

        public GlobalIndexData GetGlobalIndexData(List<Person> persons, int globalIndex, int skippedProperties = 2)
        {
            int currentIndex = 0;
            int indexCounter = 0;

            for (int i = 0; i < persons.Count; i++)
            {
                var properties = persons[i].GetType().GetProperties();

                //  skippedProperties - used to ensure that data that should not change is not changed
                for (int j = skippedProperties; j < properties.Length; j++)
                {
                    string propertyValue = properties[j].GetValue(persons[i]).ToString();

                    currentIndex = indexCounter;
                    indexCounter += propertyValue.Length;
                    if(currentIndex <= globalIndex && indexCounter > globalIndex)
                    {
                        GlobalIndexData indexData = new GlobalIndexData()
                        {
                            Human = persons[i],
                            Property = properties[j],
                            localeIndex = globalIndex - currentIndex
                        };

                        return indexData;
                    }
                }
            }

            throw new NotImplementedException();
        }

        public int GetAllIndexes(List<Person> persons)
        {
            int allDataIndexes = 0;

            foreach (Person person in persons)
            {
                allDataIndexes += GetAllIndexes(person);
            }

            return allDataIndexes;
        }

        public int GetRandomErrorsCount(List<Person> persons, int seed, int countAllIndexes, double errorsValue)
        {
            int errorsCount = (int)Math.Floor(errorsValue);

            if (Randomizer.GetRandomChanceFromDrop(errorsValue, seed))
            {
                errorsCount++;
            }

            if (countAllIndexes < errorsCount)
            {
                errorsCount = countAllIndexes;
            }

            return errorsCount;
        }

        public int GetErrorRandomIndex(int errorsCount, int seed, int allIndexesCount)
        {
            List<int> errorsIndexes = Randomizer
                                      .GetUniqueRandomNumbers(maxValue: allIndexesCount, 
                                                              seed: seed, 
                                                              count: 1);

            return errorsIndexes.FirstOrDefault();
        }

        public int GetAllIndexes(Person person, int skippedProperties = 2)
        {
            int dataIndexesCount = 0;

            var properties = person.GetType().GetProperties();
            for (int j = skippedProperties; j < properties.Length; j++)
            {
                var propertyValue = properties[j].GetValue(person);
                if (propertyValue != null && propertyValue.ToString() != null)
                {
                    dataIndexesCount += propertyValue.ToString().Length;
                }
            }

            return dataIndexesCount;
        }

        public RandomAction GetRandomAction(int seed)
        {
            Random random = new Random(seed);

            return _actions[random.Next(0, _actions.Count)];
        }

        #region Error Actions
        private void DeleteCharAction(GlobalIndexData globalIndexData, int seed)
        {
            PropertyInfo property = globalIndexData.Property;
            Person person = globalIndexData.Human;
            int localeIndex = globalIndexData.localeIndex;

            string propertyValue = globalIndexData.Property.GetValue(person).ToString();
            string editedPropertyValue = propertyValue.Remove(localeIndex, 1);

            property.SetValue(person, editedPropertyValue);
        }

        private void AddCharAction(GlobalIndexData globalIndexData, int seed)
        {
            PropertyInfo property = globalIndexData.Property;
            Person person = globalIndexData.Human;
            int localeIndex = globalIndexData.localeIndex;

            string editedPropertyValue = globalIndexData.Property.GetValue(person).ToString();
            editedPropertyValue = editedPropertyValue
                .Insert(localeIndex, Randomizer
                .GetRandomChar(editedPropertyValue[localeIndex] ,seed)
                .ToString());

            property.SetValue(person, editedPropertyValue);
        }

        private void ReplaceCharAction(GlobalIndexData globalIndexData, int seed)
        {
            PropertyInfo property = globalIndexData.Property;
            Person person = globalIndexData.Human;
            int localeIndex = globalIndexData.localeIndex;

            string propertyValue = globalIndexData.Property.GetValue(person).ToString();
            if(propertyValue.Length <= 1) { return; }

            string editedPropertyValue = propertyValue;
            if(localeIndex > 0)
            {
                string sym = propertyValue.Substring(localeIndex - 1, 1);
                editedPropertyValue = propertyValue.Remove(localeIndex - 1, 1);
                editedPropertyValue.Insert(localeIndex, sym);
            }
            else
            {
                string sym = propertyValue.Substring(localeIndex, 1);
                editedPropertyValue = propertyValue.Remove(localeIndex, 1);
                editedPropertyValue.Insert(localeIndex + 1, sym);
            }

            property.SetValue(person, editedPropertyValue);
        }
        #endregion

        private List<RandomAction> _actions { get; set; } = new List<RandomAction>();
    }
}
