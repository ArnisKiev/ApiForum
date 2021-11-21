using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ApiForum.ForumManagers;
using Microsoft.AspNetCore.Identity;
using ApiForum.DataBase.Models;
using ApiForum.Models;
using ApiForum.Managers;

namespace ApiForum.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[action]")]
    public class ApiController : Controller
    {
        SubscriberManager subscriberManager;
        ArticlesManager _articleManager;

        UserManager<User> _userManager;
        public ApiController(UserManager<User> usermanager)
        {
            subscriberManager = new SubscriberManager(usermanager);
            _userManager = usermanager;
            _articleManager = new ArticlesManager();
        }

        [HttpGet("{id}/{page}")]
        public async Task<ActionResult<List<Articles>>> GetArticles(string id, int page)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                return await subscriberManager.GetNewArticlesForUser(id, page - 1);
            }
            return null;
        }

        [HttpGet("{id}/{page}")]
        public async Task<ActionResult<List<Articles>>> GetArticlesParticularPublisher(string id, int page)
        {

            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                return await subscriberManager.GetArticlesOfUser(id, page - 1);

            }
            return null;

        }

        [HttpGet("id")] async Task<ActionResult<User>> GetUser(string id, string password)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                if (await _userManager.CheckPasswordAsync(user, password))
                {
                    return user;
                }

                return null;
            }

            return null;
        }
        [HttpGet("id")]
        async Task<ActionResult<UserInfo>> GetUserInfo(string id)
        {

            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {

                return new UserInfo() { UserId = user.Id, Email = user.Email, Subscribers = await subscriberManager.GetCountOfSubscribersOfUser(user.Id) };
            }

            return null;
        }

        [HttpPost("{id}")]
        async Task<ActionResult<Articles>> LikeArticle(Articles article)
        {
            var art = await _articleManager.FindById(article.ArticlesId);
            if (art != null)
            {
                var updatedArticle = await _articleManager.LikeArticleAsync(article.ArticlesId);
                _articleManager.Save();
                return updatedArticle;
            }

            return BadRequest();
        }
        [HttpPost]
        async Task<ActionResult<Articles>> AddArticle(Articles article)
        {
            if (ModelState.IsValid)
            {
                var art=_articleManager.Add(article);
                _articleManager.Save();
                return art;
            }
            return BadRequest();
        }
        [HttpGet]
        ActionResult<List<Articles>> GetAllArticles(int page)
        {
            return  _articleManager.GetItems().ToList().Skip(--page * 10).Take(10).ToList();
            
        }





        public IActionResult Index()
        {
            return View();
        }
    }
}
