using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace TraversalCoreProje.Areas.Member.Controllers
{
    [Area("Member")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public IActionResult Index(int appUserId)
        {
            var comments = _commentService.TGetListCommentByUserId(appUserId);
            return View(comments);
        }
    }
}
