using AutoMapper;
using E2ERepositories.Interface;
using E2EViewModals.Subscription;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2ERepositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        public List<SubscriptionInfoViewModal> GetSubscriptionInfo()
        {
            using (var db = new E2EWebPortalEntities())
            {
                var subscriptionInfo = db.sp_GetSubscriptionInfo().ToList();
                return Mapper.Map<List<sp_GetSubscriptionInfo_Result>, List<SubscriptionInfoViewModal>>(subscriptionInfo);
            }
        }

        
    }
}
