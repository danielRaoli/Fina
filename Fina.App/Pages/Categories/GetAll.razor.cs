using Fina.Core.Entities;
using Fina.Core.Handlers;
using Fina.Core.Requests.Categories;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Formats.Asn1;
using System.Linq.Expressions;

namespace Fina.App.Pages.Categories
{
    public class GetAllCategoriesPage : ComponentBase
    {
        #region properties
        public bool IsBusy { get; set; } = false;
        public List<Category> Categories { get; set; } = [];

        #endregion

        #region services
        [Inject]
        public ICategoryHandler Handler { get; set; } = null!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;
        [Inject]
        public IDialogService Dialog { get; set; } = null!;

        #endregion

        #region overrides

        protected async override Task OnInitializedAsync()
        {
            IsBusy = true;
            try
            {
                var request = new GetAllCategories();
                var result = await Handler.GetAllAsync(request);

                if (result.IsSucces)
                {
                    Categories = result.Data ?? [];
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
        #region methods

        public async void OnDeleleteButtonClickAsync(int id, string title)
        {
            var result = await Dialog.ShowMessageBox("Atencao", $" ao prosseguir a categoria {title}, vai ser excluida", yesText: "Excluir", cancelText: "Cancelar");
            if (result is true)
            {
                await OnDeleteAsync(id, title);

                StateHasChanged();
            }
        }
        public async Task OnDeleteAsync(int id, string title)
        {
            try
            {
                var request = new DeleteCategoryRequest { CategoryId = id };

                await Handler.DeleteAsync(request);
                Categories.RemoveAll(x => x.Id == id);
                Snackbar.Add($"Categoria: {title} removida", Severity.Info);
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }

        #endregion
    }
}
