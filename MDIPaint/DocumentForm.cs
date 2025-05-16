using System;
using System.Drawing;
using System.Windows.Forms;

namespace MDIPaint
{
    public partial class DocumentForm : Form
    {
        private int x, y;
        private Bitmap bitmap;
        private string filePath; // Хранит путь к файлу для этого документа

        public string FilePath
        {
            get => filePath;
            set => filePath = value;
        }

        public DocumentForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.Sizable;
            bitmap = new Bitmap(ClientSize.Width > 0 ? ClientSize.Width : 100, ClientSize.Height > 0 ? ClientSize.Height : 100);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White); // Инициализация белым фоном
            }

            this.MouseDown += DocumentForm_MouseDown;
            this.MouseMove += DocumentForm_MouseMove;
            this.MouseUp += DocumentForm_MouseUp;
            this.Resize += DocumentForm_Resize;
        }

        public void ResizeCanvas(int newWidth, int newHeight)
        {
            try
            {
                if (newWidth <= 0 || newHeight <= 0)
                    throw new ArgumentException("Размеры холста должны быть положительными.");

                Bitmap newBitmap = new Bitmap(newWidth, newHeight);
                using (Graphics g = Graphics.FromImage(newBitmap))
                {
                    g.Clear(Color.White); // Очистка нового битмапа
                    g.DrawImage(bitmap, 0, 0); // Копирование существующего содержимого
                }
                bitmap?.Dispose(); // Освобождение старого битмапа
                bitmap = newBitmap;

                // Обновление размера формы
                ClientSize = new Size(newWidth, newHeight);
                Invalidate(); // Перерисовка всей формы
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при изменении размера холста: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DocumentForm_Resize(object sender, EventArgs e)
        {
            // Синхронизация битмапа с размером формы при ручном изменении
            if (ClientSize.Width > 0 && ClientSize.Height > 0 && (bitmap.Width != ClientSize.Width || bitmap.Height != ClientSize.Width))
            {
                ResizeCanvas(ClientSize.Width, ClientSize.Height);
            }
        }

        public Bitmap GetBitmap()
        {
            return bitmap;
        }

        private void DocumentForm_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;
        }

        private void DocumentForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                try
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.DrawLine(new Pen(MainForm.PenColor, MainForm.PenWidth), x, y, e.X, e.Y);
                    }
                    x = e.X;
                    y = e.Y;
                    Invalidate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при рисовании: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DocumentForm_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.DrawLine(new Pen(MainForm.PenColor, MainForm.PenWidth), x, y, e.X, e.Y);
                }
                x = e.X;
                y = e.Y;
                Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при рисовании: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DocumentForm_Load(object sender, EventArgs e)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            try
            {
                e.Graphics.DrawImage(bitmap, 0, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при отображении изображения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}