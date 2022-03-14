using Demo.Models;
using Demo.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Demo.Web.Controllers
{

    public class EmployeeController : Controller
    {
        private readonly IHttpClientFactory _iHttpClientFactory;

        public EmployeeController(IHttpClientFactory iHttpClientFactory)
        {
            _iHttpClientFactory = iHttpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var client = _iHttpClientFactory.CreateClient("Url");
                IEnumerable<EmployeeViewModel> employeeViewModel = new List<EmployeeViewModel>();
                using (var res = await client.GetAsync("Employee/api/GetEmployee"))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        var result = await res.Content.ReadAsStringAsync();
                        employeeViewModel = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(result);
                        return View(employeeViewModel);
                    }
                    return NotFound();
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<IActionResult> InsertEmployeeTsk()
        {
            await GetPosition();
            return View();
        }

        public async Task<IEnumerable<SelectListPositionViewModel>> GetPosition()
        {
            try
            {
                var client = _iHttpClientFactory.CreateClient("Url");
                List<SelectListPositionViewModel> positionViewModel = new List<SelectListPositionViewModel>();
                using (var response = await client.GetAsync("Position/api/GetPosition"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        positionViewModel = JsonConvert.DeserializeObject<List<SelectListPositionViewModel>>(result);
                        ViewBag.Lst = positionViewModel;

                    }
                }
                return positionViewModel;



            }
            catch (Exception)
            {

                return null;
            }

        }

        [HttpPost]
        public async Task<ActionResult> InsertEmployeeTsk(EmployeeViewModel employeeViewModel)
        {
            try
            {

                var client = _iHttpClientFactory.CreateClient("Url");
                using (var response = await client.PostAsJsonAsync<EmployeeViewModel>("Employee/api/InsertEmployeeTsk", employeeViewModel))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        employeeViewModel = JsonConvert.DeserializeObject<EmployeeViewModel>(result);
                        return RedirectToAction("Index", "Employee");
                    }
                    else
                    {
                        ViewBag.mess = BadRequest();
                        await GetPosition();
                        return View(employeeViewModel);
                    }

                }

            }
            catch
            {

                throw;
            }

        }



        [HttpGet]
        public async Task<IActionResult> UpdateEmployeeTskList(int? employeeId)
        {
            try
            {
                var client = _iHttpClientFactory.CreateClient("Url");
                EmployeeViewModel employeeViewModel = new EmployeeViewModel();
                using (var res = await client.GetAsync($"Employee/api/GetEmployeeByEmployeeId/{employeeId}"))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        var result = res.Content.ReadAsStringAsync().Result;
                        employeeViewModel = JsonConvert.DeserializeObject<EmployeeViewModel>(result);
                        if (employeeViewModel == null)
                        {
                            return BadRequest("Content doesnot exists");

                        }
                        else
                        {
                            await GetPosition();
                            using (var response = await client.GetAsync($"EmployeeHistory/api/GetEmployeeHistory/{employeeId}"))
                            {
                                if (response.IsSuccessStatusCode)
                                {
                                    var resultEmpHistory = await response.Content.ReadAsStringAsync();
                                    employeeViewModel.EmployeeHistoryViewmodels = JsonConvert.DeserializeObject<List<EmployeeHistoryViewmodel>>(resultEmpHistory);
                                }

                            }

                            if (employeeId.HasValue)
                            {
                                TempData["employeHistoryId"] = employeeId.Value;

                            }
                            else
                            {
                                TempData["employeHistoryId"] = 0;
                            }
                            TempData.Keep();

                            return View(employeeViewModel);
                        }


                    }


                    return RedirectToAction("Index", "Employee");
                }
            }
            catch (Exception)
            {

                return BadRequest("Something went worng!!");
                throw;
            }


        }

        [HttpPost]
        public async Task<ActionResult> UpdateEmployeeTskList(EmployeeViewModel employeeViewModel)
        {
            try
            {
                var client = _iHttpClientFactory.CreateClient("Url");

                if (employeeViewModel.EmployeeId == 0)
                {
                    return BadRequest("Content doesnot Exists");
                }
                else
                {
                    using (var response = await client.PutAsJsonAsync<EmployeeViewModel>($"Employee/api/UpdateEmployeeTsk/{employeeViewModel.EmployeeJobHistoryId}", employeeViewModel))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            employeeViewModel = JsonConvert.DeserializeObject<EmployeeViewModel>(result);
                            return RedirectToAction("Index", "Employee");
                        }
                        else
                        {
                            ViewBag.Error = BadRequest();
                            return View(employeeViewModel);
                        }
                    }

                }



            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }

        }

        public async Task<ActionResult> DeleteEmployeeTsk(int employeeId)
        {
            try
            {
                var client = _iHttpClientFactory.CreateClient("Url");
                if (employeeId != 0)
                {
                    using (var res = await client.DeleteAsync($"Employee/api/DeleteEmployeeTsk/{employeeId}"))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index", "Employee");
                        }
                        else
                        {
                            return BadRequest("Api is not responding");
                        }

                    }

                }
                else
                {
                    return BadRequest("Content doesnot exists");
                }



            }
            catch (Exception)
            {
                return BadRequest("Something went worng!!");
                throw;
            }


        }

        public IActionResult InsertPositon()
        {
            return PartialView("_InsertPositionPartialView");
        }

        [HttpPost]
        public async Task<ActionResult> InsertPosition(PositionViewModel positionViewModel)
        {
            try
            {
                var client = _iHttpClientFactory.CreateClient("Url");

                if (ModelState.IsValid == true)
                {
                    using (var res = await client.PostAsJsonAsync("Position/api/InsertPosition", positionViewModel))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            var result = await res.Content.ReadAsStringAsync();
                            positionViewModel = JsonConvert.DeserializeObject<PositionViewModel>(result);
                        }
                        else
                        {
                            return BadRequest();
                        }
                    }

                }

                return RedirectToAction("Index", "Employee");
            }


            catch (Exception)
            {
                return BadRequest("SOmething went worng!!");
                throw;
            }

        }

        public async Task<IActionResult> InsertEmployeePosition(int? id)
        {
            int? employeHistoryId = (int?)TempData["employeHistoryId"];
            TempData.Keep();

            id = (int?)TempData["employeHistoryId"];
            await GetPosition();
            return PartialView("_InsertEmployeePositionPartialView");

        }

        [HttpPost]
        public async Task<ActionResult> InsertEmployeePosition(EmployeeHistoryViewmodel employeeHistoryViewmodel, int? id)
        {
            try
            {
                var client = _iHttpClientFactory.CreateClient("Url");

                id = (int?)TempData["employeHistoryId"];
                employeeHistoryViewmodel.EmployeeId = (int)TempData["employeHistoryId"];
                TempData.Keep();
                using (var res = await client.PostAsJsonAsync($"EmployeeHistory/api/InsertEmployeePosition/{id}", employeeHistoryViewmodel))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        var result = await res.Content.ReadAsStringAsync();
                        employeeHistoryViewmodel = JsonConvert.DeserializeObject<EmployeeHistoryViewmodel>(result);
                        return RedirectToAction("UpdateEmployeeTskList", "Employee", new { employeeId = id });
                    }
                    else
                    {
                        ViewBag.error = BadRequest();
                        return PartialView(employeeHistoryViewmodel);
                    }
                }




            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }


        }

        public async Task<ActionResult> UpdateEmployeePosition(int id)
        {
            try
            {
                var client = _iHttpClientFactory.CreateClient("Url");
                await GetPosition();

                EmployeeHistoryViewmodel employeeHistoryViewmodel = new EmployeeHistoryViewmodel();
                using (var res = await client.GetAsync($"EmployeeHistory/api/GetEmployeeHistoryByEmployeeHistoryId/{id}"))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        var result = await res.Content.ReadAsStringAsync();
                        employeeHistoryViewmodel = JsonConvert.DeserializeObject<EmployeeHistoryViewmodel>(result);

                    }
                }
                return PartialView("_UpdateEmployeePositionPartialView", employeeHistoryViewmodel);

            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }

        }

        [HttpPost]
        public async Task<ActionResult> UpdateEmployeePosition(EmployeeHistoryViewmodel employeeHistoryViewmodel)
        {
            try
            {
                var client = _iHttpClientFactory.CreateClient("Url");
                if (employeeHistoryViewmodel.EmployeeId == 0)
                {
                    return BadRequest("Content doesnot Exists");
                }
                else
                {
                    using (var res = await client.PutAsJsonAsync($"EmployeeHistory/api/UpdateEmployeeEmployeeHistory/{employeeHistoryViewmodel.EmployeeJobHistoryId}", employeeHistoryViewmodel))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            var result = await res.Content.ReadAsStringAsync();
                            employeeHistoryViewmodel = JsonConvert.DeserializeObject<EmployeeHistoryViewmodel>(result);
                            int? employeHistoryId = (int?)TempData["employeHistoryId"];
                            TempData.Keep();
                            return RedirectToAction("UpdateEmployeeTskList", "Employee", new { employeeId = employeHistoryId });
                        }
                        else
                        {
                            await GetPosition();
                            return View(employeeHistoryViewmodel);
                        }
                    }
                }


            }
            catch (Exception)
            {
                return BadRequest("Something Went Wrong!!");
                throw;
            }

        }


        public async Task<ActionResult> VerifyEmail(string email, int PersonId)
        {
            try
            {
                var client = _iHttpClientFactory.CreateClient("Url");
                bool emailExist = true;
                using (var res = await client.GetAsync($"Validation/api/verifyEmail/{email}/{PersonId}"))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        var result = await res.Content.ReadAsStringAsync();
                        emailExist = JsonConvert.DeserializeObject<bool>(result);
                        if (emailExist)
                        {
                            return Json(true);
                        }
                        else
                        {
                            return Json($"{email} is already in use.");
                        }

                    }
                }


                return Json(true);



            }
            catch (Exception)
            {

                throw;
            }


        }

        [AcceptVerbs("Get")]
        public async Task<ActionResult> VerifyPosition(string positionName)
        {
            try
            {
                var client = _iHttpClientFactory.CreateClient("Url");
                bool positionNameExist = true;
                HttpResponseMessage res = await client.GetAsync($"Validation/api/verifyPosition/{positionName}");
                if (res.IsSuccessStatusCode)
                {
                    var result = await res.Content.ReadAsStringAsync();
                    positionNameExist = JsonConvert.DeserializeObject<bool>(result);
                    if (positionNameExist)
                    {
                        return Json(true);
                    }

                    return Json($"{positionName} is already in use.");


                }
                return Json(true);



            }
            catch (Exception)
            {
                return Json(true);
            }
        }


        [AcceptVerbs("Get")]
        public IActionResult VerifyDate(EmployeeViewModel employeeViewModel)
        {
            if (TempData.ContainsKey("StartDate"))
            {
                employeeViewModel.StartDate = Convert.ToDateTime(TempData["StartDate"]);
                TempData.Keep();

            }
            var startDate = employeeViewModel.StartDate;
            var endDate = Convert.ToDateTime(employeeViewModel.EndDate);
            TempData["EndDate"] = endDate;
            TempData.Keep();
            if (endDate >= startDate)
            {
                return Json(true);

            }
            else
            {
                return Json($"Start date most is less then EndDate!!!!");

            }

        }

        [AcceptVerbs("Get")]
        public IActionResult StartDate(EmployeeViewModel employeeViewModel)
        {
            var startDate = Convert.ToDateTime(employeeViewModel.StartDate);
            TempData["StartDate"] = startDate;
            TempData.Keep();
            if (TempData.ContainsKey("EndDate"))
            {
                employeeViewModel.EndDate = Convert.ToDateTime(TempData["StartDate"]);

                TempData.Keep();
            }
            var endDate = employeeViewModel.EndDate;
            if (endDate >= startDate)
            {
                return Json(true);

            }
            else
            {
                return Json($"Start date most is less then EndDate!!!!");

            }
        }

    }
}

