using E2EViewModals.Subscription;
using System;
using System.Collections.Generic;

namespace E2ERepositories.Interface
{
    public interface ISubscriptionRepository
    {
        List<SubscriptionInfoViewModal> GetSubscriptionInfo();

        
    }
}
