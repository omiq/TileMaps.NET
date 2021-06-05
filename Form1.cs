using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace WinForm_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            dataGridView1.Width = this.Width-panel1.Width;
            dataGridView1.Height = this.Height;
            dataGridView1.BorderStyle = BorderStyle.FixedSingle;
            int cell_size = 22;
            panel1.Left = this.Width - panel1.Width;
            panel1.Height = this.Height;

            String[,] mygrid = new string[24,40];

            Random rnd = new Random();

            int map_height = 24;
            int map_width = 40;
            dataGridView1.ColumnCount = map_width;
            Debug.WriteLine("Map width: "+ map_width.ToString() + " Map height: " + map_height.ToString());

            int this_char=32;
            Padding newPadding = new Padding(-2, -2, -2, -2);
            this.dataGridView1.RowTemplate.DefaultCellStyle.Padding = newPadding;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Padding = newPadding;
            this.dataGridView1.RowTemplate.Height = cell_size;

            for (int r = 0; r < map_height; r++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(this.dataGridView1);

                for (int c = 0; c < map_width-1; c++)
                {
                    this.dataGridView1.Columns[c].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    this.dataGridView1.Columns[c].MinimumWidth = cell_size-1;
                    this.dataGridView1.Columns[c].Width = cell_size-1;
                    this.dataGridView1.Columns[c].DividerWidth = 0;
                    

                    this_char++;
                    if (this_char == 256) this_char = 32;
                    mygrid[r, c] = ".";// (Convert.ToChar(this_char)).ToString();
                    row.Cells[c].Value = mygrid[r, c];
                }

                this.dataGridView1.Rows.Add(row);
                this.dataGridView1.Rows[r].Height = cell_size-1;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Debug.WriteLine(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)//remove padding
        {
            // ignore the column header and row header cells

            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                e.PaintBackground(e.ClipBounds, true);
                e.Graphics.DrawString(Convert.ToString(e.FormattedValue), e.CellStyle.Font, Brushes.Green, e.CellBounds.X-2, e.CellBounds.Y - 1, StringFormat.GenericDefault);
                e.Handled = true;
            }
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            panel1.Left = this.Width - panel1.Width;
            panel1.Height = this.Height; 
            panel1.Top = 0;
            dataGridView1.Width = this.Width - panel1.Width;
            dataGridView1.Height = this.Height;
        }
    }
}
