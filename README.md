```
## ConenctionStrings 
in file appsettings.json
"ConnectionStrings": {
    "DefaultConnection": "Server=[YourServerName];Database=Razor_Project;Trusted_Connection=True;MuntipleActiveResultSets=True"
  },	
add entity framework in startup
services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


## ApplicationDbContext
public class ApplicationDbContext : DbContext{

     public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {        }
     // Add models to database	
     public DbSet<Item> Items { get; set; }

     protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Joke>().HasData(
                new Joke { Id= 1,JokeQuestion = "test1", JokeAnswer = "test1"});
        }
} 

## Controller 
public IActionResult Index()
        {
            IEnumerable<Item> objList = _db.Items;
            return View(objList);
        }

 public async Task<IActionResult> Index()
        {
            return View(await _context.Items.ToListAsync());
        }

## Views 
@model IEnumerable<ASP.NET_Demo.Models.{Model-Field}>

--- HTML Helper ----
@Html.DisplayNameFor(model => model.Model-Field)
@Html.DisplayFor(modelItem => item.Model-Field)

---  TAG Helper ----
<a class="navbar-brand" asp-area="" asp-page="/Index">Razor_Project</a>



```
## ASP.NET Core Identity.
```
# Role based Authorization
[Authorize(Roles = "Admin")]

# Application users
UserManager<IdentityUser> _userManager

# Signin authentication
SignInManager<IdentityUser> _signInManager

#User roles
RoleManager<IdentityRole> _roleManager
```
## Razor Pages
```
Install NuGet-Solution
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.SQLServer
Microsoft.EntityFrameworkCore.Tools
```
## Validation
```
<div class="text-danger" asp-validation-summary="ModelOnly"></div>
<span asp-validation-for="Book.Name" class="text-danger"></span>

@section Scripts{
    <partial name="_ValidationScriptsPartial" />
}
```

## Index
```
public IEnumerable<Book> Books { get; set; }
public async Task OnGetAsync()
{
	Books = await _db.Books.ToListAsync();
}
```
## Create 
```
[BindProperty]
public Book Book { get; set; }
public async Task<IActionResult> OnPostAsync()
{
        if (!ModelState.IsValid)
            {
                return Page();
            }

        _db.Books.Add(Book);
        await _db.SaveChangesAsync();
        return RedirectToPage("./Index");
}
```
## Edit
```
[BindProperty]
public Book Book { get; set; }
// Render the Edit Page first
public async Task OnGet(int Id)
{
       Book = await _db.Books.FindAsync(Id);
}
// Edit post request
public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var BookFromDb =  await _db.Books.FindAsync(Book.Id);
            BookFromDb.Name = Book.Name;
            BookFromDb.Author = Book.Author;
            BookFromDb.TSBN = Book.TSBN;

            await _db.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
```
## Delete
```
```
