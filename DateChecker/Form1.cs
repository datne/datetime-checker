using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NUnit.Framework;

namespace DateChecker
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txbDay.Clear();
            txbMonth.Clear();
            txbYear.Clear();
        }

        public bool IsLeapYear(ushort year)
        {
            if (year % 4 != 0) return false;
            if (year % 100 != 0) return true;
            if (year % 400 != 0) return false;
            return true;
        }

        public int DaysIsMonth(byte month, ushort year)
        {
            switch (month)
            {
                case 4:
                case 6:
                case 9:
                case 11: return 30;
                case 2:
                    {
                        if (IsLeapYear(year)) return 29;
                        return 28;
                    }
                default: return 31;
            }
        }

        public bool IsValidDay(byte day, byte month, ushort year)
        {
            if (IsMonth(month.ToString()) == true)
                if (IsDay(day.ToString()) == true)
                    if (day <= DaysIsMonth(month, year))
                        return true;
            return false;
        }

        public bool IsYear(string year)
        {
            try
            {
                ushort bYear = ushort.Parse(year);
                if (ushort.Parse(year) >= 1000 && ushort.Parse(year) <= 3000)
                    return true;
                else
                    MessageBox.Show("Input data for Year is out of range!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (OverflowException)
            {
                if (year.Contains("-"))
                    MessageBox.Show("Input data for Year is incorrect format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Input data for Year is out of range!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FormatException)
            {
                MessageBox.Show("Input data for Year is incorrect format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        public bool IsMonth(string month)
        {
            try
            {
                byte bMonth = byte.Parse(month);
                if (byte.Parse(month) >= 1 && byte.Parse(month) <= 12)
                    return true;
                else
                    MessageBox.Show("Input data for Month is out of range!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (OverflowException)
            {
                if (month.Contains("-"))
                    MessageBox.Show("Input data for Month is incorrect format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Input data for Month is out of range!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FormatException)
            {
                MessageBox.Show("Input data for Month is incorrect format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        public bool IsDay(string day)
        {
            try
            {
                byte bDay = byte.Parse(day);
                if (byte.Parse(day) >= 1 && byte.Parse(day) <= 31)
                    return true;
                else
                    MessageBox.Show("Input data for Day is out of range!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (OverflowException )
            {
                if (day.Contains("-"))
                    MessageBox.Show("Input data for Day is incorrect format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Input data for Day is out of range!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FormatException)
            {
                MessageBox.Show("Input data for Day is incorrect format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (IsDay(txbDay.Text) == true && IsMonth(txbMonth.Text) == true && IsYear(txbYear.Text) == true)
            {
                byte d = byte.Parse(txbDay.Text);
                byte m = byte.Parse(txbMonth.Text);
                ushort y = ushort.Parse(txbYear.Text);
                if (IsValidDay(d, m, y) == true)
                    MessageBox.Show("" + txbDay.Text + "/" + txbMonth.Text + "/" + txbYear.Text + " is correct datetime!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("" + txbDay.Text + "/" + txbMonth.Text + "/" + txbYear.Text + " is NOT correct datetime!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure exit?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                e.Cancel = false;
            else
                e.Cancel = true;
        }
    }

    public class DateTest
    {
        Form1 f = new Form1();

        #region Test IsLeapYear
        [Test]
        public void IsLeapYear_UTC01()
        {
            Assert.AreEqual(true, f.IsLeapYear(2016));  // test năm nhuận
        }

        [Test]
        public void IsLeapYear_UTC02()
        {
            Assert.AreEqual(false, f.IsLeapYear(2018)); // test năm không phải năm nhuận
        }

        #endregion

        #region Test IsDay

        [Test] // test day of out range 
        public void IsDay_UTC01()
        {
            Assert.AreEqual(true, f.IsDay(1.ToString()));
        }

        [Test] // test day correct format
        public void IsDay_UTC02()
        {
            Assert.AreEqual(true, f.IsDay(15.ToString()));
        }

        [Test] // test day correct format
        public void IsDay_UTC03()
        {
            Assert.AreEqual(true, f.IsDay(31.ToString()));
        }

        [Test] // test day correct format
        public void IsDay_UTC04()
        {
            Assert.AreEqual(false, f.IsDay(0.ToString()), "Input data for Day is out of range!");
        }

        [Test] // test day out of range
        public void IsDay_UTC05()
        {
            Assert.AreEqual(false, f.IsDay(32.ToString()), "Input data for Day is out of range!");
        }

        [Test] // test day incorrect format
        public void IsDay_UTC06()
        {
            Assert.AreEqual(false, f.IsDay(255.ToString()), "Input data for Day is incorrect format!");
        }

        [Test]
        public void IsDay_UTC07()
        {
            Assert.AreEqual(false, f.IsDay((-1).ToString()), "Input data for Day is out of range!");
        }
        [Test]
        public void IsDay_UTC08()
        {
            Assert.AreEqual(false, f.IsDay(256.ToString()), "Input data for Day is out of range!");
        }
        [Test] // test day incorrect format
        public void IsDay_UTC09()
        {
            Assert.AreEqual(false, f.IsDay("a121day"), "Input data for Day is incorrect format!");
        }
     
        [Test] // test day incorrect format
        public void IsDay_UTC10()
        {
            Assert.AreEqual(false, f.IsDay("+ ; "), "Input data for Day is incorrect format!");
        }

        #endregion

        #region Test IsMonth

        [Test] // test month of out range 
        public void IsMonth_UTC04()
        {
            Assert.AreEqual(false, f.IsMonth(0.ToString()), "Input data for Month is out of range!");
        }

        [Test] // test month correct format
        public void IsMonth_UTC02()
        {
            Assert.AreEqual(true, f.IsMonth(6.ToString()));
        }

        [Test] // test month correct format
        public void IsMonth_UTC01()
        {
            Assert.AreEqual(true, f.IsMonth(1.ToString()));
        }

        [Test] // test month correct format
        public void IsMonth_UTC03()
        {
            Assert.AreEqual(true, f.IsMonth(12.ToString()));
        }

        [Test] // test month out of range
        public void IsMonth_UTC05()
        {
            Assert.AreEqual(false, f.IsMonth(13.ToString()), "Input data for Month is out of range!");
        }

        [Test] // test month incorrect format
        public void IsMonth_UTC07()
        {
            Assert.AreEqual(false, f.IsMonth((-1).ToString()), "Input data for Month is incorrect format!");
        }

        [Test] // test month incorrect format
        public void IsMonth_UTC09()
        {
            Assert.AreEqual(false, f.IsMonth("12month"), "Input data for Month is incorrect format!");
        }

        [Test] // test month incorrect format
        public void IsMonth_UTC06()
        {
            Assert.AreEqual(false, f.IsMonth(255.ToString()), "Input data for Month is out of range!");
        }

        [Test] // test month incorrect format
        public void IsMonth_UTC08()
        {
            Assert.AreEqual(false, f.IsMonth(256.ToString()), "Input data for Month is out of range!");
        }

        [Test] // test month incorrect format
        public void IsMonth_UTC10()
        {
            Assert.AreEqual(false, f.IsMonth("- + * ;"), "Input data for Month is incorrect format!");
        }

        #endregion

        #region Test IsYear

        [Test] // test year correct format
        public void IsYear_UTC02()
        {
            Assert.AreEqual(true, f.IsYear(2000.ToString()));
        }

        [Test] // test year correct format
        public void IsYear_UTC01()
        {
            Assert.AreEqual(true, f.IsYear(1000.ToString()));
        }

        [Test] // test year correct format
        public void IsYear_UTC03()
        {
            Assert.AreEqual(true, f.IsYear(3000.ToString()));
        }

        [Test] // test year out of range
        public void IsYear_UTC04()
        {
            Assert.AreEqual(false, f.IsYear(999.ToString()), "Input data for Year is out of range!");
        }
        [Test]
        public void IsYear_UTC06()
        {
            Assert.AreEqual(false, f.IsYear(0.ToString()), "Input data for Year is out of range!");
        }


        [Test] // test year incorrect format
        public void IsYear_UTC08()
        {
            Assert.AreEqual(false, f.IsYear((-1).ToString()), "Input data for Year is incorrect format!");
        }

        [Test] // test year out of range
        public void IsYear_UTC05()
        {
            Assert.AreEqual(false, f.IsYear(3001.ToString()), "Input data for Year is out of range!");
        }

        [Test] // test year incorrect format
        public void IsYear_UTC10()
        {
            Assert.AreEqual(false, f.IsYear("12year"), "Input data for Year is incorrect format!");
        }

        [Test] // test year out of range
        public void IsYear_UTC07()
        {
            Assert.AreEqual(false, f.IsYear(65535.ToString()), "Input data for Year is out of range!");
        }

        [Test] // test year out of range
        public void IsYear_UTC09()
        {
            Assert.AreEqual(false, f.IsYear(65536.ToString()), "Input data for Year is out of range!");
        }

        [Test] // test year incorrect format
        public void IsYear_UTC11()
        {
            Assert.AreEqual(false, f.IsYear("+ - ; _ "), "Input data for Year is incorrect format!");
        }

        #endregion

        #region Test IsValidDay


        [Test] 
        public void IsValidDay_UCT01()
        {
            Assert.AreEqual(true, f.IsValidDay(31, 7, 2018));
        }


        [Test] 
        public void IsValidDay_UCT02()
        {
            Assert.AreEqual(true, f.IsValidDay(30, 6, 2018));
        }

        [Test] 
        public void IsValidDay_UCT03()
        {
            Assert.AreEqual(true, f.IsValidDay(28, 2, 2016));
        }

        [Test]
        public void IsValidDay_UCT04()
        {
            Assert.AreEqual(false, f.IsValidDay(29, 2, 2018));
        }

        [Test]
        public void IsValidDay_UCT05()
        {
            Assert.AreEqual(false, f.IsValidDay(30, 2, 2016), "");
        }

        [Test] 
        public void IsValidDay_UCT06()
        {
            Assert.AreEqual(false, f.IsValidDay(31, 2, 2018));
        }
     

        #endregion

        #region Test DaysIsMonth

        [Test]  // test 29 DaysIsMonth 2
        public void DaysIsMonth_UT01()
        {
            Assert.AreEqual(29, f.DaysIsMonth(2, 2016));
        }

        //[Test] 
        //public void DaysIsMonth_UT02()
        //{
        //    Assert.AreEqual(29, f.DaysIsMonth(2, 2018));
        //}

        [Test] // test 30 DaysIsMonth 6
        public void DaysIsMonth_UT02()
        {
            Assert.AreEqual(30, f.DaysIsMonth(6, 2018));
        }
        [Test]
        public void DaysIsMonth_UT04()
        {
            Assert.AreEqual(28, f.DaysIsMonth(2, 2018));
        }

        //[Test]  // test 31 NOT DaysIsMonth 6
        //public void DaysIsMonth_UT04()
        //{
        //    Assert.AreEqual(31, f.DaysIsMonth(6, 2018));
        //}


        [Test] // test 31 DaysIsMonth 7
        public void DaysIsMonth_UT03()
        {
            Assert.AreEqual(29, f.DaysIsMonth(2, 2020));
        }

        #endregion
    }
}
