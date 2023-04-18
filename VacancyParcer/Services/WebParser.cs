using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using VacancyParcer.Data;
using static System.Net.WebRequestMethods;

namespace VacancyParcer.Services
{
    public class WebParser_Proglib_IO
    {

    //    public List<string> ListOfNameOfVacansy { get; set; } = new List<string>();

      



      /*  public async Task <List<string>> GetNameofVacancy()
        {
            
            string webAdress = "https://proglib.io/vacancies/all?workType=all&workPlace=all&experience=&salaryFrom=&page=1";
           
            DataLoader dataLoader = new DataLoader();

            string? bobyOfBage = await dataLoader.LoadWebPage(webAdress);

            List<string> ListOfNameOfVacansy_ = new List<string>();

            string pattern = "itemprop=\"title\">(.+)</h2>";
            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(bobyOfBage);
            foreach (Match match in matches)
            {

                ListOfNameOfVacansy_.Add(match.Groups[1].Value.ToString());
           

            }
          
          return ListOfNameOfVacansy_;
           
        }
*/


        public List<string> EasyGetLincOfVacancy(string bobyOfBage)
        {
                      
            List<string> ListOfLincOfVacancy_ = new List<string>();          
            string pattern = "<a\\s+href=\"(.+)\"\\s+class=\"no-link\">";
            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(bobyOfBage);
            foreach (Match match in matches)
            {
                ListOfLincOfVacancy_.Add("https://proglib.io/"+match.Groups[1].Value.ToString());
            }
         //   ListOfLincOfVacancy_.ForEach(m => MessageBox.Show(m));
            return ListOfLincOfVacancy_;   

        }



        public async Task<string?> GetBodyOfPage(string webAdress)
        {


          //  webAdress = "https://proglib.io/vacancies/all?workType=all&workPlace=all&experience=&salaryFrom=&page=1";

            DataLoader dataLoader = new DataLoader();

            string? bobyOfBage = await dataLoader.LoadWebPage(webAdress);
            return bobyOfBage;

        }

        public List<string> EasyGetNameofVacancy(string bobyOfBage)
        { 

            List<string> ListOfNameOfVacansy_ = new List<string>();

            string pattern = "itemprop=\"title\">(.+)</h2>";
            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(bobyOfBage);
            foreach (Match match in matches)
            {

                ListOfNameOfVacansy_.Add(match.Groups[1].Value.ToString());


            }
            return ListOfNameOfVacansy_;

        }


        public int GetCountOfPages(string bobyOfBage)
        {

          
            int countOfPages=0;   
            string pattern = "data-current=\"1\" data-total=\"(.+)\">"; 
            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(bobyOfBage);
           

            if (matches.Count == 1)
            {
                countOfPages=int.Parse(matches[0].Groups[1].Value); 
                              
            }

            return countOfPages;

        }



        public List<DateTime> EasyGetPublicationDateOfVacancy(string bobyOfBage)
        {

            List<DateTime> DatesOfVacancy = new List<DateTime>();

            DateTime dateTime;
            string pattern = "<div itemprop=\"datePosted\">(.+)</div>";  
            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(bobyOfBage);
            foreach (Match match in matches)
            {
              //  MessageBox.Show(match.Groups[1].Value);
                
               dateTime = DateTime.Parse(match.Groups[1].Value);
               DatesOfVacancy.Add(dateTime);

            }
            return DatesOfVacancy;

        }








    }
}
