using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystem
{
    public class TariffPlanRecord
    {
        public DateTime StartDate;
        public DateTime? EndDate;
        public ITariffPlan TariffPlan;
    }
}
