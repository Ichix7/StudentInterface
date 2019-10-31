using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;

namespace HomeWork3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LName.MaxLength = 30;
            FName.MaxLength = 30;
            Application.Current.MainWindow.Height = 120;
        }
        List<Person> people = new List<Person>();

        bool firstNameCorrect = false;
        bool LastNameCorrect = false;
        bool EmailCorrect = false;
        bool StudentIdCorrect = false;
        bool PhoneCorrect = false;
        bool StartDateCorrect = false;
        class Person
        {
            public string firstName
            {
                get;
                set;
            }
            public string lastName
            {
                get;
                set;
            }
            public string emailAdress
            {
                get;
                set;
            }
            public string studentId
            {
                get;
                set;
            }
            public string StartDate
            {
                get;
                set;
            }
            public string PhoneNumber
            {
                get;
                set;
            }
        }
        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchBox.Text = "";
            Success.Visibility = Visibility.Hidden;
            BadS.Foreground = Brushes.Black;
            SearchBox.Foreground = Brushes.Black;
        }

        private void AddBttn_Click(object sender, RoutedEventArgs e)
        {
            ColorGrey();
            Info.IsEnabled = true;
            Save.Visibility = Visibility.Visible;
            BadS.Foreground = Brushes.Black;
            SearchBox.Text = "Student First or Last Name, or ID";
            FName.Text = "Ex: Johnny";
            LName.Text = "Ex: AppleSeed";
            Email.Text = "Ex: JohnnyAppleSeed@gmail.com";
            SID.Text = "Student ID:  # - ## - ###";
            StartD.Text = "Ex: 02-02-02";
            PhoneN.Text = "Ex: 586-747-5864";
            Info.Visibility = Visibility.Visible;
            Save.Visibility = Visibility.Visible;
            Success.Visibility = Visibility.Hidden;
            Application.Current.MainWindow.Height = 390;
        }

        private void LName_GotFocus(object sender, RoutedEventArgs e)
        {
            LName.Text = "";
            LName.Foreground = Brushes.Black;
        }

        private void FName_GotFocus(object sender, RoutedEventArgs e)
        {
            FName.Text = "";
            FName.Foreground = Brushes.Black;
        }

        private void Email_GotFocus(object sender, RoutedEventArgs e)
        {
            Email.Text = "";
            Email.Foreground = Brushes.Black;
        }

        private void SID_GotFocus(object sender, RoutedEventArgs e)
        {
            SID.Text = "";
            SID.Foreground = Brushes.Black;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string StartDate =@"\d{1,2}[-\/]\d{1,2}[-\/]\d{2,4}";
            string StudentID = @"^\d{1}[-]\d{2}[-]\d{3}$";
            string Phone1 = @"^\d{3}[-]\d{3}[-]\d{4}$";
            string Phone2 = @"^\d{3}[-]\d{4}$";
            if (FName.Text == "Ex: Johnny" || FName.Text == "") { BadF.Foreground = Brushes.Red;}
            else { firstNameCorrect = true; BadF.Foreground = Brushes.Black; }
            if (LName.Text == "Ex: AppleSeed" || LName.Text == "") { BadL.Foreground = Brushes.Red; }
            else { LastNameCorrect = true; BadL.Foreground = Brushes.Black; }
            if (Email.Text == "Ex: JohnnyAppleSeed@gmail.com" || Email.Text == "") { BadE.Foreground = Brushes.Red; }
            else { EmailCorrect = true; BadE.Foreground = Brushes.Black; }
            if ((SID.Text != "Student ID:  # - ## - ###" || SID.Text != "") && Regex.IsMatch(SID.Text, StudentID))
            { StudentIdCorrect = true; BadId.Foreground = Brushes.Black; }
            else { BadId.Foreground = Brushes.Red; }
            if ((PhoneN.Text != "Ex: 586-747-5864" || PhoneN.Text != "") && ((Regex.IsMatch(PhoneN.Text, Phone1) || Regex.IsMatch(PhoneN.Text, Phone2))))
            { PhoneCorrect = true; BadPN.Foreground = Brushes.Black;  }
            else { BadPN.Foreground = Brushes.Red; }
            if ((StartD.Text != "Ex: 02-02-02" || StartD.Text != "") && Regex.IsMatch(StartD.Text, StartDate))
            { StartDateCorrect = true; BadSD.Foreground = Brushes.Black;  }
            else { BadSD.Foreground = Brushes.Red;  }
            if (firstNameCorrect == true && LastNameCorrect == true && EmailCorrect == true && StudentIdCorrect == true && PhoneCorrect == true && StartDateCorrect == true)
            {
                Person p = new Person();
                p.firstName = FName.Text;
                p.lastName = LName.Text;
                p.emailAdress = Email.Text;
                p.studentId = SID.Text;
                p.StartDate = StartD.Text;
                p.PhoneNumber = PhoneN.Text;
                people.Add(p);
                ColorGrey();
                FName.Text = "Ex: Johnny";
                LName.Text = "Ex: AppleSeed";
                Email.Text = "Ex: JohnnyAppleSeed@gmail.com";
                SID.Text = "Student ID:  # - ## - ###";
                StartD.Text = "Ex: 02-02-02";
                PhoneN.Text = "Ex: 586-747-5864";
                NameList.Items.Add(p.lastName);
                Info.Visibility = Visibility.Hidden;
                Info.Visibility = Visibility.Hidden;
                Success.Visibility = Visibility.Visible;
                Application.Current.MainWindow.Height = 120;
                firstNameCorrect = false;
                LastNameCorrect = false;
                EmailCorrect = false;
                StudentIdCorrect = false;
                PhoneCorrect = false;
                StartDateCorrect = false;
                Save.Visibility = Visibility.Hidden;
                AddToDoc();
            }
        }
        private void AddToDoc()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("Students Winden\\Students.xml");
            XmlNode xNode = xDoc.SelectSingleNode("People");
            xNode.RemoveAll();
            foreach (Person p in people)
            {
                XmlNode xTop = xDoc.CreateElement("Person");
                XmlNode xFirst = xDoc.CreateElement("FirstName");
                XmlNode xLast = xDoc.CreateElement("LastName");
                XmlNode xEmail = xDoc.CreateElement("EmailAdress");
                XmlNode xStudentID = xDoc.CreateElement("StudentId");
                XmlNode xStartDate = xDoc.CreateElement("StartDate");
                XmlNode xPhoneNumber = xDoc.CreateElement("PhoneNumber");
                xFirst.InnerText = p.firstName;
                xLast.InnerText = p.lastName;
                xEmail.InnerText = p.emailAdress;
                xStudentID.InnerText = p.studentId;
                xStartDate.InnerText = p.StartDate;
                xPhoneNumber.InnerText = p.PhoneNumber;
                xTop.AppendChild(xFirst);
                xTop.AppendChild(xLast);
                xTop.AppendChild(xEmail);
                xTop.AppendChild(xStudentID);
                xTop.AppendChild(xStartDate);
                xTop.AppendChild(xPhoneNumber);
                xDoc.DocumentElement.AppendChild(xTop);
            }
            xDoc.Save("Students Winden\\Students.xml");
        }

        private void MainWindow1_Loaded(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists("Students Winden"))
            { Directory.CreateDirectory("Students Winden"); }
            if (!File.Exists(("Students Winden\\Students.xml")))
            {
                XmlWriter xW = XmlWriter.Create("Students Winden\\Students.xml");
                xW.WriteStartElement("People");
                xW.WriteEndElement();
                xW.Close();
            }
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load("Students Winden\\Students.xml");
            foreach (XmlNode xnode in xdoc.SelectNodes("People/Person"))
            {
                Person p = new Person();
                p.firstName = xnode.SelectSingleNode("FirstName").InnerText;
                p.lastName = xnode.SelectSingleNode("LastName").InnerText;
                p.emailAdress = xnode.SelectSingleNode("EmailAdress").InnerText;
                p.studentId = xnode.SelectSingleNode("StudentId").InnerText;
                p.StartDate = xnode.SelectSingleNode("StartDate").InnerText;
                p.PhoneNumber = xnode.SelectSingleNode("PhoneNumber").InnerText;
                people.Add(p);
                NameList.Items.Add(p.lastName);
            }
        }

        private void NameList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Application.Current.MainWindow.Height = 390;
            Save.Visibility = Visibility.Hidden;
            Info.IsEnabled = false;
            BadS.Foreground = Brushes.Black;
            Info.Visibility = Visibility.Visible;
            ColorBlack();
            LName.Text = people[NameList.SelectedIndex].lastName;
            FName.Text = people[NameList.SelectedIndex].firstName;
            Email.Text = people[NameList.SelectedIndex].emailAdress;
            SID.Text = people[NameList.SelectedIndex].studentId;
            StartD.Text = people[NameList.SelectedIndex].StartDate;
            PhoneN.Text = people[NameList.SelectedIndex].PhoneNumber;
        }

        private void SearchBttn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Height = 390;
            Save.Visibility = Visibility.Hidden;

            var searchQuery = SearchBox.Text.ToLower();
            var search =
                    from s in people
                    where ((s.firstName.ToLower() == searchQuery.ToLower()) || (s.lastName.ToLower() == searchQuery.ToLower()) || (s.studentId == searchQuery))
                    select s;
            foreach (var s in search)
            {
                FName.Text= s.firstName;
                LName.Text = s.lastName;
                Email.Text = s.emailAdress;
                SID.Text= s.studentId;
                StartD.Text= s.StartDate;
                PhoneN.Text= s.PhoneNumber;
            }

            
            SearchBox.Text = "Student First or Last Name, or ID";
            SearchBox.Foreground = Brushes.Gray;
        }
        private void ColorBlack()
        {
            FName.Foreground = Brushes.Black;
            LName.Foreground = Brushes.Black;
            Email.Foreground = Brushes.Black;
            SID.Foreground = Brushes.Black;
            StartD.Foreground = Brushes.Black;
            PhoneN.Foreground = Brushes.Black;
        }
        private void ColorGrey()
        {
            FName.Foreground = Brushes.Gray;
            LName.Foreground = Brushes.Gray;
            Email.Foreground = Brushes.Gray;
            SID.Foreground = Brushes.Gray;
            StartD.Foreground = Brushes.Gray;
            PhoneN.Foreground = Brushes.Gray;
        }

        private void StartD_GotFocus(object sender, RoutedEventArgs e)
        {
            StartD.Text = "";
            StartD.Foreground = Brushes.Black;
        }

        private void PhoneN_GotFocus(object sender, RoutedEventArgs e)
        {
            PhoneN.Text = "";
            PhoneN.Foreground = Brushes.Black;
        }
    }
}
