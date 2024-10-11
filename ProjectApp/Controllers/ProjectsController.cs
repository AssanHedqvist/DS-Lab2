using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;
using ProjectApp.Core.Interfaces;
using ProjectApp.Models.Projects;
using Project = ProjectApp.Core.Project;

namespace ProjectApp.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private IProjectService _projectService;
        
        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        // GET: ProjectsController
        public ActionResult Index()
        {
            List<Project> projects = _projectService.GetAllByUserName(User.Identity.Name);
            List<ProjectVm> projectsVms = new List<ProjectVm>();
            foreach (var project in projects)
            {
                projectsVms.Add(ProjectVm.FromProject(project));
            }
            return View(projectsVms);
        }

        // GET: ProjectsController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                Project project = _projectService.GetById(id, User.Identity.Name); // current user
                if(!project.UserName.Equals(User.Identity.Name)) return BadRequest();
                ProjectDetailsVm detailsVm = ProjectDetailsVm.FromProject(project);
                //skapar mapper projekt till projectdetailsVm
                return View(detailsVm);
            }
            catch (DataException ex)
            {
                return BadRequest();
            }
        }
    
        // GET: ProjectsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProjectsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateProjectVm projectVm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string title = projectVm.Title;
                    string userName = User.Identity.Name;
                    
                    _projectService.Add(userName, title);
                    return RedirectToAction("Index");
                }
                return View(projectVm);
            }
            catch(DataException ex)
            {
                return View(projectVm);
            }
            
        }

        // GET: ProjectsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProjectsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProjectsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProjectsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
