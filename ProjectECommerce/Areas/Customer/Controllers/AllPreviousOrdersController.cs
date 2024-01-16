using Microsoft.AspNetCore.Mvc;
using ProjectECommerce.DataAccess.Repoistory.IRepository;
using ProjectECommerce.Models;
using ProjectECommerce.Utility;

namespace ProjectECommerce.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class AllPreviousOrdersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AllPreviousOrdersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;            
        }


        public IActionResult Index(int orderId)
        {
            var previousOrders = _unitOfWork.OrderDetail.GetAll(m => m.OrderHeaderId == orderId, includeproperties: "Product");
            return View(previousOrders);
        }
    }
}
