using TestProject1.DTO.Database1DTOs;

namespace TestProject1.Interfaces.DBase1Interfaces;

public interface IProductRepository
{
    Task<ProductsTableDTO> GetProductById(int id);
}