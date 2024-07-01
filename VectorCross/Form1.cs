using System.Diagnostics;
using System.Numerics;

namespace VectorCross
{
    public partial class Form1 : Form
    {
        List<Vector2> selects = new List<Vector2>();
        List<Vector2> selects2 = new List<Vector2>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Bitmap img = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            //ImageオブジェクトのGraphicsオブジェクトを作成する
            Graphics g = Graphics.FromImage(img);
            //全体を黒で塗りつぶす
            g.FillRectangle(Brushes.Black, g.VisibleClipBounds);
            //リソースを解放する
            g.Dispose();

            pictureBox1.Image = img;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Graphics g = Graphics.FromImage(pictureBox1.Image);

            if (checkBox1.Checked)
            {
                selects.Add(new Vector2(e.X, e.Y));
                g.DrawEllipse(Pens.Blue, e.X - 5, e.Y - 5, 10, 10);
                if (selects.Count >= 2)
                {
                    g.DrawLine(Pens.Blue, e.X, e.Y, selects[selects.Count - 2].X, selects[selects.Count - 2].Y);
                }
            }
            else
            {
                if (selects2.Count >= 2)
                {
                    selects2.Clear();
                    g.FillRectangle(Brushes.Black, g.VisibleClipBounds);
                    for (int i = 0; i < selects.Count; i++)
                    {
                        g.DrawEllipse(Pens.Blue, selects[i].X - 5, selects[i].Y - 5, 10, 10);
                        if (i > 0)
                        {
                            g.DrawLine(Pens.Blue, selects[i].X, selects[i].Y, selects[i - 1].X, selects[i - 1].Y);
                        }
                    }
                }

                g.DrawEllipse(Pens.Red, e.X - 5, e.Y - 5, 10, 10);
                selects2.Add(new Vector2(e.X, e.Y));
                if (selects2.Count >= 2)
                {
                    g.DrawLine(Pens.Red, e.X, e.Y, selects2[selects2.Count - 2].X, selects2[selects2.Count - 2].Y);

                    for (int i = 1; i < selects.Count; i++)
                    {
                        Vector2 vec = CrossCalc.Intersection(selects2[0], selects2[1], selects[i], selects[i - 1]);
                        if (!float.IsNaN(vec.X))
                        {
                            g.DrawEllipse(Pens.Yellow, vec.X - 5, vec.Y - 5, 10, 10);
                        }
                    }

                }
            }
            g.Dispose();
            pictureBox1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            selects.Clear();
            selects2.Clear();
            Graphics g = Graphics.FromImage(pictureBox1.Image);
            g.FillRectangle(Brushes.Black, g.VisibleClipBounds);
            g.Dispose();
            pictureBox1.Refresh();
        }
    }
}
