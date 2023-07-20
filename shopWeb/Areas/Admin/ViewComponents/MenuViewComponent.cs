using Data;
using Data.Repositories;
using Entities.Menu;
using Microsoft.AspNetCore.Mvc;

namespace shopWeb.Areas.Admin.ViewComponents
{
    public class MenuViewComponent:ViewComponent
    {
        private readonly IRepository<Menu> _mymenu;

        public MenuViewComponent(IRepository<Menu> mymenu)
        {
            this._mymenu = mymenu;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
           
            var result = _mymenu.Table.ToList();
            return View(result);
        }
    }
}
