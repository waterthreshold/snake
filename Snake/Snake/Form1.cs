using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Start();
            timer2.Start();
        }
        PictureBox [,]pic =new PictureBox[20,32];
        int bait_x, bait_y;
        Bitmap body = new Bitmap(15,15);
        Bitmap bait = new Bitmap(15, 15);
        Bitmap head = new Bitmap(15, 15);
       
        uint time = 0;
        int body_count=3;
        int score; int sum = 0;
        int direction = 1;
        int head_x, head_y;
        int tail_x, tail_y;
        Bitmap empty = new Bitmap(15, 15);
        bool [,]cango=new bool[20,32];
        List<int> body_x = new List<int>();
        List<int> body_y = new List<int>();
        private void Form1_Load(object sender, EventArgs e)
        {
            initialDraw();
           
        }

        private void initialDraw()
        {
            Graphics g;
            g = Graphics.FromImage(body);
            g.DrawEllipse(new Pen(Color.Blue, 1), new Rectangle(0, 0, 8, 8));
            g = Graphics.FromImage(head);
            g.DrawEllipse(new Pen(Color.Red, 1), new Rectangle(0, 0,8, 8));
            g = Graphics.FromImage(bait);
            g.DrawRectangle(new Pen(Color.Red, 1),new Rectangle(0, 0, 8, 8));

            for (int i = 0; i <= 19; i++)
            {
                for (int j = 0; j <= 31; j++)
                {
                    if (i == 0 || j == 0 || i == 19 || j == 31)
                    {
                        cango[i, j] = false;
                    }
                    else
                    {
                        cango[i, j] = true;
                        pic[i, j] = new PictureBox();
                        tableLayoutPanel1.Controls.Add(pic[i, j]);
                    }

                }
            }
            head_x = 12; head_y = 20;
            body_x.Add(head_x);
            body_y.Add(head_y);
            tail_x = 12; tail_y = 23;
            pic[head_x, head_y].BackgroundImage = head;
            for (int i = 1; i <= body_count; i++)
            {
                if (i != body_count)
                {
                    body_x.Add(head_x);
                    body_y.Add(head_y + i);
                }
                pic[head_x, head_y + i].BackgroundImage = body;
            }
            randombait();
        }

        private void randombait()
        {
            Random r = new Random();
            bait_x = r.Next(17)+1;
            bait_y = r.Next(29)+1;
            if (body_x.Contains(bait_x) && body_y.Contains(bait_y))
            {
                randombait();
            }
            else
            {
                pic[bait_x, bait_y].BackgroundImage = bait;
            }
                
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            speed_lbl.Text = "Speed: " +timer1.Interval;
            score_lbl.Text = "Score: " + sum;
            if (body_count < 4 )
            {
                score = 2;
                timer1.Interval = 1000;
            }
            else if (body_count >= 4&&body_count<7)
            {
                score = 3;
                timer1.Interval = 700;
            }
            else if (body_count >= 7 && body_count < 10)
            {
                score = 5;
                timer1.Interval = 300;
            }
            else if (body_count >= 10 && body_count < 13)
            {
                score = 7;
                timer1.Interval = 100;
            }
            else
            {
                score = 11;
                timer1.Interval = 70;
            }
            move();
            
        }
        private void move()
        {
            try
            {
                int tmp_x;
                int tmp_y;
                bool flag = false;
                if (direction == 1)
                {
                    --head_y;

                }
                else if (direction == 2)
                {
                    head_x++;

                }
                else if (direction == 3)
                {
                    head_y++;
                }
                else if (direction == 4)
                {
                    head_x--;
                }
                pic[head_x, head_y].BackgroundImage = head;
                body_x.Add(head_x);
                body_y.Add(head_y);
                if (head_x == bait_x && head_y == bait_y)
                {
                    sum += score;
                    flag = true;
                }
                else
                {
                    pic[tail_x, tail_y].BackgroundImage = empty;
                }
                for (int i = 1; i <= body_count; i++)
                {
                    if (i == body_count)
                    {
                        if (!flag)
                        {
                            pic[body_x[0], body_y[0]].BackgroundImage = body;
                            tail_x = body_x[0];
                            tail_y = body_y[0];
                            body_x.RemoveAt(0);
                            body_y.RemoveAt(0);
                        }
                        else
                        {
                            pic[body_x[0], body_y[0]].BackgroundImage = body;
                            tmp_x = body_x[0];
                            tmp_y = body_y[0];
                            body_x.RemoveAt(0);
                            body_y.RemoveAt(0);
                            body_x.Add(tmp_x);
                            body_y.Add(tmp_y);
                            pic[tail_x, tail_y].BackgroundImage = body;
                            body_count++;
                            randombait();
                            break;
                        }
                    }
                    else
                    {
                        pic[body_x[0], body_y[0]].BackgroundImage = body;
                        tmp_x = body_x[0];
                        tmp_y = body_y[0];
                        body_x.RemoveAt(0);
                        body_y.RemoveAt(0);
                        body_x.Add(tmp_x);
                        body_y.Add(tmp_y);
                    }


                }
            }
            catch (Exception e)
            {
                timer1.Stop();

                if (MessageBox.Show("Play Again??", "Game Over!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Application.Restart();
                }
                else
                {
                    Application.Exit();
                }
            }
      
           
        }

        

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up && direction != 2)
            {
                direction = 4;
            }
            else if (e.KeyCode == Keys.Down && direction != 4)
            {
                direction = 2;
            }
            else if (e.KeyCode == Keys.Left && direction != 3)
            {
                direction = 1;
            }
            else if (e.KeyCode == Keys.Right && direction != 1)
            {
                direction = 3;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            time_lbl.Text = "Time : " + (++time);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.Clear(Color.Black);
        }
    }
}
