using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GasApplication
{
    public partial class frmMain : Form
    {
        private GasAccount[] data;  // Array for storing GasAccount objects
        private GasAccount temp;
        private const int MAX = 5;  // Maximum number of GasAccount objects
        private int nextRef = 1;    // Used for assigning the next GasAccount reference number
        private int count = 0;      // To keep track of number of GasAccounts

        public frmMain()
        {
            data = new GasAccount[MAX]; // initialise storage for GasAccount objects
            InitializeComponent();
            txtRef.Text = nextRef + "";
        }

        // this method will get updated information from the object when triggered
        private void updateFields()
        {
            try
            {
                txtRef.Text = data[lstAccounts.SelectedIndex].getRef().ToString();
                txtName.Text = data[lstAccounts.SelectedIndex].getName();
                txtAdd.Text = data[lstAccounts.SelectedIndex].getAdd();
                txtUnitsUsed.Text = data[lstAccounts.SelectedIndex].getUnits().ToString();
                txtBalance.Text = data[lstAccounts.SelectedIndex].getBalance().ToString();
                txtUnitCost.Text = data[lstAccounts.SelectedIndex].getUnitCost().ToString();
                
            }
            catch
            {
                
            }
        }

        // this is the handler for when the user clicks the "Add" button
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if ( txtName.Text == "" || txtAdd.Text == "")
            {
                MessageBox.Show("Please enter a name and an address before adding an account.");
            }
            else
            {
                if (count < MAX)
                {
                    // add a new Student
                    temp = new GasAccount(Convert.ToInt32(txtRef.Text),
                                                txtName.Text,
                                                txtAdd.Text);
                    data[count] = temp;
                    count++;
                    lstAccounts.Items.Add(temp.getDetails());

                    nextRef++;
                }
                txtRef.Text = Convert.ToString(nextRef);
            }
        }

        // this is the handler for when the user clicks the "Add" button
        private void btnRecord_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(txtRecUnits.Text) > 0)
                {
                    data[lstAccounts.SelectedIndex].recordUnits(Convert.ToDouble(txtRecUnits.Text));
                    txtUnitsUsed.Text = Convert.ToString(data[lstAccounts.SelectedIndex].getUnits());

                    txtBalance.Text = data[lstAccounts.SelectedIndex].calcBalance().ToString();
                }
                else
                {
                    MessageBox.Show("Units to be recorded must be greater than 0");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // this is the handler for when the user clicks the "Payment" button
        private void btnPayment_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(txtPayment.Text) > 0)
                {
                    txtBalance.Text = data[lstAccounts.SelectedIndex].deposit(Convert.ToDouble(txtPayment.Text)).ToString();
                }
                else
                {
                    MessageBox.Show("Payment into the account must be greater than 0");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        // this is the handler for when the user clicks the "Set Units" button
        private void btnSetUnits_Click(object sender, EventArgs e)
        {
            try
            {
                data[lstAccounts.SelectedIndex].setUnits(Convert.ToDouble(txtSetUnits.Text));

                txtUnitCost.Text = data[lstAccounts.SelectedIndex].getUnitCost().ToString();

                // recalculate balance based on new unit cost
                txtBalance.Text = data[lstAccounts.SelectedIndex].calcBalance().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        // this is the method that will sort the list items by reference number
        private void sortListRef()
        {
            int count = data.Length;
            if (count == MAX)
            {
                Boolean swaps;
                do
                {
                    // Pass through data array swapping pairs
                    // as necessary
                    swaps = false;

                    for (int i = 1; i < data.Length; i++)
                    {
                        GasAccount gas1 = data[i - 1];
                        GasAccount gas2 = data[i];

                        int ref1 = gas1.getRef();
                        int ref2 = gas2.getRef();

                        if (ref1 > ref2)
                        {
                            // values need to be swapped
                            data[i - 1] = gas2;
                            data[i] = gas1;
                            swaps = true;
                        }
                    }
                }
                while (swaps == true);
            }

        }

        // this is the method that will sort the list items by balance
        private void sortListBal()
        {
            int count = data.Length;
            if (count == MAX)
            {
                Boolean swaps;
                do
                {
                    // Pass through data array swapping pairs
                    // as necessary
                    swaps = false;

                    for (int i = 1; i < data.Length; i++)
                    {
                        GasAccount gas1 = data[i - 1];
                        GasAccount gas2 = data[i];

                        double bal1 = gas1.getBalance();
                        double bal2 = gas2.getBalance();

                        if (bal1 > bal2)
                        {
                            // values need to be swapped
                            data[i - 1] = gas2;
                            data[i] = gas1;
                            swaps = true;
                        }
                    }
                }
                while (swaps == true);
            }

        }

        // this is the method that will update the list
        private void updateList()
        {
            lstAccounts.Items.Clear();

            for (int i = 0; i < MAX; i++)
            {
                lstAccounts.Items.Add(data[i].getDetails());
            }
        }

        // this is the handler for when the user clicks the "Sort" button
        private void btnSort_Click(object sender, EventArgs e)
        {
            if (data.Length == MAX)
            {
                if (rdoRef.Checked == true)
                {
                    sortListRef();
                    updateList();
                }
                else
                {
                    sortListBal();
                    updateList();
                }
            }
        }

        // this is the handler for when the user clicks the "Close" button
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (data[lstAccounts.SelectedIndex].getBalance() == 0)
            {
                int i = lstAccounts.SelectedIndex;

                lstAccounts.Items.RemoveAt(i);
                lstAccounts.Items.Insert(i, data[i].closedDetails());

                data[i].setActive(false);
            }
            else
            {
                MessageBox.Show("Account must be paid off to be closed");
            }
        }

        // this is the handler for when the user clicks an item in the list
        private void lstAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateFields();

            // try/catch used to prevent 
            try
            {
                // Checking if fields should be disabled
                if (data[lstAccounts.SelectedIndex].getActive() == false)
                {
                    txtName.Enabled = false;
                    txtAdd.Enabled = false;
                    txtBalance.Enabled = false;
                    txtRecUnits.Enabled = false;
                    txtRecUnits.Text = "0";
                    txtPayment.Enabled = false;
                    txtPayment.Text = "0";
                    txtSetUnits.Enabled = false;
                    txtSetUnits.Text = "0";
                    txtUnitCost.Enabled = false;
                    txtUnitsUsed.Enabled = false;
                }
                else
                {
                    txtName.Enabled = true;
                    txtAdd.Enabled = true;
                    txtBalance.Enabled = true;
                    txtPayment.Enabled = true;
                    txtRecUnits.Enabled = true;
                    txtSetUnits.Enabled = true;
                    txtUnitCost.Enabled = true;
                    txtUnitsUsed.Enabled = true;
                }
            }
            catch
            {
                
            }
        }

    }
}
