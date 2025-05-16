using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MDIPaint
{
    public partial class CanvasSizeForm: Form
    {
        private int canvasWidth;
        private int canvasHeight;

        public int CanvasWidth => canvasWidth;
        public int CanvasHeight => canvasHeight;

        public CanvasSizeForm()
        {
            InitializeComponent();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(WidthTextBox.Text, out int width) && int.TryParse(HeightTextBox.Text, out int height))
            {
                if (width > 0 && height > 0)
                {
                    canvasWidth = width;
                    canvasHeight = height;
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Размеры должны быть положительными числами.");
                }
            }
            else
            {
                MessageBox.Show("Введите корректные числа для ширины и высоты.");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void CanvasSizeForm_Load(object sender, EventArgs e)
        {
        }

        private void WidthTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void HeightTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
