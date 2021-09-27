using ClosedXML.Excel;
using System;
using System.Linq;
using System.Windows.Forms;

namespace LogikTableCreator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveFileDialog1.FileName = "table";
            saveFileDialog1.DefaultExt = ".xlsx";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.InitialDirectory = path;
            saveFileDialog1.Filter = "Excel (.xlsx)|*.xlsx";
            saveFileDialog1.ShowDialog();
            path = saveFileDialog1.FileName;
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Sample Sheet");
                bool state = false;
                int counter = 0;
                int currentFreq;
                int cases;
                String[] inputs;
                inputs = inputTextBox.Text.Split(',');
                inputs = inputs.Reverse().ToArray();
                cases = Convert.ToInt32(Math.Pow(inputs.Length, 2));
                for (int j = 1; j < inputs.Length + 1; j++)
                {
                    currentFreq = Convert.ToInt32(Math.Pow(2, j - 1));
                    worksheet.Cell(1, j).Value = inputs[j - 1];
                    counter = 0;
                    state = false;
                    for (int i = 2; i < cases + 2; i++)
                    {
                        if (counter == currentFreq)
                        {
                            state = !state;
                            counter = 0;
                        }
                        worksheet.Cell(i, inputs.Length - j + 1).Value = Convert.ToByte(state);
                        counter++;
                    }
                }

                workbook.SaveAs(path);
            }
            MessageBox.Show("done");

        }
    }
}
