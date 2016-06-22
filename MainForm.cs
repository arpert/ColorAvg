/*
 * Created by SharpDevelop.
 * User: arpert
 * Date: 2014-11-05
 * Time: 14:50
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace ColorAvg
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		List<Point> pts;
		Bitmap bmp;
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			pts = new List<Point>();
			pts.Add(new Point(10, 20));
			pts.Add(new Point(20, 30));
			pts.Add(new Point(30, 40));
			
			for(int i = 0; i < pts.Count; i++)
			{
     			ListViewItem lvi = listView1.Items.Insert(i, string.Format("Pt {0} ({1},{2})", i, pts[i].X, pts[i].Y));
			}
			listView1.Items[0].BackColor = Color.Red;
			listView1.Items[1].BackColor = Color.Green;
			listView1.Items[2].BackColor = Color.Blue;
			
			bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
			listView1.Items[0].Selected = true;
		}
		
		int dx = 5, dy = 5;
		void PictureBox1MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			int n = -1;
			
			if (listView1.SelectedIndices != null)
				n = listView1.SelectedIndices[0];
			
			if (n < 0) return;
			
			if (e.Button == MouseButtons.Left)
				pts[n] = new Point(e.X, e.Y);
			
			
			listView1.Items[n].Text =  string.Format("Pt {0} ({1},{2})", n, pts[n].X, pts[n].Y);
			Draw();
		}
		
		public void Draw()
		{
			if (bmp == null) return;
			
			int w = pictureBox1.Width;
			int h = pictureBox1.Height;
									
			double r;
			double k = 90;
			double mr, mg, mb;
     		int R = 0, G = 0, B = 0;
			for(int i = 0; i < pts.Count; i++)
			{
     		   R += (int)listView1.Items[i].BackColor.R;
			   G += (int)listView1.Items[i].BackColor.G;
			   B += (int)listView1.Items[i].BackColor.B;
			}
			mr = Math.Ceiling(R / 256 + .001);
			mg = Math.Ceiling(G / 256 + .001);
			mb = Math.Ceiling(B / 256 + .001);
			double m = Math.Max(mr, Math.Max(mg, mb));
			
//			Parallel.For(0, h - 1, y =>
            Graphics gr = Graphics.FromImage(bmp);
			for(int y = 0; y < h; y += dy)
			{
				for(int x = 0; x < w; x += dx)
				{
					R = 0; G = 0; B = 0;
					for(int i = 0; i < pts.Count; i++)
					{
   					   r = Math.Sqrt((x-pts[i].X)*(x-pts[i].X) + (y-pts[i].Y)*(y-pts[i].Y));
   					   R += (int)(listView1.Items[i].BackColor.R * Math.Exp(-r/k)) % 256;
   					   G += (int)(listView1.Items[i].BackColor.G * Math.Exp(-r/k)) % 256;
   					   B += (int)(listView1.Items[i].BackColor.B * Math.Exp(-r/k)) % 256;
					}
					Color c = Color.FromArgb((int)(R / m) % 256, (int)(G / m) % 256, (int)(B / m) % 256);
					
					gr.FillRectangle(new SolidBrush(c), x, y, dx, dy);
					
				}
			}//);
			//gr.DrawEllipse(Pens.Green, pts[0].X - 2, pts[0].Y - 2, 5, 5);
			//gr.DrawEllipse(Pens.Red, pts[1].X - 2, pts[1].Y - 2, 5, 5);

			Invalidate();
		}

		void MainFormPaint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			pictureBox1.CreateGraphics().DrawImageUnscaled(bmp, new Point(0, 0));
		}

		void PictureBox1SizeChanged(object sender, EventArgs e)
		{
			bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
			Draw();
		}

		void ListView1SelectedIndexChanged(object sender, EventArgs e)
		{
           	
		}
		
		void BtColorClick(object sender, EventArgs e)
		{
			int n = -1;
			
			if (listView1.SelectedIndices != null)
				n = listView1.SelectedIndices[0];
			
			if (n < 0) return;
			
			ListViewItem lvi = listView1.Items[n];
			colorDialog1.Color = lvi.BackColor;
			
			if (colorDialog1.ShowDialog() == DialogResult.OK)
			{
				lvi.BackColor = colorDialog1.Color;
				Draw();
			}
			
		}
	}
}
