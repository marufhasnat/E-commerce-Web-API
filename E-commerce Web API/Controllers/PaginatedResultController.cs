using Microsoft.AspNetCore.Mvc;

namespace E_commerce_Web_API.Controllers
{
    public class PaginatedResultController<T> : Controller
    {
        public IEnumerable<T> Items { get; set; } = new List<T>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int) Math.Ceiling((double) TotalCount / PageSize);

        public IActionResult Index()
        {
            return View();
        }
    }
}
