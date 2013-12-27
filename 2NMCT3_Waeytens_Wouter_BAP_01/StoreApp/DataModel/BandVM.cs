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
    public class BandVM:ObservableObject,INotifyPropertyChanged
    {
        private ObservableCollection<Band> _lstBand;

        public ObservableCollection<Band> LstBand
        {
            get { return _lstBand; }
            set { _lstBand = value; OnPropertyChanged("LstBand"); }
        }

        private Genre _selectedGenre;

        public Genre SelectedGenre
        {
            get { return _selectedGenre; }
            set { _selectedGenre = value; OnPropertyChanged("SelectedGenre"); }
        }

        private ObservableCollection<Band> _lstBandGenre;

        public ObservableCollection<Band> LstBandGenre
        {
            get { return _lstBandGenre; }
            set { _lstBandGenre = value; OnPropertyChanged("LstBandGenre"); }
        }      

        public BandVM()
        {
            LstBand = new ObservableCollection<Band>();
            getBands();
        }

        private async void getBands()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new
            System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"));
            HttpResponseMessage response = await client.GetAsync("http://localhost:8090/api/band");
            if (response.IsSuccessStatusCode)
            {
                Stream stream = await response.Content.ReadAsStreamAsync();
                DataContractSerializer dxml = new DataContractSerializer(typeof(ObservableCollection<Band>));
                LstBand = dxml.ReadObject(stream) as ObservableCollection<Band>;
            }
        }

    }
}
