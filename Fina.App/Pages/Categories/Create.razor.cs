using Fina.Core.Handlers;
using Fina.Core.Requests.Categories;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Fina.App.Pages.Categories
{
    public partial class CreateCategoryPage : ComponentBase
    {
        #region properties


        public bool IsBusy { get; set; } = false;
        public CreateCategoryRequest Model { get; set; } = new();

        #endregion

        #region services

        [Inject]
        public ICategoryHandler Handler { get; set; } = null!;
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public ISnackbar snackbar { get; set; }

        #endregion

        #region methods
        public async Task OnValidSubmitAsync()
        {
            IsBusy = true;
            try
            {
                var result = await Handler.CreateCategoryAsync(Model);
                if (result.IsSucces)
                {
                    snackbar.Add(result.Message, Severity.Success);
                    NavigationManager.NavigateTo("/categories");
                }
                else
                {
                    snackbar.Add(result.Message, Severity.Error);
                }
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion
    }
}
