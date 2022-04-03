
using System;
using System.Text.Json;

namespace Lab2Test
{
    class Program
    {
        static void Main(string[] args)
        {
            People people = new People("D:\\4year\\Diplom\\person.txt");
            var options = new JsonSerializerOptions { WriteIndented = true };
            if (people != null)
            {
                string text = JsonSerializer.Serialize(people.SearchByEmail("julianalaird@gmail.com"), options);
                Console.WriteLine(text);
            }
            else 
            {
                Console.WriteLine("Missing data");
            }
            people.SaveData(people.FilterBySex("Male"), "D:\\4year\\Diplom\\person2.txt");
            Person p1 = new Person();
            p1.FirstName = "Oksana";
            p1.LastName = "Luzhniak";
            p1.DateofBirth = "09.05.2001";
            p1.Email = "luzhnyako@gmail.com";
            p1.Phone = "0671686047";
            p1.City = "Bila";
            p1.Adrress = "Central st,58A";
            p1.Country = "Ukraine";
            p1.Sex = "Female";
            //people.AddPerson(p1, "D:\\4year\\Diplom\\person2.txt");
            //people.DeletePerson("luzhnyako@gmail.com", "D:\\4year\\Diplom\\person.txt");
            Console.WriteLine(people.AgePerson(people.DataPeople[1]));
            
        }
    }
}
