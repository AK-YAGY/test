using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{
    public partial class Form1 : Form
    {
        HObject Image;
        HTuple width, height, Grayval1;

        public Form1()
        {
            InitializeComponent();
            InitialWindow(hSmartWindowControl1);

        }
        private void InitialWindow(HSmartWindowControl box)
        {
            box.Click += new System.EventHandler(Hsw_click);
            box.MouseWheel += new System.Windows.Forms.MouseEventHandler(Hsw_mouseWheel);
        }
        private void Hsw_click(object sender, EventArgs e)
        {
            HSmartWindowControl hsw = (HSmartWindowControl)sender;
            hsw.Focus();
        }

        private void Hsw_mouseWheel(object sender, MouseEventArgs e)
        {
            HSmartWindowControl hSmartWindowControl = (HSmartWindowControl)sender;
            if (e.X > 0 && e.X < hSmartWindowControl.Size.Width && e.Y > 0 && e.Y < hSmartWindowControl.Size.Height)
            {
                hSmartWindowControl.HSmartWindowControl_MouseWheel(sender, e);
            }
        }


        private void hSmartWindowControl1_HMouseMove(object sender, HMouseEventArgs e)
        {
            if (Image == null)
            {
                return;
            }
            else
            {
                HTuple Row = (int)e.Y;
                HTuple Col = (int)e.X;
                HOperatorSet.GetImageSize(Image, out width, out height);
                if (Row >= 0 && Row < height && Col >= 0 && Col < height)
                {
                    HOperatorSet.GetGrayval(Image, Row, Col, out Grayval1);
                    labelX1.ForeColor = Color.Black;
                    labelX1.Text = $"Row:{Row.D.ToString("0")} Col:{Col.D.ToString("0")} Val:{Grayval1.D.ToString("0")}";
                    //labelX1.Text = $"Row:{Row.D.ToString("0")} Col:{Col.D.ToString("0")} Val:{Grayval1.D.ToString("0")}";
                }
            }

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Filter = "图片文件|*.bmp;*.png;*.jpg;*.jpeg|所有文件|*.*";
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                HOperatorSet.ReadImage(out Image, openFileDialog.FileName);
                HOperatorSet.GetImageSize(Image, out width, out height);
                HOperatorSet.DispObj(Image, hSmartWindowControl1.HalconWindow);
                HOperatorSet.SetPart(hSmartWindowControl1.HalconWindow, 0, 0, height - 1, width - 1);
            }
        }
    }
}
