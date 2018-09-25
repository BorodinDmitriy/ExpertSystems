using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    
    public partial class Form1 : Form
    {
        public delegate void ChangeInterface(bool answer);
        public class CellPoint
        {
            public int rowPosition;
            public int colPosition;
            public CellPoint(int r, int c)
            {
                rowPosition = r;
                colPosition = c;
            }
        }

        private Logic logic = new Logic();
        private int tableSize = 6;
        private List<CellPoint> selectedCells;
        private List<Fact> checkingFacts;
        private List<Fact> checkingGoals;
        public Form1()
        {
            InitializeComponent();
            //MyTest();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            dataGridView1.RowCount = tableSize;
            dataGridView1.ColumnCount = tableSize;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ColumnHeadersVisible = false;
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                DataGridViewColumn col = dataGridView1.Columns[i];
                col.Width = dataGridView1.Rows[0].Height;
            }
            dataGridView1.Height = dataGridView1.Rows[0].Height * tableSize;
            dataGridView1.Width = dataGridView1.Columns[0].Width * tableSize;
            dataGridView1.ScrollBars = ScrollBars.None;

            selectedCells = new List<CellPoint>();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e){

            CellPoint selectedCell = new CellPoint(dataGridView1.CurrentCell.RowIndex, dataGridView1.CurrentCell.ColumnIndex);
            if (!selectedCells.Contains(selectedCell))
            {
                selectedCells.Add(selectedCell);
            }
            foreach (CellPoint a in selectedCells)
            {
                if (!dataGridView1.Rows[a.rowPosition].Cells[a.colPosition].Selected)
                    dataGridView1.Rows[a.rowPosition].Cells[a.colPosition].Selected = true;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private int countPixels(DataGridView dgw, int left, int right, int top, int bottom)
        {
            int result = 0;
            for (int i = left; i < right; i++)
            {
                for (int j = top; j < bottom; j++)
                {
                    if (dgw.Rows[j].Cells[i].Selected)
                    {
                        result++;
                    }
                }
            }
            return result;
        }

        private Fact createFact(DataGridView dgw, String name, int left, int right, int top, int bottom)
        {
            return new Fact(name, "==" , countPixels(dgw, left, right, top, bottom));
        }
        private void button2_Click(object sender, EventArgs e)
        {


        }

        

        private void button2_Click_1(object sender, EventArgs e)
        {
            checkingFacts = new List<Fact>();
            checkingGoals = new List<Fact>();

            int divider = (tableSize / 3);

            checkingFacts.Add(createFact(dataGridView1, "A1", 0, divider, 0, divider));
            checkingFacts.Add(createFact(dataGridView1, "B1", divider, 2 * divider, 0, divider));
            checkingFacts.Add(createFact(dataGridView1, "C1", 2 * divider, tableSize, 0, divider));

            checkingFacts.Add(createFact(dataGridView1, "A2", 0, divider, divider, 2 * divider));
            checkingFacts.Add(createFact(dataGridView1, "B2", divider, 2 * divider, divider, 2 * divider));
            checkingFacts.Add(createFact(dataGridView1, "C2", 2 * divider, tableSize, divider, 2 * divider));

            checkingFacts.Add(createFact(dataGridView1, "A3", 0, divider, 2 * divider, tableSize));
            checkingFacts.Add(createFact(dataGridView1, "B3", divider, 2 * divider, 2 * divider, tableSize));
            checkingFacts.Add(createFact(dataGridView1, "C3", 2 * divider, tableSize, 2 * divider, tableSize));

            int value = (int)numericUpDown1.Value;

            checkingGoals.Add(new Fact("Number", "==", value));
            token = new CancellationToken();
            this.label3.Text = "...";
            if (comboBox1.SelectedIndex == 0)
            {
                worker = new Task(StartDirectWork, token);

            }
            else
            {
                worker = new Task(StartReverseWork, token); 
            }
            worker.Start();
        }

        private void StartDirectWork()
        {
            bool state = logic.DirectOutput(checkingGoals, checkingFacts);
            ChangeInterface print = new ChangeInterface(PrintResult);
            this.label3.BeginInvoke(print, state);
            worker.Dispose();
        }

        private void StartReverseWork()
        {
            bool state = logic.ReverseOutput(checkingGoals, checkingFacts);
            ChangeInterface print = new ChangeInterface(PrintResult);
            this.label3.BeginInvoke(print, state);
            worker.Dispose();
        }

        private void PrintResult(bool state)
        {
            this.label3.Text = state.ToString();
        }

        private Task worker;
        private CancellationToken token;

        private void button1_Click_1(object sender, EventArgs e)
        {
            while (selectedCells.Count > 0)
            {
                CellPoint a = selectedCells[0];
                if (dataGridView1.Rows[a.rowPosition].Cells[a.colPosition].Selected)
                {
                    dataGridView1.Rows[a.rowPosition].Cells[a.colPosition].Selected = false;
                    selectedCells.Remove(a);
                }
            }
        }
    }
}
