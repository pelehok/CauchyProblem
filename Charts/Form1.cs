﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Charts
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

        public void Draw1(decimal[] values,decimal[] x)
        {
            for (int i = 0; i < values.Length; i++)
            {
                chart2.Series[0].Points.AddXY(x[i], values[i]);
            }
        }
        public void Draw2(decimal[] values,decimal[] x)
        {
	        for (int i = 0; i < values.Length; i++)
	        {
		        chart2.Series[1].Points.AddXY(x[i], values[i]);
	        }
        }
	}
}