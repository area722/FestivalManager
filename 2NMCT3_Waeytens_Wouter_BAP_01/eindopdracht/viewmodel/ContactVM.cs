using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using eindopdracht.model;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows;

namespace eindopdracht.viewmodel
{
    class ContactVM: ObservableObject, IPage
    {
        public string Name
        {
            get { return "Contact"; }
        }

        ContactPersoonType searchType;
        string searchTerm;

        #region properties for binding
        private ObservableCollection<ContactPersoon> _contactPersoonLst;

        public ObservableCollection<ContactPersoon> ContactPersoonLst
        {
            get { return _contactPersoonLst; }
            set { _contactPersoonLst = value; OnPropertyChanged("ContactPersoonLst"); }
        }

        private ObservableCollection<ContactPersoonType> _typesList;

        public ObservableCollection<ContactPersoonType> TypesList
        {
            get { return _typesList; }
            set { _typesList = value; OnPropertyChanged("TypesList"); }
        }

        private ContactPersoon _addContactPerson;

        public ContactPersoon AddContactPerson
        {
            get { return _addContactPerson; }
            set { _addContactPerson = value; OnPropertyChanged("AddContactPerson"); }
        }

        private String _addCategorie;

        public String AddCategorie
        {
            get { return _addCategorie; }
            set { _addCategorie = value; OnPropertyChanged("AddCategorie"); }
        }

        private String _buttonText;

        public String ButtonText
        {
            get { return _buttonText; }
            set { _buttonText = value; OnPropertyChanged("ButtonText"); }
        }

        private String _visibleButton;

        public String VisibleButton
        {
            get { return _visibleButton; }
            set { _visibleButton = value; OnPropertyChanged("VisibleButton"); }
        }
        #endregion

        #region ctor
        public ContactVM()
        {
            _contactPersoonLst = ContactPersoon.GetContacts();
            _typesList = ContactPersoonType.GetTypes();
            AddContactPerson = new ContactPersoon();
            ButtonText = "Add";
            VisibleButton = "Hidden";
        }
        #endregion

        #region commands
        public ICommand SaveContactCommand
        {
            get { return new RelayCommand(SaveContact); }
        }

        private void SaveContact()
        {
            if (ButtonText == "Add")
            {
                //Console.WriteLine("addcontact");
                ContactPersoon.addContact(AddContactPerson);
            }
            else if (ButtonText == "Edit")
            {
                //Console.WriteLine("editcontact");
                ContactPersoon.editContact(AddContactPerson);
            }
            ContactPersoonLst = ContactPersoon.GetContacts();
        }

        public ICommand AddCatCommand
        {
            get { return new RelayCommand(AddCat); }
        }

        private void AddCat()
        {
            ContactPersoonType.AddCategorie(AddCategorie);
            TypesList = ContactPersoonType.GetTypes();
        }

        public ICommand EditContactCommand
        {
            get { return new RelayCommand<ContactPersoon>(EditContact); }
        }

        private void EditContact(ContactPersoon person)
        {
            ButtonText = "Edit";
            VisibleButton = "Visible";
            //velden invullen
            AddContactPerson = person;
            if(AddContactPerson.ImageByte == null)
            {
                //zwartVak
                System.Drawing.Image black = System.Drawing.Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "black.jpg");
                MemoryStream me = new MemoryStream();
                black.Save(me, System.Drawing.Imaging.ImageFormat.Jpeg);
                AddContactPerson.ImageByte = me.ToArray();
            }
        }

        public ICommand newContactCommand
        {
            get { return new RelayCommand(newContact); }
        }

        private void newContact()
        {
            VisibleButton = "Hidden";
            ButtonText = "Add";
            //alle velden clearen
            AddContactPerson = null;
            AddContactPerson = new ContactPersoon();
        }

        public ICommand GetPhotoCommand
        {
            get { return new RelayCommand(getPhoto); }
        }

        private void getPhoto()
        {
            Console.WriteLine("getphoto");
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = ("jpg|*.jpg");
            string path = "";
            if (dialog.ShowDialog() == true)
            {
                path = dialog.FileName;
            }
            if (path != "")
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(path);
                MemoryStream me = new MemoryStream();
                img.Save(me, System.Drawing.Imaging.ImageFormat.Jpeg);
                AddContactPerson.ImageByte = me.ToArray();
            }
            EditContact(AddContactPerson);
        }

        public ICommand ZoekCommand
        {
            get { return new RelayCommand<string>(Zoek); }
        }

        private void Zoek(string term)
        {
            searchTerm = term;
            if (term != string.Empty && term != "Search")
            {
                Console.WriteLine("term: " + term);
                ContactPersoonLst = ContactPersoon.SearchContactPerson(term,searchType);
            }
            if (term == string.Empty)
            {
                ContactPersoonLst = ContactPersoon.GetContacts();
            }
        }


        public ICommand DeleteContactCommand
        {
            get { return new RelayCommand<ContactPersoon>(deleteHandler); }
        }

        private void deleteHandler(ContactPersoon person)
        {
            //alle gereserveerde tickets met dat type ook verwijderen en waarschuwen met een dialogbox
            string sMessageBoxText = "Weet je zeker dat je " + person.Name + " wil verwijderen?";
            string sCaption = "Bent u zeker?";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Question;

            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
            if (rsltMessageBox == MessageBoxResult.Yes)
            {
                ContactPersoon.DeleteContactPerson(person);
                ContactPersoonLst = ContactPersoon.GetContacts();
            }
        }

        #region filters
        public ICommand Type1Command
        {
            get { return new RelayCommand<ContactPersoonType>(Type1Handler); }
        }

        private void Type1Handler(ContactPersoonType type)
        {
            Console.WriteLine("filter1: "+type.Name);
            if (type.Id != 0)
            {
                ContactPersoonLst = ContactPersoonType.GetPersonsByType(type);
                searchType = type;
            }
            else
            {
                ContactPersoonLst = ContactPersoon.GetContacts();
            }
        }
        #endregion

        #endregion
    }
}
