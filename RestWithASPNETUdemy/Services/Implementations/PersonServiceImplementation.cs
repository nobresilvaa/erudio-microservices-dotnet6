﻿using RestWithASPNETUdemy.Model;
using System.Reflection;

namespace RestWithASPNETUdemy.Services.Inplementations
{
    public class PersonServiceImplementation : IPersonServices
    {
        private volatile int count;

        public Person Creat(Person person)
        {
            return person;
        }

        public void Delete(long id)
        {
            
        }

        public List<Person> FindAll()
        {
            List<Person> Persons = new List<Person>();
            for (int i = 0; i < 8; i++)
            {   

                Person person = MockPerson(i);
                Persons.Add(person);
            }

            return Persons;
        }



        public Person FindByID(long id)
        {
            return new Person
            {
                Id = IncrementAndGet(),
                FirstName = "Leandro",
                LastName = "Costa",
                Addres = "Uberlandia - Minas Gerais - Brasil",
                Gender = "Male"

            };
        }

        public Person Update(Person person)
        {
            return person;
        }
        private Person MockPerson(int i)
        {
            return new Person
            {
                Id = IncrementAndGet()+i,
                FirstName = "Person Name"+i,
                LastName = "Person LastName"+i,
                Addres = "Some Address"+i,
                Gender = "Male"

            };  

        }

        private long IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }
    }
}
