using LibraryModel.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Configuration;
using System.ComponentModel.DataAnnotations;
using Merca_Darius_ExamenNew.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Merca_Darius_ExamenNew.Controllers
{
    [Authorize(Policy = "Managers")]
    public class RolesController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<IdentityUser> userManager;
        public RolesController(RoleManager<IdentityRole> roleMgr, UserManager<IdentityUser> userMgr)
        {
            roleManager = roleMgr;
            userManager = userMgr;
        }
        public ViewResult Index() => View(roleManager.Roles);

        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create([Required] string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await roleManager.CreateAsync(new
               IdentityRole(name));
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            return View(name);
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);

        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await
               roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "No role found");
            return View("Index", roleManager.Roles);
        }
        
        public async Task<IActionResult> Update(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            List<IdentityUser>? members = new List<IdentityUser>();
            List<IdentityUser>? NonMembers = new List<IdentityUser>();

            foreach (IdentityUser user in userManager.Users)
            {
                var list = await userManager.IsInRoleAsync(user, role.Name) ? members : NonMembers;
                list.Add(user);
            }
            return View(new RoleEdit
            {
                Role = role,
                Members = members,
                NonMembers = NonMembers
            });
        }


        [HttpPost]
        public async Task<IActionResult> Update(RoleModification model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.AddIds ?? new string[] { })
                {
                    IdentityUser user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.AddToRoleAsync(user,
                       model.RoleName);
                        if (!result.Succeeded)
                            Errors(result);
                    }
                }
                foreach (string userId in model.DeleteIds ?? new string[] {
})
                {
                    IdentityUser user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await
                       userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            Errors(result);
                    }
                }
            }
            if (ModelState.IsValid)
                return RedirectToAction(nameof(Index));
            else
                return await Update(model.RoleId);
        }
    }

}

