﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Threading.Tasks;
using VacancyParcer.Model;
using VacancyParcer.Services;
using VacancyParcer.ViewModel.ViewModel;
using System.Diagnostics;

namespace VacancyParcer.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        #region Propertys
        Stopwatch stopwatch;
        

        private WebParser_Proglib_IO webParser_Proglib_IO = new WebParser_Proglib_IO();

        private string[] pagesContent;
        private int a;

        private string tmpBody;

        public string TmpBody
        {
            get { return tmpBody; }
            set {
                tmpBody = value;
                OnPropertyChanged();
            }
        }

        private bool AllreadyLoad = false;


        private ObservableCollection<Vacancy> vacancies;

        public ObservableCollection<Vacancy> Vacancies

        {
            get => vacancies;
            set
            {
                vacancies = value;
                OnPropertyChanged();
            }
        }



        private List<string> listOfNameOfVacansy;

        public List<string> ListOfNameOfVacansy
        {
            get => listOfNameOfVacansy;
            set
            {
                listOfNameOfVacansy = value;
                OnPropertyChanged();
            }
        }


        private List<string> listOfNameOfLinc;

        public List<string> ListOfNameOfLinc
        {
            get => listOfNameOfLinc;
            set
            {
                listOfNameOfLinc = value;
                OnPropertyChanged();
            }
        }



        #endregion

        #region Command

        public ActionCommand AssembleObjectSalaryCommand => new ActionCommand(x => AssembleObjectSalary(),x=> CanAssembleAllVacancy());
        public ActionCommand GetCountCommand => new ActionCommand(x => GetCountOfPage());
     

        #endregion



        public MainViewModel()
        {
            stopwatch = new Stopwatch();
            RunTasksAsync();
             
            Vacancies = new ObservableCollection<Vacancy>();
          

        }


        private async Task GetBobyOfFirstPage(WebParser_Proglib_IO webParser_Proglib_IO)

        {
            TmpBody = string.Empty;
            TmpBody = await webParser_Proglib_IO.GetBodyOfPage("https://proglib.io/vacancies/all?workType=all&workPlace=all&experience=&salaryFrom=&page=1");

        }



         private async Task GetEveryBobyOfPage_Select_Task_Parallel(WebParser_Proglib_IO webParser_Proglib_IO)
         {
                   
             
         }










        private async Task RunTasksAsync()
        {

           
            await GetBobyOfFirstPage(webParser_Proglib_IO);
            GetCountOfPage();

         
           await GetEveryBobyOfPage_Auto_Select_Task(webParser_Proglib_IO);
           AllreadyLoad = true;

        }





   

       private async Task GetEveryBobyOfPage_Auto_Select_Task(WebParser_Proglib_IO webParser_Proglib_IO)

        {
           List<string> pages = new List<string>();

           
            int x = 1;
            int y = x + 9;
            int q = (a / 10);
            IEnumerable<Task<string?>?> tasks;
           
            TmpBody = string.Empty;
            stopwatch.Start();
            for (int z = 0; z < q; z++)

            {
                for (int i = x; i <= y; i++)
                {
                    pages.Add($"https://proglib.io/vacancies/all?workType=all&workPlace=all&experience=&salaryFrom=&page={i.ToString()}");
                }

                tasks = pages.Select(p => webParser_Proglib_IO.GetBodyOfPage(p));
                pagesContent = await Task.WhenAll(tasks);

                foreach (string str in pagesContent)
                {
                    TmpBody += " " + str;
                }
              pages.Clear();


                x = x + 10;
                y = x + 9;
            }
            y = x + (a % 10);
            for (int i = x; i < y; i++)
            {
                pages.Add($"https://proglib.io/vacancies/all?workType=all&workPlace=all&experience=&salaryFrom=&page={i.ToString()}");
            }
            tasks = pages.Select(p => webParser_Proglib_IO.GetBodyOfPage(p));
            pagesContent = await Task.WhenAll(tasks);
            stopwatch.Stop();
            foreach (string str in pagesContent)
            {
                TmpBody += " " + str;
            }
            MessageBox.Show($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");
 
        }



        private void GetCountOfPage()
        {
            a = webParser_Proglib_IO.GetCountOfPages(TmpBody);
          
        }



        private void AssembleObjectSalary()
        {

            ListOfNameOfVacansy = webParser_Proglib_IO.EasyGetNameofVacancy(TmpBody);
            List<string> LincOfVacancy = webParser_Proglib_IO.EasyGetLincOfVacancy(TmpBody);
            List<DateTime> DatesOfVacancy = webParser_Proglib_IO.EasyGetPublicationDateOfVacancy(TmpBody);
          

            Vacancies.Clear();
            foreach (string nameOfVacancy in ListOfNameOfVacansy)
            {
                Vacancy vacancy = new Vacancy { Name = nameOfVacancy };
                Vacancies.Add(vacancy);

            }

            for (int i = 0; i< Vacancies.Count(); i++)
            {
                Vacancies[i].WebLinq = LincOfVacancy[i];
                Vacancies[i].DateOfPublication = DatesOfVacancy[i];

            }

        }



      


        private bool CanAssembleAllVacancy()
        {

          return AllreadyLoad;
        }



        public void OnPropertyChanged([CallerMemberName] string property = "")
        {

            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
