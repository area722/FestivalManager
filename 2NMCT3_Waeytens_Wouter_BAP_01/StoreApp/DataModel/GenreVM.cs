using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortableLibrary;
using System.ComponentModel;
using System.Net.Http;
using System.IO;
using System.Runtime.Serialization;

namespace StoreApp.DataModel
{
    public class GenreVM:ObservableObject, INotifyPropertyChanged
    {
        private List<Genre> _lstGenre;

        public List<Genre> LstGenre
        {
            get { return _lstGenre; }
            set { _lstGenre = value; OnPropertyChanged("LstGenre"); }
        }
        
        public GenreVM()
        {
            LstGenre = new List<Genre>();
            GetGenres();
        }

        public async void GetGenres()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new
            System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"));

            HttpResponseMessage response = await client.GetAsync("http://localhost:8090/api/genre");
            if (response.IsSuccessStatusCode)
            {
                Stream stream = await response.Content.ReadAsStreamAsync();
                DataContractSerializer dxml = new DataContractSerializer(typeof(List<Genre>));
                LstGenre = dxml.ReadObject(stream) as List<Genre>;
            }
        }
    }
}
