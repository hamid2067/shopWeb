using Data;
using Entities.Menu;
using Entities.Slide;
using Microsoft.AspNetCore.Mvc;

namespace shopWeb.ViewComponents
{
    public class SlideViewComponent : ViewComponent
    {
        private readonly IRepository<Slide> _myslide;

        public SlideViewComponent(IRepository<Slide> myslide)
        {
            _myslide = myslide;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var result = _myslide.Table.ToList();
            return View(result);
        }
    }
}
