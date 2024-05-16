using ConsumeSwaggerApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ConsumeSwaggerApi.Controllers
{
    public class ProductController : Controller
    {
        Uri basedAddress = new Uri("https://localhost:7036/api");
   private readonly HttpClient _client;

        public ProductController()
        {
            _client = new HttpClient();
            _client.BaseAddress = basedAddress;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Product> productList = new List<Product>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress+ "/Brand/GetBrands").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                productList = JsonConvert.DeserializeObject<List<Product>>(data);
            }
            return View(productList);
        }
        [HttpGet]
        public IActionResult Create() {
                    


            return View();
        
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            try
            {

                String data = JsonConvert.SerializeObject(product);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Brand/PostBrand", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Product Created";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id) 
        {
            Product product = new Product();
            try
            {
              
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Brand/GeetBrandById/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    product = JsonConvert.DeserializeObject<Product>(data);
                }
                return View(product);

            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
                return View();
            }

          
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            try
            {
                string data = JsonConvert.SerializeObject(product);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/Brand/PutBrand?id="+product.Id, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Product Updated Successfully";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
                return View();
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Product product = new Product();
            try
            {

                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Brand/GeetBrandById/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    product = JsonConvert.DeserializeObject<Product>(data);
                }
                return View(product);

            }

            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
                return View();
            }
        }
        [HttpPost,ActionName("Delete")] 
        public IActionResult DeleteConfirmed(int id) 
        {
            try
            {
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Brand/DeleteBrand/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Product Deleted Successfully";
                    return RedirectToAction("Index");
                }

            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
                return View();
            }
            return View();
        }
        
    }
}
