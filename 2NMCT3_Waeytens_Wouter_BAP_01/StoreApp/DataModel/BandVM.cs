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
    public class BandVM:ObservableObject,INotifyPropertyChanged
    {
        private List<Band> _lstBand;

        public List<Band> LstBand
        {
            get { return _lstBand; }
            set { _lstBand = value; OnPropertyChanged("LstBand"); }
        }
        
        public BandVM()
        {
            LstBand = new List<Band>();
            GetBandsFromAPI();
        }

        public async void GetBandsFromAPI()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new
            System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"));

            HttpResponseMessage response = await client.GetAsync("http://localhost:8090/api/band");
            if (response.IsSuccessStatusCode)
            {
                Stream stream = await response.Content.ReadAsStreamAsync();
                DataContractSerializer dxml = new DataContractSerializer(typeof(List<Band>));
                LstBand = dxml.ReadObject(stream) as List<Band>;
            }
        }
    }
}
