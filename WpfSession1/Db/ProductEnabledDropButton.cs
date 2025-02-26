using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSession1.Db
{
    public partial class Product
    {
        public bool IsAvailableToDrop
        {
            get => OrderProducts.Count == 0;
        }
    }
}
