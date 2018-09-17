using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    
    public partial class Form1 : Form
    {
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
        public Form1()
        {
            InitializeComponent();
            MyTest();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
                dataGridView1.Rows[a.rowPosition].Cells[a.colPosition].Selected = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void MyTest()
        {
            List<Fact> number = new List<Fact>();
            List<Fact> goal = new List<Fact>();

            /*number.Add(new Fact("A1", 0));
            number.Add(new Fact("B1", 1));
            number.Add(new Fact("C1", 0));
            number.Add(new Fact("A2", 0));
            number.Add(new Fact("B2", 3));
            number.Add(new Fact("C2", 0));
            number.Add(new Fact("A3", 0));
            number.Add(new Fact("B3", 3));
            number.Add(new Fact("C3", 1));

            goal.Add(new Fact("Number", 1));
            */

            number.Add(new Fact("A1", 1));
            number.Add(new Fact("B1", 1));
            number.Add(new Fact("C1", 0));
            number.Add(new Fact("A2", 2));
            number.Add(new Fact("B2", 3));
            number.Add(new Fact("C2", 0));
            number.Add(new Fact("A3", 0));
            number.Add(new Fact("B3", 2));
            number.Add(new Fact("C3", 0));
            goal.Add(new Fact("Number", 1));
            bool state = logic.DirectOutput(goal, number);
            bool mistake = !state;
            if (mistake)
            {
                int K = 0;
                K++;
            }
        }
    }
}
