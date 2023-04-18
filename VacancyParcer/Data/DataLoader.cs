using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VacancyParcer.Data
{
    public class DataLoader
    {
        private string? WebPageBody { get; set; }    
        private string? WebPageAdress { get; set; }

        public async Task<string?> LoadWebPage(string pageAdress)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage message = await client.GetAsync(pageAdress);
                    WebPageBody = await message.Content.ReadAsStringAsync();
                   
                }
                return WebPageBody;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;    
            }

        }



       





    }
}
