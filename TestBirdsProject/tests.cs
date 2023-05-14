using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office.CustomUI;
using DocumentFormat.OpenXml.Office2010.Excel;
using SpreadsheetLight;
using System;
using WPF_login;

namespace birdsProject
{
    public class User
    {
        public string username;
        public string password;
        public string id;
        public User(string username, string password, string id)
        {
            this.username = username;
            this.password = password;
            this.id = id;
        }
        public bool signUpVlidation()
        {
            if ((id.Length == 9) && (username.Length >= 6) && (username.Length <= 8) && (password.Length <= 10) && (password.Length >= 8))
            {
                int countLettersu = 0; int countdigitsu = 0;
                for (int i = 0; i < username.Length; i++)
                {
                    string temp = "" + username[i];
                    if (int.TryParse(temp, out int numericValue))
                    {
                        countdigitsu++;
                    }
                    if ((username[i] >= 'a' && username[i] <= 'z') || (username[i] >= 'A' && username[i] <= 'Z'))
                    {
                        countLettersu++;
                    }
                }
                int countLettersp = 0; int countdigitsp = 0;
                for (int i = 0; i < password.Length; i++)
                {
                    string temp = "" + password[i];
                    if (int.TryParse(temp, out int numericValue))
                    {
                        countdigitsp++;
                    }
                    if ((password[i] >= 'a' && password[i] <= 'z') || (password[i] >= 'A' && password[i] <= 'Z'))
                    {
                        countLettersp++;
                    }
                }
                if (((countLettersu + countdigitsu == username.Length) && (countdigitsu <= 2)) && ((password.Length - (countLettersp + countdigitsp) != 0) && (countdigitsp >= 1) && (countLettersp >= 1)))
                {
                    SLDocument doc = new SLDocument(@"\\Mac\Home\Desktop\birdsProject-master\birdsProject\Data.xlsx");
                    doc.SelectWorksheet("users");
                    int counter = 2; int flag = 0;
                    string cell = doc.GetCellValueAsString("B2");
                    while (cell != "")
                    {
                        if (cell == id)
                        {
                            return false;
                        }
                        counter++;
                        cell = doc.GetCellValueAsString("B" + counter);
                    }
                    if (flag == 0)
                    {
                        return true;
                    }
                    else
                    { return false; }
                }
            }
            else
            {
                return false;
            }
            return false;
        }
        public bool logInValidation()
        {

            SLDocument doc = new SLDocument(@"\\Mac\Home\Desktop\birdsProject-master\birdsProject\Users.xlsx");
            doc.SelectWorksheet("users");
            string username = doc.GetCellValueAsString("A2");
            string password = doc.GetCellValueAsString("C2");
            int flag = 0; int counter = 0;
            while (username != "")
            {
                if ((this.password == password) && (this.username == username))
                {
                    flag++;
                    username = doc.GetCellValueAsString("A" + counter);
                    password = doc.GetCellValueAsString("B" + counter);
                    counter++;
                }
                else
                {
                    username = doc.GetCellValueAsString("A" + counter);
                    password = doc.GetCellValueAsString("B" + counter);
                    counter++;
                }
            }
            if (flag == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }

    public class Bird
    {
        public string subSpeciec;
        public string birthDate;
        public string sex;
        public string id;
        public string birdSpeciec;
        public string cageIdc;
        public string fatherIdc;
        public string motherIdc;
        public Bird(string subSpeciec, string birthDate, string sex, string id, string birdSpeciec, string cageIdc, string fatherIdc, string motherIdc)
        {
            this.subSpeciec = subSpeciec;
            this.birthDate = birthDate;
            this.sex = sex;
            this.id = id;
            this.birdSpeciec = birdSpeciec;
            this.cageIdc = cageIdc;
            this.fatherIdc = fatherIdc;
            this.motherIdc = motherIdc;
        }
        public bool Add()
        {

            if ((id.Length == 8) && (subSpeciec == "pink") && (cageIdc.Length == 8) && (fatherIdc.Length == 8) && (motherIdc.Length == 8) && (cageIsFound(cageIdc, 'A')) && (IsFound(fatherIdc, 'A')) && (IsFound(motherIdc, 'A')) && (!IsFound(id, 'A')) && (sex != "-1") && (birdSpeciec != "-1"))
            {
                SLDocument doc = new SLDocument(@"\\Mac\Home\Desktop\birdsProject-master\birdsProject\Data.xlsx");
                doc.SelectWorksheet("Birds");
                string cell = doc.GetCellValueAsString("A2");
                int flag = 0;
                int index = 2;
                int num;
                if (!(int.TryParse(id, out num)))
                {
                    flag = 1;
                }
                string[] arr = birthDate.Split('/');
                for (int i = 0; i < arr.Length; i++)
                {
                    int numt;
                    bool isNumeric = int.TryParse(arr[i], out numt);
                    if (!isNumeric)
                    {
                        flag = 1;
                    }
                }
                if (flag == 1) { return false; }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        public bool IsFound(string id, char letter)
        {
            SLDocument doc = new SLDocument(@"\\Mac\Home\Desktop\birdsProject-master\birdsProject\Data.xlsx");
            doc.SelectWorksheet("Birds");
            int counter = 2;
            int flag = 0;
            string cell = doc.GetCellValueAsString("" + letter + "" + counter);
            while (cell != "")
            {
                if ((id == cell))
                {
                    counter++;
                    return true;
                }
                else
                {
                    counter++;
                    cell = doc.GetCellValueAsString("" + letter + "" + counter);
                }
            }
            return false;
        }
        public bool cageIsFound(string id, char letter)
        {
            SLDocument doc = new SLDocument(@"\\Mac\Home\Desktop\birdsProject-master\birdsProject\Data.xlsx");

            doc.SelectWorksheet("Cages");
            int counter = 2;
            int flag = 0;
            string cell = doc.GetCellValueAsString("" + letter + "" + counter);
            while (cell != "")
            {
                if ((id == cell))
                {
                    counter++;
                    return true;
                }
                else
                {
                    counter++;
                    cell = doc.GetCellValueAsString("" + letter + "" + counter);
                }
            }
            return false;
        }
    }
    public class Cage
    {
        private string id;
        private string hight;
        private string width;
        private string length;
        private string matirial;
        public Cage(string id, string hight, string width, string length, string matirial)
        {
            this.id = id;
            this.hight = hight;
            this.width = width;
            this.length = length;
            this.matirial = matirial;
        }
        public bool add()
        {
            int num;
            int index = 2;
            int flag = 0;
            for (int i = 0; i < this.hight.Length; i++)
            {
                if ((this.hight[i] <= '0' && this.hight[i] >= '9'))
                {
                    flag = 1;
                    break;
                }

            }
            for (int i = 0; i < this.width.Length; i++)
            {
                if ((this.width[i] <= '0' && this.width[i] >= '9'))
                {
                    flag = 1;
                    break;
                }

            }
            for (int i = 0; i < this.length.Length; i++)
            {
                if ((this.length[i] <= '0' && this.length[i] >= '9'))
                {
                    flag = 1;
                    break;
                }

            }
            if ((flag==0)||((int.Parse(this.length) > 0) && (int.Parse(this.hight) > 0) && (int.Parse(this.width) > 0) && (!cageIsFound(this.id, 'A')) && (this.matirial != "-1")))
            {
                for (int i = 0; i < this.id.Length; i++)
                {
                    if ((this.id[i] <= 'a' && this.id[i] >= 'z') || (this.id[i] <= 'A' && this.id[i] >= 'Z'))
                    {
                        flag = 1;
                        break;
                    }

                }
                if ((!(int.TryParse(this.length, out num))) || (!(int.TryParse(this.hight, out num))) || (!(int.TryParse(this.width, out num))) || (flag == 1))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
            return false;
            }
        }
        public bool cageIsFound(string id, char letter)
        {
            SLDocument doc = new SLDocument(@"\\Mac\Home\Desktop\birdsProject-master\birdsProject\Data.xlsx");

            doc.SelectWorksheet("Cages");
            int counter = 2;
            int flag = 0;
            string cell = doc.GetCellValueAsString("" + letter + "" + counter);
            while (cell != "")
            {
                if ((id == cell))
                {
                    counter++;
                    return true;
                }
                else
                {
                    counter++;
                    cell = doc.GetCellValueAsString("" + letter + "" + counter);
                }
            }
            return false;
        }
    }

}
