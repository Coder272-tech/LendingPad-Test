using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Core.Services.Products;
using WebApi.Models.Products;

namespace WebApi.Controllers
{
    [RoutePrefix("products")]
    public class ProductController : BaseApiController
    {
        private readonly ICreateProductService _createService;
        private readonly IUpdateProductService _updateService;
        private readonly IGetProductService _getService;
        private readonly IDeleteProductService _deleteService;

        public ProductController(ICreateProductService create, IUpdateProductService update, IGetProductService get, IDeleteProductService delete)
        {
            _createService = create;
            _updateService = update;
            _getService = get;
            _deleteService = delete;
        }

        [HttpPost]
        [Route("{id:guid}/create")]
        public HttpResponseMessage CreateProduct(Guid id, [FromBody] ProductModel model)
        {
            var product = _createService.Create(id, model.Name, model.Description, model.Price, model.StockQuantity, model.Tags);
            return Request.CreateResponse(HttpStatusCode.OK, new ProductData(product));
        }

        [HttpPost]
        [Route("{id:guid}/update")]
        public HttpResponseMessage UpdateProduct(Guid id, [FromBody] ProductModel model)
        {
            var product = _getService.GetProduct(id);
            if (product == null) return Request.CreateResponse(HttpStatusCode.NotFound);

            _updateService.Update(product, model.Name, model.Description, model.Price, model.StockQuantity, model.Tags);
            return Request.CreateResponse(HttpStatusCode.OK, new ProductData(product));
        }

        [HttpDelete]
        [Route("{id:guid}/delete")]
        public HttpResponseMessage DeleteProduct(Guid id)
        {
            var product = _getService.GetProduct(id);
            if (product == null) return Request.CreateResponse(HttpStatusCode.NotFound);

            _deleteService.Delete(product);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public HttpResponseMessage GetProduct(Guid id)
        {
            var product = _getService.GetProduct(id);
            if (product == null) return Request.CreateResponse(HttpStatusCode.NotFound);

            return Request.CreateResponse(HttpStatusCode.OK, new ProductData(product));
        }

        [HttpGet]
        [Route("debug/all")]
        public IHttpActionResult GetAllProducts()
        {
            var products = _getService.GetAllProducts()
                .Select(p => new {
                    p.Id,
                    p.Name,
                    p.Description,
                    p.Price,
                    p.StockQuantity,
                    Tags = p.Tags.ToArray()
                });

            return Ok(products);
        }

        [HttpGet]
        [Route("list")]
        public HttpResponseMessage GetProducts(string name = null, decimal? minPrice = null, decimal? maxPrice = null, string tag = null)
        {
            var products = _getService.GetProducts(name, minPrice, maxPrice, tag)
                .Select(p => new ProductData(p))
                .ToList();
            return Request.CreateResponse(HttpStatusCode.OK, products);
        }
    }
}
