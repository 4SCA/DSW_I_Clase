using CibertecDemo.Data;
using CibertecDemo.Grpc;
using CibertecDemo.Models;
using Grpc.Core;

namespace CibertecDemo.Services
{
    public class ProductoGrpcService : ProductoService.ProductoServiceBase
    {
        private readonly ProductoRepository repo;
        private readonly ILogger<ProductoGrpcService> logger;

        public ProductoGrpcService(ProductoRepository repo, ILogger<ProductoGrpcService> logger)
        {
            this.repo = repo;
            this.logger = logger;
        }

        public override async Task<ProductoResponse> ObtenerProducto(ProductoRequest request, ServerCallContext context)
        {
            try
            {
                var producto = await repo.ObtenerProductoPorIdAsync(request.Id);

                if (producto == null)
                {
                    return new ProductoResponse()
                    {
                        Mensaje = $"Producto con ID {request.Id} no encontrado"
                    };
                }
                return new ProductoResponse
                {
                    Id = producto.Id,
                    Nombre = producto.Nombre,
                    Precio = (double) producto.Precio,
                    Cantidad = producto.Cantidad,
                    Estado = producto.Estado,
                    Mensaje = "Producto encontrado exitosamente"
                };
            }catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener producto con ID {ProductId}", request.Id);
                throw new RpcException(new Status(StatusCode.Internal, "Error interno del servidor."));

            }
        }

        public override async Task<ProductoResponse> CrearProducto(ProductoCrearRequest request, ServerCallContext context) 
        {
            try 
            {
                var nuevoProducto = new ProductoModel
                {
                    Nombre = request.Nombre,
                    Precio = (decimal) request.Precio,
                    Cantidad = request.Cantidad,
                    Estado = request.Estado
                };

                await repo.AgregarProductoAsync(nuevoProducto);
                var productos = await repo.ObtenerProductosAsync();
                var productoRecienCreado = productos.OrderByDescending(p => p.Id).First();

                return new ProductoResponse
                {
                    Id = productoRecienCreado.Id,
                    Nombre = request.Nombre,
                    Precio = request.Precio,
                    Cantidad = request.Cantidad,
                    Estado = request.Estado,
                    Mensaje = "Producto creado exitosamente"
                };
            }
            catch (Exception ex) 
            {
                logger.LogError(ex, "Error al crear producto: [ProductName]", request.Nombre);
                throw new RpcException(new Status(StatusCode.Internal, "Error al crear producto."));
            }
        }

        public override async Task<ListarProductosResponse> ListarProducto(ListarRequest request, ServerCallContext context) 
        {
            try 
            {
                if (request.Page > 0 && request.PageSize > 0)
                {
                    var (productos, totalRegistros) = await repo.obtenerProductosPaginado(request.Page, request.PageSize);
                    var totalPaginas = (int)Math.Ceiling((double)totalRegistros / request.PageSize);

                    var response = new ListarProductosResponse
                    {
                        TotalRegistros = totalPaginas,
                        PaginaActual = request.Page,
                        TotalPaginas = totalPaginas,
                    };

                    foreach (var product in productos)
                    {
                        response.Productos.Add(new ProductoResponse
                        {
                            Id = product.Id,
                            Nombre = product.Nombre,
                            Precio = (double)product.Precio,
                            Cantidad = product.Cantidad,
                            Estado = product.Estado
                        });
                    }
                    return response;
                }
                else {
                    var productos = await repo.ObtenerProductosAsync();
                    var response = new ListarProductosResponse
                    {
                        TotalRegistros = productos.Count,
                        PaginaActual = 1,
                        TotalPaginas = 1
                    };

                    foreach (var product in productos) 
                    {
                        response.Productos.Add(new ProductoResponse
                        {
                            Id = product.Id,
                            Nombre = product.Nombre,
                            Precio = (double)product.Precio,
                            Cantidad= product.Cantidad,
                            Estado = product.Estado
                        });
                    }
                    return response;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al listar productos");
                throw new RpcException(new Status(StatusCode.Internal, "Error al listar productos"));
            };
        }

        public override async Task<ProductoResponse> ActualizarProducto(ProductoActualizarRequest request, ServerCallContext context) 
        {
            try
            {
                var productoRequest = new ProductoModel
                {
                    Id = request.Id,
                    Nombre = request.Nombre,
                    Precio = (decimal)request.Precio,
                    Cantidad = request.Cantidad,
                    Estado = request.Estado
                };

                await repo.ActualizarProductoAsync(productoRequest);
                var productoBd = await repo.ObtenerProductoPorIdAsync(request.Id);

                return new ProductoResponse
                {
                    Id = productoBd.Id,
                    Nombre = productoBd.Nombre,
                    Precio = (double)productoBd.Precio,
                    Cantidad = productoBd.Cantidad,
                    Estado = productoBd.Estado,
                    Mensaje = "Producto actualizado exitosamente"
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al actualizar el producto: [ProductName]", request.Nombre);
                throw new RpcException(new Status(StatusCode.Internal, "Error al actualizar el producto."));
            }
        }

        public override async Task<EliminarResponse> EliminarProducto(ProductoRequest request, ServerCallContext context) 
        {
            throw new RpcException(new Status(StatusCode.Unimplemented, "Método no implementado"));
        }
    }
}
