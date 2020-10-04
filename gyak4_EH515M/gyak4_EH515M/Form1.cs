using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace gyak4_EH515M
{

    
    public partial class Form1 : Form
    {
        Microsoft.Office.Interop.Excel.Application xlApp;
        Microsoft.Office.Interop.Excel.Workbook xlWB;
        Microsoft.Office.Interop.Excel.Worksheet xlSheet;

        RealEstateEntities context = new RealEstateEntities();
        public List<Flat> Flats;
        public Form1()
        {
            InitializeComponent();
            LoadData();
            CreateExcel();
        }



        private void LoadData()
        {
            Flats = context.Flat.ToList();
        }

        private void CreateExcel()
        {
            try
            {
                xlApp = new Microsoft.Office.Interop.Excel.Application();
                xlWB = new Microsoft.Office.Interop.Excel.Workbook();
                xlSheet = new Microsoft.Office.Interop.Excel.Worksheet();

                //CreateTable();

                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception)
            {
                string errMsg = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(errMsg, "Error");
                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }                                 
        }
    }
}
