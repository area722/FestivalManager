using PortableLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.IO;
using System.Runtime.Serialization;

namespace StoreApp.DataModel
{
    public class GenreVM:ObservableObject,INotifyPropertyChanged
    {
        private ObservableCollection<Genre> _lstGenre;

        public ObservableCollection<Genre> LstGenre
        {
            get { return _lstGenre; }
            set { _lstGenre = value; OnPropertyChanged("LstGenre"); }
        }

        private String _festName;

        public String FestName
        {
            get { return _festName; }
            set { _festName = value; OnPropertyChanged("FestName"); }
        }
        
        
        public GenreVM()
        {
            LstGenre = new ObservableCollection<Genre>();
            getGenres();
            getFestName();
        }

        private async void getFestName()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new
            System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"));
            HttpResponseMessage response = await client.GetAsync("http://localhost:8090/api/fest");
            if (response.IsSuccessStatusCode)
            {
                Stream stream = await response.Content.ReadAsStreamAsync();
                DataContractSerializer dxml = new DataContractSerializer(typeof(String));
                FestName = dxml.ReadObject(stream) as String;
            }
        }

        private async void getGenres()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new
            System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"));
            HttpResponseMessage response = await client.GetAsync("http://localhost:8090/api/genre");
            if (response.IsSuccessStatusCode)
            {
                Stream stream = await response.Content.ReadAsStreamAsync();
                DataContractSerializer dxml = new DataContractSerializer(typeof(ObservableCollection<Genre>));
                LstGenre = dxml.ReadObject(stream) as ObservableCollection<Genre>;
            }
        }
        
    }
}
