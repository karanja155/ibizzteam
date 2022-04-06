﻿using Saas.SignupAdministration.Web.Services.StateMachine;


namespace Saas.SignupAdministration.Web.Services
{
    public class OnboardingWorkflow
    {
        private readonly IAdminServiceClient _adminServiceClient;
        private readonly IPersistenceProvider _persistenceProvider;

        public OnboardingWorkflowItem OnboardingWorkflowItem { get; internal set; }
        public OnboardingWorkflowState OnboardingWorkflowState { get; internal set; }

        public OnboardingWorkflowState.States CurrentState
        {
            get
            {
                return OnboardingWorkflowState.CurrentState;

            }
        }

        public OnboardingWorkflow(IAdminServiceClient adminServiceClient, IPersistenceProvider persistenceProvider)
        {
            _adminServiceClient = adminServiceClient;
            _persistenceProvider = persistenceProvider;

            OnboardingWorkflowItem item = _persistenceProvider.Retrieve<OnboardingWorkflowItem>(SR.OnboardingWorkflowItemKey);
            OnboardingWorkflowState state = _persistenceProvider.Retrieve<OnboardingWorkflowState>(SR.OnboardingWorkflowStateKey);

            OnboardingWorkflowItem = (item is null) ? new() : item;
            OnboardingWorkflowState = (state is null) ? new() : state;
        }

        public void TransitionState(OnboardingWorkflowState.Triggers trigger)
        {
            OnboardingWorkflowState.CurrentState = OnboardingWorkflowState.Transition(trigger);
        }

        public async Task OnboardTenant()
        {
            NewTenantRequest tenantRequest = new()
            {
                Name = OnboardingWorkflowItem.OrganizationName,
                Route = OnboardingWorkflowItem.TenantRouteName,
                // TODO : Add Email with info from logged in user
                CreatorEmail = "test@email.com",
                ProductTierId = OnboardingWorkflowItem.ProductId,
                CategoryId = OnboardingWorkflowItem.CategoryId
                
            };

            //TODO: Call new Admin API
            await _adminServiceClient.TenantsPOSTAsync(tenantRequest);

            OnboardingWorkflowItem.IsComplete = true;
            OnboardingWorkflowItem.Created = DateTime.Now;
        }

        public void PersistToSession()
        {
            _persistenceProvider.Persist(SR.OnboardingWorkflowStateKey, OnboardingWorkflowState);
            _persistenceProvider.Persist(SR.OnboardingWorkflowItemKey, OnboardingWorkflowItem);
        }
    }
}
