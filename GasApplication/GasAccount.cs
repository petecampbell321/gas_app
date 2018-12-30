using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasApplication
{
    class GasAccount
    {
        // add attributes & properties here
        private int accRefNo;               // account reference number
        private String name;                // name of account holder
        private String address;             // address of account holder
        private double balance;             // balance of account (in £)
        private double units;               // current amount of units used by account holder
        private double unitCost = 0.02;     // cost per unit [initialised = 0.02]
        private Boolean active;             // indicates if the account is active or not



        // add behaviour here

        public GasAccount(int intNewAccRefNo, String strNewName, String strNewAddress)
        {
            accRefNo = intNewAccRefNo;
            name = strNewName;
            address = strNewAddress;
            balance = 0.0;
            units = 0.0;
            active = true;
        }

        public double deposit(double amount)
        {
            balance -= amount;
            return balance;
        }

        public void recordUnits(double unitsUsed)
        {
            units += unitsUsed;
        }

        public double calcBalance()
        {
            balance = units * unitCost;
            return balance;
        }

        // getters
        public String getDetails()
        {
            return accRefNo + " " + name;
        }

        public int getRef()
        {
            return accRefNo;
        }

        public String getName()
        {
            return name;
        }

        public String getAdd()
        {
            return address;
        }

        public double getUnits()
        {
            return units;
        }

        public double getUnitCost()
        {
            return unitCost;
        }

        public double getBalance()
        {
            return balance;
        }

        public String closedDetails()
        {
            return accRefNo + " " + name + " " + "[CLOSED]";
        }

        public Boolean getActive()
        {
            return active;
        }



        //setters
        public void setUnits(double newUnits)
        {
            unitCost = newUnits;
        }

        public void setActive(Boolean value)
        {
            active = value;
        }
    }
}
