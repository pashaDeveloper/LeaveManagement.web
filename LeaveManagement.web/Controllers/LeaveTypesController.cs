using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeaveManagement.web.Data;
using LeaveManagement.web.Model;
using AutoMapper;
using LeaveManagement.web.ViewModels;
using JQueryAjaxCRUDWithModal;
using System.Diagnostics;

namespace LeaveManagement.web.Controllers
{
    public class LeaveTypesController : Controller
    {
        private readonly IdentityDbContext _context;
        private readonly IMapper _mapper;
        public LeaveTypesController(IdentityDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var leaveModel = _mapper.Map<List<LeaveTypeVM>>(await _context.LeaveTypes.Where(p=>
            (EF.Property<bool>(p,"IsRemoved")).Equals(false))
                .ToListAsync());
            return _context.LeaveTypes != null ?
                        View(leaveModel) :
                        Problem("Entity set 'IdentityDbContext.LeaveTypes'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LeaveTypes == null)
            {
                return NotFound();
            }

            var leaveType = await _context.LeaveTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveType == null)
            {
                return NotFound();
            }

            return View(leaveType);
        }


        [HttpGet]
        public async Task<IActionResult> CreateOrEdit(int? Id=0)
        {
            if (Id == 0)
            {
                return View(new LeaveTypeVM());
            }
            var leaveType = await _context.LeaveTypes.FirstOrDefaultAsync(m => m.Id == Id);
            if (Id != leaveType.Id)
            {
                return NotFound();
            }
            var leaveMapperype = _mapper.Map<LeaveTypeVM>(leaveType);
            return View(leaveMapperype);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit(int? id, LeaveTypeVM leaveType)
        {

            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    IsSuccess = false,
                    Message = "لطفا ورودی های خود را کنترل نمائید",

                });
            }
            if (id == 0)
            {
                var leaveTypeMap = _mapper.Map<LeaveType>(leaveType);
                await _context.AddAsync(leaveTypeMap);
                bool result = Convert.ToBoolean(await _context.SaveChangesAsync());
                if (result)
                {
                    return Json(new
                    {
                        IsSuccess = true,
                        Message = "مرخصی ثبت شد",
                        html = Helper.RenderRazorViewToString(this, "_LeaveType", GetViewPartial())
                    });
                }
            }
            else
            {
                try
                {
                    var leaveTypeUpdate = _mapper.Map<LeaveType>(leaveType);
                    _context.Update(leaveTypeUpdate);
                    bool result =Convert.ToBoolean(await _context.SaveChangesAsync());
                    if (result)
                    {
                        return Json(new
                        {
                            IsSuccess = true,
                            Message = "مرخصی با موفقیت ویرایش شد",
                            html = Helper.RenderRazorViewToString(this, "_LeaveType", GetViewPartial())
                        });
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveTypeExists(leaveType.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            return Json(new
            {
                IsSuccess = false,
                Message = "عملیات با خطا مواجه شد",
                html = Helper.RenderRazorViewToString(this, "CreateOrEdit", leaveType)
            });
        }


        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LeaveTypes == null)
            {
                return Problem("Entity set 'IdentityDbContext.LeaveTypes'  is null.");
            }
            var leaveType = await _context.LeaveTypes.FindAsync(id);
            if (leaveType != null)
            {
                _context.LeaveTypes.Remove(leaveType);
            }

           var result=Convert.ToBoolean(await _context.SaveChangesAsync());
            if (result)
            {
                return Json(new
                {
                    IsSuccess = true,
                    Message = "آیتم مورد نظر با موفقیت حذف شد",
                    html = Helper.RenderRazorViewToString(this, "_LeaveType", GetViewPartial())
                });
            }
            return Json(new
            {
                IsSuccess = false,
                Message = "عملیات با خطا مواجه شد",
            });
        }

        private bool LeaveTypeExists(int id)
        {
            return (_context.LeaveTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private object GetViewPartial()
        {
            var leaveList = _context.LeaveTypes.Where(p=>(EF.Property<bool>(p,"IsRemoved")).Equals(false)).ToList();
            var mapList = _mapper.Map<List<LeaveTypeVM>>(leaveList);
            return mapList;
        }
    }
}
