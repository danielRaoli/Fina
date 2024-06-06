using Fina.Core.Entities;
using Fina.Core.Handlers;
using Fina.Core.Requests.Categories;
using Fina.Core.Requests.Transactions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Fina.App.Pages.Transactions
{
    public class CreateTransactionPage : ComponentBase
    {
        #region properties 
        public bool IsBusy { get; set; }
        public List<Category> Categories { get; set; } = [];
        public CreateTransactionRequest Model { get; set; } = new();

        #endregion

        #region services 
        [Inject]
        public ITransactionHandler Handler { get; set; } = null!;
        [Inject]
        public ICategoryHandler CategoryHandler { get; set; } = null!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } =null!;
        #endregion

        #region overrides 

        protected async override Task OnInitializedAsync()
        {
            
            try
            {
                var request = new GetAllCategories(); 
                var categories = await CategoryHandler.GetAllAsync(request);
               
                Categories = categories.Data ?? [];  
            }
            catch(Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }
        #endregion

        #region methods

        public async Task OnValidSubmitAsync()
        {
            IsBusy = true;
            try
            {
               
                var result = await Handler.CreateAsync(Model);

                if (result.IsSucces)
                {
                    Snackbar.Add(result.Message, Severity.Success);
                    NavigationManager.NavigateTo("/transaction");
                }
                else
                {
                    Snackbar.Add(result.Message, Severity.Error);
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion
    }
}
