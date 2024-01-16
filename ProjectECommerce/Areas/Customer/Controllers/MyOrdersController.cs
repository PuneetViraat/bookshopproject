using Microsoft.AspNetCore.Mvc;
using ProjectECommerce.DataAccess.Repoistory.IRepository;

namespace ProjectECommerce.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class MyOrdersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public MyOrdersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;            
        }
        public IActionResult Index( int orderId)
        {
            var myOrders = _unitOfWork.OrderDetail.GetAll(m => m.OrderHeaderId == orderId, includeproperties: "Product");
            return View(myOrders);

            
        }
    }
}
