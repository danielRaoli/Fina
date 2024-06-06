using Fina.Core.Entities;
using Fina.Core.Handlers;
using Fina.Core.Requests.Categories;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Interfaces;
using System.Data;

namespace Fina.App.Pages.Categories
{
    public class UpdateCategoryPage : ComponentBase
    {

        public bool IsBusy { get; set; } = false;
        [Parameter]
        public int Id { get; set; }

        public UpdateCategoryRequest Model { get; set; } = new UpdateCategoryRequest();
        [Inject]
        public ICategoryHandler Handler { get; set; } = null!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        protected async override Task OnInitializedAsync()
        {
            var request = new GetCategoryById { CategoriyId = Id};
            var categoryToEdit = await Handler.GeByIdAsync(request);   

            Model.Title = categoryToEdit.Data.Title;

            Model.Description = categoryToEdit.Data.Title;

            Model.CategoryId = Id;
        }


        public async Task OnValidSubmitAsync()
        {
            IsBusy = true;
            try
            {
                var result = await Handler.UpdateCategoryAsync(Model);

                if (result.IsSucces)
                {
                    Snackbar.Add(result.Message, Severity.Success);
                }
                else
                    Snackbar.Add(result.Message, Severity.Error);

                StateHasChanged();

            }catch (Exception ex) 
            {
            Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }


    }
}
