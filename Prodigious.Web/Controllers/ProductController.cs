using Prodigious.Data;
using Prodigious.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using System.Web.Http.OData.Extensions;
using System.Net.Http.Headers;
using System.DirectoryServices;
using Prodigious.Web.Models;

namespace Prodigious.Web.Controllers
{

    public class ProductController : ApiController
    {

        IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [EnableQuery(MaxExpansionDepth = 3)]
        public IHttpActionResult GetProduct(int id)
        {
            try
            {
                if (id <= 0)
                    return NotFound();
                return Ok(SingleResult.Create(_productRepository.Get().Where(p => p.ProductID == id)));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetProduct(ODataQueryOptions<Product> queryOptions)
        {
            try
            {
                IQueryable results = queryOptions.ApplyTo(_productRepository.Get());
                return Ok(new
                {
                    Items = results,
                    Count = Request.ODataProperties().TotalCount
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostProduct(Product product)
        {
            try
            {
                if(product == null )
                    return BadRequest("Product can be Null");
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);
                Product newProduct = _productRepository.Insert(product);
                if (newProduct == null)
                    return Conflict();
                return Created<Product>(Request.RequestUri + newProduct.ProductID.ToString(), newProduct);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PutProduct(int id, Product product)
        {
            try
            {
                if (product == null)
                    return BadRequest("Product can be Null");
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                Product updateProduct = _productRepository.Update(product);
                if (updateProduct == null)
                    return NotFound();
                return Ok(updateProduct);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult DeleteProduct(int id)
        {
            try
            {
                if (id <= 0)
                    return NotFound();
                _productRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/Product/{id}/thumbnail")]
        public IHttpActionResult GetImage(int id)
        {
            byte[] thumbNailPhoto = _productRepository.Get(id).ThumbNailPhoto;
            if (thumbNailPhoto != null)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new ByteArrayContent(thumbNailPhoto);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
                return ResponseMessage(response);
            }
            return NotFound();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (_productRepository != null)
                _productRepository.Dispose();
        }

    }
}
