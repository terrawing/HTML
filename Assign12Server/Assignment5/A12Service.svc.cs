using Assignment5.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Assignment5
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "A12Service" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select A12Service.svc or A12Service.svc.cs at the Solution Explorer and start debugging.
    public class A12Service : IA12Service
    {
        private Manager m = new Manager();

        // Add a new customer
        public CustomerWithData CustomerAdd(CustomerAdd newItem)
        {
            if (newItem == null)
            {
                return null;
            }

            var vc = new ValidationContext(newItem, null, null);
            var modelStateIsValid = Validator.TryValidateObject(newItem, vc, null, true);

            if (modelStateIsValid)
            {
                var addedItem = m.CustomerAdd(newItem);

                if (addedItem == null)
                {
                    return null;
                }
                else
                {
                    return addedItem;
                }
            }
            else
            {
                return null;
            }
        }

        // Edit (PUT) a customer's address
        public CustomerWithData CustomerEditAddress(int? id, CustomerEditAddress editedItem)
        {
            if (editedItem == null)
            {
                return null;
            }

            if (id.GetValueOrDefault() != editedItem.CustomerId)
            {
                return null;
            }

            var vc = new ValidationContext(editedItem, null, null);
            var modelStateIsValid = Validator.TryValidateObject(editedItem, vc, null, true);

            if (modelStateIsValid)
            {
                var changedItem = m.CustomerEditAddress(editedItem);

                if (changedItem == null)
                {
                    return null;
                }
                else
                {
                    return changedItem;
                }
            }
            else
            {
                return null;
            }
        }

        // Get all customers
        public IEnumerable<CustomerWithData> CustomerGetAll()
        {
            return m.CustomerGetAll();
        }

        // Get a customer by Id
        public CustomerWithData CustomerGetById(int? id)
        {
            if (!id.HasValue) { return null; }

            var o = m.CustomerGetById(id.Value);

            if (o == null)
            {
                return null;
            }
            else
            {
                return o;
            }
        }

        // COMMAND to set a customer support rep
        public void CustomerSetSupportRep(int? id, CustomerSupportRep item)
        {
            if (item == null)
            {
                return;
            }

            if (id.GetValueOrDefault() != item.CustomerId)
            {
                return;
            }

            var vc = new ValidationContext(item, null, null);
            var modelStateIsValid = Validator.TryValidateObject(item, vc, null, true);

            if (modelStateIsValid)
            {
                m.CustomerSetSupportRep(item);
            }
            else
            {
                return;
            }
        }

        // Get all invoices
        public IEnumerable<InvoiceWithData> InvoiceGetAll()
        {
            return m.InvoiceGetAll();
        }
    }
}
