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
		bool initDone = false;
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			int x = pictureBox1.Width;
			int y = pictureBox1.Height;
			int d = 50;
			pts = new List<Point>();
			pts.Add(new Point(    d,     d));
			pts.Add(new Point(x - d,     d));
			pts.Add(new Point(x - d, y - d));
			pts.Add(new Point(    d, y - d));
			
			for(int i = 0; i < pts.Count; i++)
			{
     			ListViewItem lvi = listView1.Items.Insert(i, string.Format("Pt {0} ({1},{2})", i, pts[i].X, pts[i].Y));
			}
			listView1.Items[0].BackColor = Color.Red;
			listView1.Items[1].BackColor = Color.Green;
			listView1.Items[2].BackColor = Color.Blue;
			listView1.Items[3].BackColor = Color.White;
			
			bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
			listView1.Items[0].Selected = true;
			
			initDone = true;
			Draw();
		}
		
		int dx = 3, dy = 3;
		
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
			if (bmp == null || pts == null) return;
			
			int w = pictureBox1.Width;
			int h = pictureBox1.Height;
			bmp = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
			var colors = new List<Color>();
			
			double k = (double)numericUpDown1.Value;
			double mr = 0, mg = 0, mb = 0;

			for(int i = 0; i < pts.Count; i++)
			{
     		   mr += (int)listView1.Items[i].BackColor.R;
			   mg += (int)listView1.Items[i].BackColor.G;
			   mb += (int)listView1.Items[i].BackColor.B;
			   colors.Add(listView1.Items[i].BackColor);
			}
			mr = Math.Ceiling(mr / 255 + .001);
			mg = Math.Ceiling(mg / 255 + .001);
			mb = Math.Ceiling(mb / 255 + .001);
			double m = Math.Max(mr, Math.Max(mg, mb));
			m *= (double)numericUpDown2.Value / 100;
            unsafe
            {
               System.Drawing.Imaging.BitmapData bitmapData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);

               int BytesPerPixel = System.Drawing.Image.GetPixelFormatSize(bmp.PixelFormat) / 8;
               int HeightInPixels = bitmapData.Height;
               int WidthInBytes = bitmapData.Width * BytesPerPixel;
               byte* PtrFirstPixel = (byte*)bitmapData.Scan0;
			
				Parallel.For(0, HeightInPixels - 1, y =>
	        //    for(int y = 0; y < HeightInPixels; y++)
				{
                    double r;
			 		int R = 0, G = 0, B = 0;
	                byte* CurrentLine = PtrFirstPixel + (y * bitmapData.Stride);
					for(int ix = 0; ix < WidthInBytes; ix += BytesPerPixel)
					{
						int x = ix / BytesPerPixel;
						R = 0; G = 0; B = 0;
						for(int i = 0; i < pts.Count; i++)
						{
	   					   r = /*Math.Sqrt*/((x-pts[i].X)*(x-pts[i].X) + (y-pts[i].Y)*(y-pts[i].Y));
	   					   double ex = Math.Exp(-r/k/k);
	   					   //double ex = k*k / (r + 1);
	   					   R += (int)(colors[i].R * ex) % 256;
	   					   G += (int)(colors[i].G * ex) % 256;
	   					   B += (int)(colors[i].B * ex) % 256;
						}
						CurrentLine[ix + 0] = (byte)((B / m) % 256);
						CurrentLine[ix + 1] = (byte)((G / m) % 256);
						CurrentLine[ix + 2] = (byte)((R / m) % 256);
					}
	            }  );
	            bmp.UnlockBits(bitmapData);
            }
		//	pictureBox1.Refresh();
			Refresh();
		}

		        
		public void Draw2()
		{
			if (bmp == null || pts == null) return;
			
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

			Invalidate();
		}

		void MainFormPaint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			pictureBox1.Image = bmp;
		}

		void PictureBox1SizeChanged(object sender, EventArgs e)
		{
//			bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
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
		
		void BtAddClick(object sender, EventArgs e)
		{
	        // colorDialog1.Color = lvi.BackColor;
			
			if (colorDialog1.ShowDialog() == DialogResult.OK)
			{
				pts.Add(new Point(pictureBox1.Width / 2, pictureBox1.Height / 2));
		        int n = pts.Count - 1;
	  			ListViewItem lvi = listView1.Items.Insert(n, string.Format("Pt {0} ({1},{2})", n, pts[n].X, pts[n].Y));
				
	  			listView1.Items[n].BackColor = colorDialog1.Color;
				
				listView1.Items[n].Selected = true;
				Draw();
			}
			
			
		}
		void NumericUpDown1ValueChanged(object sender, EventArgs e)
		{
			Draw();
		}

		void NumericUpDown2ValueChanged(object sender, EventArgs e)
		{
			Draw();
		}
	}
}
