using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.Json;

namespace Lab2Test
{
    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateofBirth { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Phone Number.")]
        public string Phone { get; set; }
        public string City { get; set; }
        public string Adrress { get; set; }
        public string Country { get; set; }
        public string Sex { get; set; }
        public List<string> languages;
        public List<string> Languages 
        {
            get
            {
                List<Language> listLanguages;
                using (StreamReader fileObj = new StreamReader("D:\\4year\\Diplom\\language.txt"))
                {
                    string languages = fileObj.ReadToEnd();                    
                    listLanguages = JsonSerializer.Deserialize<List<Language>>(languages);                    
                }
                List<string> ListOfLanguages=new List<string>();
                foreach (string l in this.languages)
                {
                    ListOfLanguages.Add(listLanguages.Where(i=>i.ID==l).FirstOrDefault().NameofLanguage);
                    ListOfLanguages.Add(listLanguages.Where(i => i.ID == l).FirstOrDefault().Level);
                }
                return ListOfLanguages;
            }
            set
            {
                languages = value;
            }
        
        }
    }
}
