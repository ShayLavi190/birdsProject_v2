using DocumentFormat.OpenXml.Drawing.Charts;
using SpreadsheetLight;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace birdsProject.pages
{
    /// <summary>
    /// Interaction logic for searchCage.xaml
    /// </summary>
    public class Cell
    {
            public string id;
            public string Length;
            public string Hight;
            public string Width;
            public string Matirial;
        public Cell(string idt, string Lengtht, string Hightt, string Widtht, string Matirialt)
        {
            this.id = idt;
            this.Length = Lengtht;
            this.Hight = Hightt;
            this.Width = Widtht;
            this.Matirial = Matirialt;
        }
    }
    class CellIdComparer : IComparer<Cell>
    {
        public int Compare(Cell x, Cell y)
        {
            return x.id.CompareTo(y.id);
        }
    }
    public partial class searchCage : Page
    {
        public searchCage()
        {
            InitializeComponent();
        }
        private void Backreq(object sender, RoutedEventArgs e)
        {
           NavigationService.Navigate(new Uri("pages/Page1.xaml", UriKind.Relative));
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {

            }
        }
        private void SearchCage(object sender, RoutedEventArgs e)
        {
            string Id = id.Text;
            string Matirial;
            string Length = length.Text;
            string Hight = hight.Text;
            string Width = width.Text;
            string[] matirials = { "Wood","Plastic","Steel" };
            int matirialindex = material.SelectedIndex;
            if (matirialindex == -1)
            {
                Matirial = "-1";

            }
            else
            {
                Matirial = matirials[matirialindex];

            }
            int num;
            int index = 2;
            int flag = 0;
            Cell[] matchingCells = new Cell[0];
            SLDocument doc = new SLDocument(@"\\Mac\Home\Desktop\birdsProject-master\birdsProject\Data.xlsx");
            doc.SelectWorksheet("Cages");
            string cell = doc.GetCellValueAsString("A2");
            while (cell != "")
            {
                 string idExcel = doc.GetCellValueAsString("A" + (index));
                 string LengthExcel = doc.GetCellValueAsString("B" + (index));
                 string HightExcel = doc.GetCellValueAsString("C" + (index));
                 string WidthExcel = doc.GetCellValueAsString("D" + (index));
                 string MatirialExcel = doc.GetCellValueAsString("E" + (index));
                 if((Width==WidthExcel)||(Hight==HightExcel)||(Length==LengthExcel)||(Id==idExcel)||(MatirialExcel== Matirial))
                 {
                        Cell[] temp = new Cell[matchingCells.Length + 1];
                        for (int i = 0; i < matchingCells.Length; i++)
                        {
                            temp[i] = matchingCells[i];
                        }
                        Cell newCell = new Cell(idExcel, LengthExcel, HightExcel, WidthExcel, MatirialExcel);
                        matchingCells = new Cell[matchingCells.Length + 1];
                        for (int i = 0; i < matchingCells.Length; i++)
                        {
                            if (temp[i] != null)
                            {
                                matchingCells[i] = temp[i];
                            }
                        }
                        matchingCells[temp.Length - 1] = newCell;
                        flag++;
                 }
                 index++;
                 cell = doc.GetCellValueAsString("A" + index);
            }
            if(flag!=0)
            {
                Array.Sort(matchingCells, new CellIdComparer());
                cageTable page1 = new cageTable(matchingCells);
                NavigationService.Navigate(page1);
                doc.Save();
            }
            else
            {
                MessageBox.Show("No cage was found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void CloseApp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
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
