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
    public partial class tilemap_main : Form
    {
        Boolean Mouse_Up = true;

        private void button_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Clicked" + sender);
            Button this_button = (Button)sender;
            this.chosen_char.Text = "Chosen Char: " + this_button.Text;
        }
        public tilemap_main()
        {
            InitializeComponent();
            this.chosen_char.Text = "Chosen Char: ";


            dataGridView1.Width = this.Width-panel1.Width;
            dataGridView1.Height = this.Height;
            dataGridView1.BorderStyle = BorderStyle.FixedSingle;
            int cell_size = 22;


            panel1.Left = this.Width - panel1.Width;
            panel1.Height = this.Height;

            String this_letter = "";
            Button[] ch_buttons = new Button[256];
           
            String buttons_needed = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            
            for (int this_button=0; this_button<buttons_needed.Length; this_button++)
            {
                ch_buttons[this_button] = new Button();
                ch_buttons[this_button].Click += new EventHandler(button_Click);
                ch_buttons[this_button].FlatStyle = FlatStyle.Flat;
                ch_buttons[this_button].ForeColor = Color.YellowGreen;
                ch_buttons[this_button].BackColor = Color.Black;
                ch_buttons[this_button].Dock = DockStyle.None;
                ch_buttons[this_button].AutoSize = false;
                ch_buttons[this_button].Width=20;
                ch_buttons[this_button].Height = 25;
                if ((this_button % 10) == 0)
                {
                    ch_buttons[this_button].Left = 0;
                }
                else
                {
                    ch_buttons[this_button].Left = ch_buttons[this_button - 1].Left + ch_buttons[this_button].Width;
                }
                ch_buttons[this_button].Top = (this_button/10) * 25;
                this_letter = buttons_needed[this_button].ToString();
                ch_buttons[this_button].Text = this_letter ;
                ch_buttons[this_button].Name = "Button_"+ this_letter;
                ch_buttons[this_button].Font = new Font("C64 Pro Mono", 10);
                panel1.Controls.Add(ch_buttons[this_button]);
            }

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
            
            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = chosen_char.Text.Last();
            Debug.WriteLine(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.Height = this.Height - 50;
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
            panel1.Height = this.Height-50; 
            panel1.Top = 0;
            dataGridView1.Width = this.Width - panel1.Width;
            dataGridView1.Height = this.Height;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

            foreach (DataGridViewCell this_cell in dataGridView1.SelectedCells)
            {
                Debug.WriteLine(this_cell);
                this_cell.Value = chosen_char.Text.Last();
            }

        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            Mouse_Up = false;
        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            Mouse_Up = true;
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.Button==MouseButtons.Right)
            {
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = " ";
            }
        }
    }
}
