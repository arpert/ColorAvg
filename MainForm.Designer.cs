/*
 * Created by SharpDevelop.
 * User: arpert
 * Date: 2014-11-05
 * Time: 14:50
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ColorAvg
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.listView1 = new System.Windows.Forms.ListView();
			this.btColor = new System.Windows.Forms.Button();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox1.Location = new System.Drawing.Point(3, 71);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(400, 341);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.SizeChanged += new System.EventHandler(this.PictureBox1SizeChanged);
			this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PictureBox1MouseClick);
			// 
			// listView1
			// 
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(409, 71);
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(106, 226);
			this.listView1.TabIndex = 2;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.List;
			this.listView1.SelectedIndexChanged += new System.EventHandler(this.ListView1SelectedIndexChanged);
			// 
			// btColor
			// 
			this.btColor.Location = new System.Drawing.Point(409, 303);
			this.btColor.Name = "btColor";
			this.btColor.Size = new System.Drawing.Size(75, 23);
			this.btColor.TabIndex = 3;
			this.btColor.Text = "Color";
			this.btColor.UseVisualStyleBackColor = true;
			this.btColor.Click += new System.EventHandler(this.BtColorClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(527, 424);
			this.Controls.Add(this.btColor);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.pictureBox1);
			this.Name = "MainForm";
			this.Text = "ColorAvg";
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainFormPaint);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.Button btColor;
		private System.Windows.Forms.ColorDialog colorDialog1;
	}
}
