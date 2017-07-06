using KPIWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XKPI.Models;

namespace KPIWebApi.Models.XKPI
{
    public class XKPIContext:XKPIEntities
    {
        public XKPIContext() {
            this.Configuration.LazyLoadingEnabled = false;

        }
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var newException = new FormattedDbEntityValidationException(e);
                throw newException;
            }
        }
    }
}
