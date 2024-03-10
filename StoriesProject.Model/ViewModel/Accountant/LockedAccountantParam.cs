using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoriesProject.Model.ViewModel.Accountant
{
    public class LockedAccountantParam
    {
        public Guid AccountantId { get; set; }
        public bool IsLocked { get; set; }
    }
}
