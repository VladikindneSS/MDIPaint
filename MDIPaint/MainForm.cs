using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace MDIPaint
{
    public partial class MainForm : Form
    {
        public static Color PenColor { get; set; }
        public static int PenWidth { get; set; }

        public MainForm()
        {
            InitializeComponent();
            PenColor = Color.Black;
            PenWidth = 3;

            toolStripTextBox1.KeyDown += ToolStripTextBox1_KeyDown;
            MdiChildActivate += MainForm_MdiChildActivate;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            bool hasActiveDoc = ActiveMdiChild != null;
            сохранитьToolStripMenuItem.Enabled = hasActiveDoc;
            сохранитьКакToolStripMenuItem.Enabled = hasActiveDoc;
            размерХолстаToolStripMenuItem.Enabled = hasActiveDoc;
            красныйToolStripMenuItem.Enabled = hasActiveDoc;
            синийToolStripMenuItem.Enabled = hasActiveDoc;
            зеленыйToolStripMenuItem.Enabled = hasActiveDoc;
            другойToolStripMenuItem.Enabled = hasActiveDoc;
            toolStripLabel1.Enabled = hasActiveDoc;
            toolStripTextBox1.Enabled = hasActiveDoc;
        }

        private void MainForm_MdiChildActivate(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog dlg = new OpenFileDialog())
                {
                    dlg.Filter = "Изображения (*.bmp;*.jpg;*.png)|*.bmp;*.jpg;*.png|Все файлы (*.*)|*.*";
                    dlg.FilterIndex = 1;
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        using (Bitmap bmp = new Bitmap(dlg.FileName))
                        {
                            var frm = new DocumentForm();
                            frm.MdiParent = this;
                            frm.FilePath = dlg.FileName;
                            frm.ResizeCanvas(bmp.Width, bmp.Height);
                            using (Graphics g = Graphics.FromImage(frm.GetBitmap()))
                            {
                                g.DrawImage(bmp, 0, 0);
                            }
                            frm.Show();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var frmAbout = new AboutForm())
            {
                frmAbout.ShowDialog();
            }
        }

        private void новыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new DocumentForm();
            frm.MdiParent = this;
            frm.Show();
        }

        private void красныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PenColor = Color.Red;
        }

        private void синийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PenColor = Color.Blue;
        }

        private void зеленыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PenColor = Color.Green;
        }

        private void другойToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ColorDialog cd = new ColorDialog())
            {
                if (cd.ShowDialog() == DialogResult.OK)
                    PenColor = cd.Color;
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (ActiveMdiChild is DocumentForm activeDoc)
                {
                    if (!string.IsNullOrEmpty(activeDoc.FilePath))
                    {
                        ImageFormat format;
                        if (activeDoc.FilePath.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
                            format = ImageFormat.Jpeg;
                        else if (activeDoc.FilePath.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                            format = ImageFormat.Png;
                        else
                            format = ImageFormat.Bmp;

                        activeDoc.GetBitmap().Save(activeDoc.FilePath, format);
                    }
                    else
                    {
                        сохранитьКакToolStripMenuItem_Click(sender, e);
                    }
                }
                else
                {
                    MessageBox.Show("Нет активного документа для сохранения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (ActiveMdiChild is DocumentForm activeDoc)
                {
                    using (SaveFileDialog dlg = new SaveFileDialog())
                    {
                        dlg.AddExtension = true;
                        dlg.Filter = "Windows Bitmap (*.bmp)|*.bmp|JPEG (*.jpg)|*.jpg|PNG (*.png)|*.png";
                        dlg.FilterIndex = 1;
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            ImageFormat format;
                            switch (dlg.FilterIndex)
                            {
                                case 2:
                                    format = ImageFormat.Jpeg;
                                    break;
                                case 3:
                                    format = ImageFormat.Png;
                                    break;
                                default:
                                    format = ImageFormat.Bmp;
                                    break;
                            }
                            activeDoc.GetBitmap().Save(dlg.FileName, format);
                            activeDoc.FilePath = dlg.FileName;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Нет активного документа для сохранения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void каскадомToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void слеваНаправоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void сверхуВнизToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void упорядочитьЗначкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            using (ColorDialog cd = new ColorDialog())
            {
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    PenColor = cd.Color;
                }
            }
        }

        private void размерХолстаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (ActiveMdiChild is DocumentForm activeDoc)
                {
                    using (var sizeForm = new CanvasSizeForm())
                    {
                        if (sizeForm.ShowDialog() == DialogResult.OK)
                        {
                            activeDoc.ResizeCanvas(sizeForm.CanvasWidth, sizeForm.CanvasHeight);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Нет активного документа для изменения размера холста.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при изменении размера холста: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ToolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (sender is ToolStripTextBox tb && int.TryParse(tb.Text, out int width))
                    {
                        if (width > 0 && width <= 50)
                        {
                            PenWidth = width;
                        }
                        else
                        {
                            MessageBox.Show("Введите значение от 1 до 50.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Введите корректное число.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при установке толщины пера: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}