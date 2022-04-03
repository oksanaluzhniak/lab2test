using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Lab2Test
{
    class People
    {
        public List<Person> DataPeople { get; set; }
        public People(string filename)
        {
            LoadData(filename);
        }
        public void LoadData(string filename)
        {
            FileInfo file = new FileInfo(filename);
            if (file.Exists)
                using (StreamReader fileObj = new StreamReader(filename))
                {
                    string datapeople = fileObj.ReadToEnd();

                    this.DataPeople = JsonSerializer.Deserialize<List<Person>>(datapeople);
                }
            else
            {
                file.Create();
            }
        }
        public void SaveData(List<Person> people, string filename)
        {
            using (StreamWriter fileObj=new StreamWriter(filename))
            {
                var options = new JsonSerializerOptions 
                {
                    WriteIndented = true
                };
                string text = JsonSerializer.Serialize(people,options);
                fileObj.Write(text);
            }
        }
        public Person SearchByEmail(string email)
        {
            Person persona = this.DataPeople.Where(p => p.Email == email).FirstOrDefault();
            return persona;
        }
        public List<Person> FilterBySex(string sex)
        {
            List<Person> persona = new List<Person>(this.DataPeople.Where(p => p.Sex == sex));
            return persona;
        }
        public void AddPerson(Person person, string filename)
        {
            if (ValidatePhone(person) == true)
            {
                if (SearchByEmail(person.Email) == null)
                {
                    this.DataPeople.Add(person);
                    SaveData(this.DataPeople, filename);
                }
                else
                {
                    int i = this.DataPeople.IndexOf(SearchByEmail(person.Email));
                    this.DataPeople[i] = person;
                    SaveData(this.DataPeople, filename);
                }
            }
            else { Console.WriteLine("Перевірте дані"); }
           
        }
        
        public void DeletePerson(string email,string filename)
        {
            int i = this.DataPeople.IndexOf(SearchByEmail(email));
            this.DataPeople.RemoveAt(i);
            SaveData(this.DataPeople, filename);
        }
        public bool ValidatePhone(Person person)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(person);
            bool validated = false;
            if (Validator.TryValidateObject(person, context, results, true))
            { 
                validated = true;           
            }
            return validated;
        }
        public string AgePerson(Person person)
        {          
            var dateTime = DateTime.Parse(person.DateofBirth); 
            int age = (DateTime.Today.Year- dateTime.Year);
            if (DateTime.Today.DayOfYear < dateTime.DayOfYear)
            {
                age--; 
            }
            return age.ToString();
        }
    }
}
