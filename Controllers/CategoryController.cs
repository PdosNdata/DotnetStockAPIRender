using DotnetStockAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetStockAPI.Controllers;
//[Authorize(Roles =UserRolesModel.Admin + "," + UserRolesModel.Manager)]
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
   //สร้าง Object ของ ApplicationDbContext
    private readonly ApplicationDbContext _context;

    // ฟังก์ชันสร้าง Constructor รับค่า ApplicationDbContext
    public CategoryController(ApplicationDbContext context)
    {
        _context = context;
    }
    //CRUD category
   // CRUD Category
    // ฟังก์ชันสำหรับการดึงข้อมูล Category ทั้งหมด
    [HttpGet]
    public ActionResult<category> GetCategories()
    {
        // LINQ stand for "Language Integrated Query"
        var categories = _context.categories.ToList();

        // ส่งข้อมูลกลับไปให้ Client เป็น JSON
        return Ok(categories);
    }
    [HttpGet("{id}")]
    public ActionResult<category> GetCategory(int id){
        var category = _context.categories.Find(id);
        if (category == null){
            return NotFound();
        }
        return Ok(category);
    }

    [HttpPost]
    public ActionResult<category> AddCategory([FromBody] category category){
        _context.categories.Add(category);  //insert into database
        _context.SaveChanges();
        //ส่งข้อมูลกลับไปให้ Client เป็น JSON
        return Ok(category);
    }
    //[Authorize(Roles =UserRolesModel.Admin + "," + UserRolesModel.Manager)]
   [HttpPut("{id}")]
    public ActionResult<category> UpdateCategory(int id, [FromBody] category category) {
        if (id != category.categoryid) {
            return BadRequest();
        }
        _context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        _context.SaveChanges();
        return Ok(category);
    }
    //การลบ
    [HttpDelete("{id}")]
    public ActionResult<category> DeleteCategory(int id) {
        var category = _context.categories.Find(id);
        if (category == null) {
            return NotFound();
        }
        _context.categories.Remove(category);
        _context.SaveChanges();
        return Ok(category);
    }

}